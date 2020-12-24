using OrderService.Database;
using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.CommonModel.Order;

namespace OrderService.Subscribers
{
    public class OrderExchangeSubscriber: IOrderExchangeSubscriber
    {
        private readonly IOrderDatabase _orderDatabase;

        public OrderExchangeSubscriber(IOrderBrokerClient orderBrokerClient, IOrderDatabase orderDatabase)
        {
            _orderDatabase = orderDatabase;
            orderBrokerClient.Subscribe("order-service", OnOrderReceived);
        }

        public void OnOrderReceived(Order order)
        {
            _orderDatabase.AddOrder(order);
        }
    }
}
