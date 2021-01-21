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

        private static int paymentNumber = 0;
        private static DateTime startTime;

        public void OnPaymentReceived(Payment payment)
        {

            if (paymentNumber == 0)
            {
                startTime = DateTime.Now;
            }

            paymentNumber++;
            _logger.LogInformation($"Adding Payment #{paymentNumber} with {payment.PaymentMethod} to database...");
            _paymentDatabase.AddPayment(payment);


            var totalTime = (DateTime.Now - startTime).TotalSeconds;
            var averageTime = totalTime / paymentNumber;
            _logger.LogInformation($"Average time to process a payment: {averageTime:G2}s");

            /*
            try
            {
                _paymentDatabase.AddPayment(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            */
        }
    }
}
