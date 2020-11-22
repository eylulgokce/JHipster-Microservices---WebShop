using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Database
{
    public class MySQLDatabase : IOrderDatabse
    {
        public IEnumerable<Order> GetAllOrders()
        {
            var connection = GetConnection();
            var cmd = new MySqlCommand("SELECT * FROM orders", connection);
            var reader = cmd.ExecuteReader();
            var orders = new List<Order>();
            while (reader.Read())
            {
                var costumerid = reader.GetInt32("costumerid");
                var products = reader.GetInt32("products");
                var totalprice = reader.GetDecimal("totalprice");
                var orderdate = reader.GetDateTime("date");
                orders.Add(new Order(costumerid, products, totalprice, orderdate));
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
