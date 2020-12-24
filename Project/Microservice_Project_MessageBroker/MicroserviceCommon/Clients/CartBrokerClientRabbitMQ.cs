using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.CommonModel.Cart;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace MicroserviceCommon.Clients
{
    public class CartBrokerClientRabbitMQ : ICartBrokerClient
    {
        public const string CartExchangeName = "cart-exchange";

        private readonly IModel _channel;

        public CartBrokerClientRabbitMQ()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.ExchangeDeclare(CartExchangeName, ExchangeType.Fanout);
        }

        public void AddProductToCart(SelectProductRequest request)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
            _channel.BasicPublish(CartExchangeName, string.Empty, null, body);
        }

        public void Subscribe(string queueName, Action<SelectProductRequest> onAddProductToCart)
        {
            Console.WriteLine($"Subscribing to queue {queueName} in exchange {CartExchangeName}...");
            _channel.QueueDeclare(queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueBind(queueName, CartExchangeName, string.Empty, null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, eventArguments) =>
            {
                var body = eventArguments.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var selectProductRequest = JsonConvert.DeserializeObject<SelectProductRequest>(json);
                onAddProductToCart(selectProductRequest);
            };

            _channel.BasicConsume(queueName, true, consumer);
        }
    }
}
