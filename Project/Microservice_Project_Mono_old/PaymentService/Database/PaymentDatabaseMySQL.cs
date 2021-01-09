using MicroserviceCommon.ErrorHandling;
using MySql.Data.MySqlClient;
using PaymentService.Model;
using System;
using System.Collections.Generic;

namespace PaymentService.Database
{
    public class PaymentDatabaseMySQL: IPaymentDatabase
    {
        public void AddPayment(Payment payment)
        {
            var connection = GetConnection();
            var cmd = new MySqlCommand("INSERT INTO microservices.payment (idOrder, paymentMethod) VALUES (@idOrder, @paymentMethod)", connection);
            cmd.Parameters.AddWithValue("@idOrder", payment.IdOrder);
            cmd.Parameters.AddWithValue("@paymentMethod", payment.PaymentMethod);
            try
            {
                var affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows != 1)
                {
                    throw new BaseMicroserviceException(System.Net.HttpStatusCode.InternalServerError, "Could not add payment to the system!");
                }
            }
            catch(MySqlException ex)
            {
                if(ex.Message.Contains("UNIQUE"))
                {
                    throw new BaseMicroserviceException(System.Net.HttpStatusCode.BadRequest, "This order has already been paid for!");
                }
            }
        }

        public IEnumerable<Payment> ListAllPayments(int idCustomer)
        {
            var connection = GetConnection();
            var cmd = new MySqlCommand("SELECT * FROM microservices.payment WHERE payment.idcostumer = {idCustomer}", connection);
            var reader = cmd.ExecuteReader();
            var payments = new List<Payment>();
            while (reader.Read())
            {
                var costumerid = reader.GetInt32("idcostumer");
                var paymentmethod = reader.GetString("paymentmethod");
                payments.Add(new Payment(costumerid, paymentmethod));
            }

            reader.Close();
            connection.Close();

            return payments;
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
    }
}
