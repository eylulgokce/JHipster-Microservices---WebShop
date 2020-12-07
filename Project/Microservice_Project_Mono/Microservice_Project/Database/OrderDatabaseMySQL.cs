using MicroserviceCommon.ErrorHandling;
using MicroserviceCommon.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace OrderService.Database
{
    public class OrderDatabaseMySQL : IOrderDatabase
    {
        public void AddOrder(Order order)
        {
            // Disposable
            using var connection = GetConnection();
            var transaction = connection.BeginTransaction();
            var insertToOrderCommand = new MySqlCommand($@"INSERT INTO microservices.order (idCustomer, totalprice)
                                VALUES ({ order.IdCustomer }, 0.0)", connection, transaction);

            var rowsAffected = insertToOrderCommand.ExecuteNonQuery();
            if (rowsAffected != 1)
            {
                throw new Exception("There is an error occoured when inserting an Order! " + rowsAffected + "Orders are effected when executing the query!");
            }

            if (!GetLastInsertId(connection, transaction, out var idOrder))
            {
                throw new Exception("Could not get last order id");
            }

            // INSERT INTO order
            foreach (var orderToProduct in order.Products)
            {
                // INSERT INTO ordertoproduct
                var insertToOtp = new MySqlCommand($@"
                                INSERT INTO microservices.ordertoproduct (idOrder, idProduct, numBoughtUnits)
                                VALUES ({idOrder}, {orderToProduct.IdProduct}, {orderToProduct.NumBoughtUnits})", connection, transaction);

                rowsAffected = insertToOtp.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    throw new Exception("There is an error occoured when inserting to ordertoproduct! " + rowsAffected + "Orders are effected when executing the query!");
                }
            }

            var totalPrice = CalculateTotalPrice(connection, transaction, idOrder);
            var updateOrderCommand = new MySqlCommand($"UPDATE microservices.order SET totalprice=@paramTotalPrice WHERE idOrder=@paramIdOrder", connection, transaction);
            updateOrderCommand.Parameters.AddWithValue("@paramTotalPrice", totalPrice);
            updateOrderCommand.Parameters.AddWithValue("@paramIdOrder", idOrder);
            updateOrderCommand.ExecuteNonQuery();

            //UPDATE PRODUCT QUANTITY
            var boughtUnitsFromOrder = new MySqlCommand($@"SELECT P.idProduct, OTP.numBoughtUnits
                                                FROM microservices.ordertoproduct OTP
                                                JOIN microservices.products P ON P.idProduct = OTP.idProduct
                                                WHERE OTP.idOrder={idOrder}", connection, transaction);

            var numBoughtUnitsPerProduct = new Dictionary<int, int>();
            using (var reader = boughtUnitsFromOrder.ExecuteReader())
            {
                while (reader.Read())
                {
                    var idProduct = reader.GetInt32("idProduct");
                    var numBoughtUnits = reader.GetInt32("numBoughtUnits");
                    if(numBoughtUnits <= 0)
                    {
                        throw new BadRequestMicroserviceException($"Tried to buy invalid amount of {numBoughtUnits} unit(s) of product ID {idProduct}!");
                    }
                    numBoughtUnitsPerProduct.Add(idProduct, numBoughtUnits);
                }
            }

            foreach (var entry in numBoughtUnitsPerProduct)
            {
                var reduceQuantityByProductID = new MySqlCommand($@"UPDATE microservices.products
                                                SET availableUnits = availableUnits-{entry.Value}
                                                WHERE idProduct={entry.Key} AND availableUnits >= {entry.Value}", connection, transaction);

                var affectedRows = reduceQuantityByProductID.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    throw new NotFoundMicroserviceException($"Product with ID {entry.Key} could not be sold (failed to sell {entry.Value} unit(s))!");
                }
            }

            transaction.Commit();
        }

        private decimal CalculateTotalPrice(MySqlConnection connection, MySqlTransaction transaction, int idOrder)
        {
            var query = $@"SELECT SUM(OTP.numBoughtUnits*P.price)
                                FROM microservices.ordertoproduct OTP
                                JOIN microservices.products P ON P.idProduct = OTP.idProduct
                                WHERE OTP.idOrder={idOrder}";

            GetQueryResultSimpleDecimal(connection, transaction, query, out var result);
            return result;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var connection = GetConnection();
            var cmd = new MySqlCommand("SELECT * FROM microservices.order", connection);
            var reader = cmd.ExecuteReader();
            var orders = new List<Order>();
            while (reader.Read())
            {
                var costumerid = reader.GetInt32("costumerid");
                var totalprice = reader.GetDecimal("totalprice");
                var orderdate = reader.GetDateTime("date");
                orders.Add(new Order(costumerid, totalprice, orderdate));
            }

            reader.Close();
            connection.Close();

            return orders;
        }

        public IEnumerable<Product> GetAllProductsByOrderId(int idOrder)
        {
            var connection = GetConnection();
            var query = $@"SELECT P.idProduct, P.name, P.description, P.price, P.availableUnits
                            FROM microservices.ordertoproduct OTP
                            JOIN microservices.products P ON P.idProduct = OTP.idProduct
                            WHERE OTP.idOrder={idOrder}";

            var cmd = new MySqlCommand(query, connection);
            var reader = cmd.ExecuteReader();
            var products = new List<Product>();
            while (reader.Read())
            {
                var name = reader.GetString("name");
                var description = reader.GetString("description");
                var price = reader.GetDecimal("price");
                var availableUnits = reader.GetInt32("availableUnits");
                products.Add(new Product( name, description, price, availableUnits));
            }

            reader.Close();
            connection.Close();

            return products;
        }

        public MySqlConnection GetConnection()
        {
            var server = "localhost";
            var database = "microservices";
            var username = "root";
            var password = "root";

            string connectionString = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={password}";
            try
            {
                var connection = new MySqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        private bool GetQueryResultSimpleDecimal(MySqlConnection connection, MySqlTransaction transaction, string query, out decimal result)
        {
            var cmd = new MySqlCommand(query, connection, transaction);
            var rdr = cmd.ExecuteReader();
            result = 0;
            if (!rdr.HasRows)
            {
                rdr.Close();
                return false;
            }

            rdr.Read();
            result = rdr.GetDecimal(0);
            rdr.Close();

            return true;
        }

        private bool GetQueryResultSimpleInt32(MySqlConnection connection, MySqlTransaction transaction, string query, out int result)
        {
            var cmd = new MySqlCommand(query, connection, transaction);
            var rdr = cmd.ExecuteReader();
            result = 0;
            if (!rdr.HasRows)
            {
                rdr.Close();
                return false;
            }

            rdr.Read();
            result = rdr.GetInt32(0);
            rdr.Close();

            return true;
        }

        private bool GetLastInsertId(MySqlConnection connection, MySqlTransaction transaction, out int result)
        {
            return GetQueryResultSimpleInt32(connection, transaction, "SELECT LAST_INSERT_ID()", out result);
        }
    }
}
