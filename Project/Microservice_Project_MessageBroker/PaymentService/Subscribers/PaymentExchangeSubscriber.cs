using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.CommonModel;
using PaymentService.Database;
using System;

namespace PaymentService.Subscribers
{
    public class PaymentExchangeSubscriber: IPaymentExchangeSubscriber
    {
        private readonly IPaymentDatabase _paymentDatabase;

        public PaymentExchangeSubscriber(IPaymentBrokerClient paymentBrokerClient, IPaymentDatabase paymentDatabase)
        {
            _paymentDatabase = paymentDatabase;
            paymentBrokerClient.Subscribe("payment-service", OnPaymentReceived);
        }

        public void OnPaymentReceived(Payment payment)
        {
            _paymentDatabase.AddPayment(payment);
        }
    }
}
