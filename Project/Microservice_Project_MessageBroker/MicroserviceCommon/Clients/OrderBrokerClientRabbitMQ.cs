using System;
using System.Text;
using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.CommonModel.Order;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MicroserviceCommon.Clients
{
    public class OrderBrokerClientRabbitMQ : IOrderBrokerClient
    {
        public const string OrderExchangeName = "orders-exchange";

        private readonly IModel _channel;

        public OrderBrokerClientRabbitMQ()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.ExchangeDeclare(OrderExchangeName, ExchangeType.Fanout);
        }

        public void Subscribe(string queueName, Action<Order> onOrderReceived)
        {
            Console.WriteLine($"Subscribing to queue {queueName} in exchange {OrderExchangeName}...");
            _channel.QueueDeclare(queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueBind(queueName, OrderExchangeName, string.Empty, null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, eventArguments) =>
            {
                var body = eventArguments.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var order = JsonConvert.DeserializeObject<Order>(json);
                onOrderReceived(order);
            };

            _channel.BasicConsume(queueName, true, consumer);
        }

        public void PublishOrder(Order order)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var orderJson = JsonConvert.SerializeObject(order);
                var bytes = Encoding.UTF8.GetBytes(orderJson.ToString());
                channel.BasicPublish(OrderExchangeName, string.Empty, null, bytes);
            }
        }
    }
}
