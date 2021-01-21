using System;
using System.Collections.Generic;
using MicroserviceCommon.Clients;
using MicroserviceCommon.CommonModel;
using MicroserviceCommon.CommonModel.Order;

namespace TestClient
{
    public class RandomPaymentPublisher : AbstractTestPublisher
    {
        private readonly int _numPayments;
        private readonly TimeSpan _timeSpanBetweenPayments;

        public RandomPaymentPublisher(int numPayments, TimeSpan timeSpanBetweenPayments)
        {
            _numPayments = numPayments;
            _timeSpanBetweenPayments = timeSpanBetweenPayments;
        }
        
        protected override void Run()
        {
            var paymentBroker = new PaymentBrokerClientRabbitMQ();
            var random = new Random();
            for (var i = 1; i < _numPayments+1; i++)
            {
                var payment = new Payment(i, "VISA");

                Console.WriteLine($"Publishing payment #{i} for Order {payment.IdOrder} ");
                paymentBroker.PublishPayment(payment);
            }
        }
    }
}
