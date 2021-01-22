using System;
using System.Text;
using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.CommonModel;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MicroserviceCommon.Clients
{
    public class PaymentBrokerClientRabbitMQ : IPaymentBrokerClient
    {
        public const string PaymentExchangeName = "payments-exchange";

        private readonly IModel _channel;

        public PaymentBrokerClientRabbitMQ()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.ExchangeDeclare(PaymentExchangeName, ExchangeType.Fanout);
        }

        public void PublishPayment(Payment payment)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var paymentJson = JsonConvert.SerializeObject(payment);
                var bytes = Encoding.UTF8.GetBytes(paymentJson.ToString());
                channel.BasicPublish(PaymentExchangeName, string.Empty, null, bytes);
            }
        }

        public void Subscribe(string queueName, Action<Payment> onPaymentReceived)
        {
            Console.WriteLine($"Subscribing to queue {queueName} in exchange {PaymentExchangeName}...");
            _channel.QueueDeclare(queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueBind(queueName, PaymentExchangeName, string.Empty, null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, eventArguments) =>
            {
                var body = eventArguments.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var payment = JsonConvert.DeserializeObject<Payment>(json);
                onPaymentReceived(payment);
            };

            _channel.BasicConsume(queueName, true, consumer);
        }
    }
}
