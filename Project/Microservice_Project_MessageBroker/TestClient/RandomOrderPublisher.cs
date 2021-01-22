using System;
using System.Collections.Generic;
using MicroserviceCommon.Clients;
using MicroserviceCommon.CommonModel.Order;

namespace TestClient
{
    public class RandomOrderPublisher : AbstractTestPublisher
    {
        private readonly int _numOrders;
        private readonly TimeSpan _timeSpanBetweenOrders;

        public RandomOrderPublisher(int numOrders, TimeSpan timeSpanBetweenOrders)
        {
            _numOrders = numOrders;
            _timeSpanBetweenOrders = timeSpanBetweenOrders;
        }
        
        protected override void Run()
        {
            var orderBrokerClient = new OrderBrokerClientRabbitMQ();
            var random = new Random();
            for (var i = 0; i < _numOrders; i++)
            {
                var order = new Order(1, 10.0M, DateTime.Now)
                {
                    Products = new List<OrderToProduct>
                    {
                        new OrderToProduct(0, 4, 1+random.Next(3))
                    }
                };

                Console.WriteLine($"Publishing order #{i + 1} with {order.Products[0].NumBoughtUnits} units");
                orderBrokerClient.PublishOrder(order);
            }
        }
    }
}
