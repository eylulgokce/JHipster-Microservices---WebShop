using MicroserviceCommon.CommonModel;
using System;

namespace MicroserviceCommon.Clients.Interfaces
{
    public interface IPaymentBrokerClient
    {
       void Subscribe(string queueName, Action<Payment> onPaymentReceived);

       void PublishPayment(Payment payment);
    }
}
