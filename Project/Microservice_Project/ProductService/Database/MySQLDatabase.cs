using MySql.Data.MySqlClient;
using ProductService.Model;
using System;
using System.Collections.Generic;

namespace ProductService.Database
{
    public class MySQLDatabase : IProductDatabase
    {
        private const string ProductTableName = "products";

        public IEnumerable<Product> GetAllProducts()
        {
            var connection = GetConnection();
            var cmd = new MySqlCommand($"SELECT * FROM {ProductTableName}", connection);
            var reader = cmd.ExecuteReader();
            var products = new List<Product>();
            while(reader.Read())
            {
                var name = reader.GetString("name");
                var description = reader.GetString("description");
                var price = reader.GetDecimal("price");
                var availableUnits = reader.GetInt32("availableUnits");
                products.Add(new Product(name, description, price, availableUnits));
            }

            reader.Close();
            connection.Close();

            return products;
        }

        public void SellProduct(int idProduct, int numSoldUnits)
        {
            var connection = GetConnection();
            var cmd = new MySqlCommand($@"
                UPDATE {ProductTableName}
                SET availableUnits=availableUnits-{numSoldUnits}
                WHERE idProduct={idProduct} AND availableUnits >= {numSoldUnits}", connection);

            var affectedRows = cmd.ExecuteNonQuery();
            if(affectedRows == 0)
            {
                throw new Exception($"Product with ID {idProduct} could not be sold (failed to sell {numSoldUnits} unit(s))!");
            }
        }

        private MySqlConnection GetConnection()
        {
            var server = "localhost";
            var database = "products";
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
