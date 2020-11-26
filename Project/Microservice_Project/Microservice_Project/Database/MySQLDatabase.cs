using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Database
{
    public class MySQLDatabase : IOrderDatabse
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
            var connection = GetConnection();
            // Das soll als Transaktion
           
            using(MySqlConnection db = connection) {
                MySqlTransaction transaction;

                db.Open();
                transaction = db.BeginTransaction();

                // INSERT INTO order
                foreach (var orderToProduct in order.OrderToProducts)
                {
                    // INSERT INTO ordertoproduct
                    MySqlCommand cmd = new MySqlCommand($@"
                                INERT INTO orders.ordertoproduct ('idOrder', 'idProduct', 'numBoughtUnits')
                                VALUES ({orderToProduct.IdOrder}, {orderToProduct.IdProduct}, {orderToProduct.NumBoughtUnits})", db, transaction);
                }

                transaction.Commit();
            }

            var insertToOrder = new MySqlCommand($@"INSERT INTO orders.order ('costumerid', 'totalprice') 
                                VALUES ({ costumerid }, { calculateTotalPrice() })", connection);
            var reader = insertToOrder.ExecuteReader();
        }

        private decimal calculateTotalPrice()
        {
            decimal total = 0;
            var connection = GetConnection();
            var cmd = new MySqlCommand($@"SELECT SUM(price) FROM orders.ordertoproduct OTP
                                JOIN products.products P ON P.idProduct = OTP.idProduct", connection);
            
            var reader = cmd.ExecuteReader();
            total = reader.GetDecimal("sum(price)");

            reader.Close();
            connection.Close();

            return total;
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
    }
}
