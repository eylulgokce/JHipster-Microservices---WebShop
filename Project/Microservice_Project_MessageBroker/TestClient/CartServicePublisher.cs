using System;
using MicroserviceCommon.Clients;
using MicroserviceCommon.CommonModel.Cart;

namespace TestClient
{
    public class CartServicePublisher : AbstractTestPublisher
    {
        private readonly int _numSelectProductRequests;
        private readonly TimeSpan _timeSpanBetweenOrders;

        public CartServicePublisher(int numSelectProductRequests, TimeSpan timeSpanBetweenOrders)
        {
            _numSelectProductRequests = numSelectProductRequests;
            _timeSpanBetweenOrders = timeSpanBetweenOrders;
        }
        
        protected override void Run()
        {
            var cartBrokerClient = new CartBrokerClientRabbitMQ();
            var random = new Random();
            for (var i = 0; i < _numSelectProductRequests; i++)
            {
                if(i > 0)
                {
                    //System.Threading.Thread.Sleep(_timeSpanBetweenOrders);
                }

                var selectProductRequest = new SelectProductRequest
                {
                    IdCustomer = 1,
                    SelectedProduct = new SelectedProduct(1, 1 + random.Next(3))
                };

                Console.WriteLine($"Publishing select product request #{i + 1} with {selectProductRequest.SelectedProduct.NumUnits} units");
                cartBrokerClient.AddProductToCart(selectProductRequest);
            }
        }
    }
}
