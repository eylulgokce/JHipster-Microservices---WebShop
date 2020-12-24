using MicroserviceCommon.CommonModel.Order;

namespace OrderService.Subscribers
{
    public interface IOrderExchangeSubscriber
    {
        void OnOrderReceived(Order order);
    }
}
