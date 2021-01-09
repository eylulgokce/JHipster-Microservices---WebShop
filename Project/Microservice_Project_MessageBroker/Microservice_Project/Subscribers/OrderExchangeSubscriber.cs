using System;
using OrderService.Database;
using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.CommonModel.Order;
using Microsoft.Extensions.Logging;

namespace OrderService.Subscribers
{
    public class OrderExchangeSubscriber: IOrderExchangeSubscriber
    {
        private readonly IOrderDatabase _orderDatabase;
        private readonly ILogger<OrderExchangeSubscriber> _logger;

        public OrderExchangeSubscriber(IOrderBrokerClient orderBrokerClient, IOrderDatabase orderDatabase, ILogger<OrderExchangeSubscriber> logger)
        {
            _orderDatabase = orderDatabase;
            _logger = logger;
            orderBrokerClient.Subscribe("order-service", OnOrderReceived);
        }

        private static int orderNumber = 0;
        private static DateTime startTime;

        public void OnOrderReceived(Order order)
        {
            if (orderNumber == 0)
            {
                startTime = DateTime.Now;
            }

            orderNumber++;
            _logger.LogInformation($"Adding order #{orderNumber} with {order.Products[0].NumBoughtUnits} to database...");
            _orderDatabase.AddOrder(order);

            var totalTime = (DateTime.Now - startTime).TotalSeconds;
            var averageTime = totalTime / orderNumber;
            _logger.LogInformation($"Average time to process an order: {averageTime:G2}s");
        }
    }
}
