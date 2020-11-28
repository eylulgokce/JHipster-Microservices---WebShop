using MySql.Data.MySqlClient;
using PaymentService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Database
{
    public class MySQLDatabase : IPaymentDatabase
    {
        public void AddPayment(Payment payment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payment> ListAllPayments(int idCustomer)
        {
            var connection = GetConnection();
            var cmd = new MySqlCommand("SELECT * FROM payments.payment WHERE payment.idcostumer = {idCustomer}", connection);
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
            var database = "payments";
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
