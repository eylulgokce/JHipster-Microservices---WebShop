using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Database
{
    public class MySQLDatabase : IOrderDatabase
    {
        private int costumerid;

        private int GetNextOrderID()
        {
            var connection = GetConnection();
            var cmd = new MySqlCommand($@"
                 SELECT AUTO_INCREMENT
                 FROM information_schema.TABLES
                 WHERE TABLE_SCHEMA = 'orders'
                 AND TABLE_NAME = 'order'", connection);

            var reader = cmd.ExecuteReader();
            int nextOrderId = reader.GetInt32("AUTO_INCREMENT");
            reader.Close();
            connection.Close();

            return nextOrderId;
        }
        public void AddOrder(Order order)
        {
            // Disposable
            using var connection = GetConnection();
            var transaction = connection.BeginTransaction();
            var insertToOrderCommand = new MySqlCommand($@"INSERT INTO orders.order (idCustomer, totalprice)
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
                                INSERT INTO orders.ordertoproduct (idOrder, idProduct, numBoughtUnits)
                                VALUES ({idOrder}, {orderToProduct.IdProduct}, {orderToProduct.NumBoughtUnits})", connection, transaction);

                rowsAffected = insertToOtp.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    throw new Exception("There is an error occoured when inserting to ordertoproduct! " + rowsAffected + "Orders are effected when executing the query!");
                }
            }

            var totalPrice = CalculateTotalPrice(connection, transaction, idOrder);
            var updateOrderCommand = new MySqlCommand($"UPDATE orders.order SET totalprice=@paramTotalPrice WHERE idOrder=@paramIdOrder", connection, transaction);
            updateOrderCommand.Parameters.AddWithValue("@paramTotalPrice", totalPrice);
            updateOrderCommand.Parameters.AddWithValue("@paramIdOrder", idOrder);
            updateOrderCommand.ExecuteNonQuery();

            //UPDATE PRODUCT QUANTITY
            var BoughtUnitsFromOrder = new MySqlCommand($@"SELECT P.idProduct, OTP.numBoughtUnits
                                                FROM orders.ordertoproduct OTP
                                                JOIN products.products P ON P.idProduct = OTP.idProduct
                                                WHERE OTP.idOrder={idOrder}", connection, transaction);

            var reader = BoughtUnitsFromOrder.ExecuteReader();
            var numBoughtUnitsPerProduct = new Dictionary<int, int>();

            while (reader.Read())
            {
                var idProduct = reader.GetInt32("idProduct");
                var numBoughtUnits = reader.GetInt32("numBoughtUnits");
                numBoughtUnitsPerProduct.Add(idProduct, numBoughtUnits);
            }

            foreach (KeyValuePair<int, int> entry in numBoughtUnitsPerProduct)
            {
                var reduceQuantityByProductID = new MySqlCommand($@"UPDATE products
                                                SET availableUnits = availableUnits-{entry.Value}
                                                WHERE idProduct={entry.Key} AND availableUnits >= {entry.Value}", connection, transaction);

                var affectedRows = reduceQuantityByProductID.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    throw new Exception($"Product with ID {entry.Key} could not be sold (failed to sell {entry.Value} unit(s))!");
                }
            }

            transaction.Commit();
        }

        private decimal CalculateTotalPrice(MySqlConnection connection, MySqlTransaction transaction, int idOrder)
        {
            var query = $@"SELECT SUM(OTP.numBoughtUnits*P.price)
                                FROM orders.ordertoproduct OTP
                                JOIN products.products P ON P.idProduct = OTP.idProduct
                                WHERE OTP.idOrder={idOrder}";

            GetQueryResultSimpleDecimal(connection, transaction, query, out var result);
            return result;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var connection = GetConnection();
            var cmd = new MySqlCommand("SELECT * FROM orders.order", connection);
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

        public MySqlConnection GetConnection()
        {
            var server = "localhost";
            var database = "orders";
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
