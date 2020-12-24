using System;
using MicroserviceCommon.CommonModel.Order;

namespace MicroserviceCommon.Clients.Interfaces
{
    public interface IOrderBrokerClient
    {
       void Subscribe(string queueName, Action<Order> onOrderReceived);
    }
}
