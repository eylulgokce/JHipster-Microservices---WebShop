using MySql.Data.MySqlClient;
using System;
using MicroserviceCommon.ErrorHandling;

namespace CostumerService.Database
{
    public class MySQLDatabase : ICustomerDatabase
    {
        public int FindCostumerID(string firstname, string lastname, string email)
        {
            using var connection = GetConnection();
            using var cmd = new MySqlCommand("SELECT idcostumers FROM costumers where firstname = {firstname} and surname = {surname}", connection);
            using var reader = cmd.ExecuteReader();
            var customerId = 0;
            while (reader.Read())
            {
                customerId = reader.GetInt32("idcostumers");
            }

            if (customerId == 0)
            {
                Console.WriteLine("NO CUSTOMER FOUND!");
            }

            return customerId;
        }

        public void InsertCustomer(string firstname, string lastname, string email, string address, string city, string country)
        {
            using var connection = GetConnection();
            using var cmd = new MySqlCommand($@"INSERT INTO costumers (firstname, surname, email, address, city, country) 
                                                VALUES ('{firstname}', '{lastname}', '{email}', '{address}', '{city}', '{country}')", connection);
            Console.WriteLine($"Creating customer {firstname} {lastname}");
            try
            {
                var affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows != 1)
                {
                    throw new BaseMicroserviceException(System.Net.HttpStatusCode.InternalServerError, "Could not insert costumer to the system!");
                }
            }
            catch (MySqlException ex)
            {
            }
        }


        public MySqlConnection GetConnection()
        {
            var server = "localhost";
            var database = "costumers";
            var username = "root";
            var password = "root";

            string connectionString = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={password}";
            try
            {
                var connection = new MySqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (MySqlException)
            {
            }

            return null;
        }

    }
}
