using System;
using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.CommonModel;
using Microsoft.Extensions.Logging;
using PaymentService.Database;

namespace PaymentService.Subscribers
{
    public class PaymentExchangeSubscriber: IPaymentExchangeSubscriber
    {
        private readonly IPaymentDatabase _paymentDatabase;
        private readonly ILogger<PaymentExchangeSubscriber> _logger;

        public PaymentExchangeSubscriber(IPaymentBrokerClient paymentBrokerClient, IPaymentDatabase paymentDatabase, ILogger<PaymentExchangeSubscriber> logger)
        {
            _paymentDatabase = paymentDatabase;
            paymentBrokerClient.Subscribe("payment-service", OnPaymentReceived);
            _logger = logger;
        }

        public void OnPaymentReceived(Payment payment)
        {
            try
            {
                _paymentDatabase.AddPayment(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
