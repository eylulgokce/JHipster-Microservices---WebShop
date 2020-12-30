using MicroserviceCommon.ErrorHandling;
using MySql.Data.MySqlClient;
using System;

namespace CostumerService.Database
{
    public class CostumerDatabaseMySQL : ICostumerDatabase
    {
        public int FindCostumerID(string firstname, string lastname, string email)
        {
            var connection = GetConnection();
            var cmd = new MySqlCommand($"SELECT idcostumers FROM costumers where firstname = {firstname} and surname = {lastname}", connection);
            var reader = cmd.ExecuteReader();
            var costumerid = 0;
            while (reader.Read())
            {
                costumerid = reader.GetInt32("idcostumers");
            }

            if (costumerid == 0)
            {
                Console.WriteLine("NO COSTUMER FOUND!");
            }

            reader.Close();
            connection.Close();

            return costumerid;
        }

        public void insertCostumer(string firstname, string lastname, string email, string address, string city, string country)
        {
            using var connection = GetConnection();
            var cmd = new MySqlCommand($@"INSERT INTO costumers.costumers (firstname, surname, email, address, city, country) 
                                                VALUES ({firstname}, {lastname}, {email}, {address}, {city}, {country})", connection);
            
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
