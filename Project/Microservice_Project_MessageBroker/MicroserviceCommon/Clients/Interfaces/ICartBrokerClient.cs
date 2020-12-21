using MicroserviceCommon.CommonModel.Cart;
using System;

namespace MicroserviceCommon.Clients.Interfaces
{
    public interface ICartBrokerClient
    {
        void AddProductToCart(SelectProductRequest request);
        void Subscribe(string queueName, Action<SelectProductRequest> onAddProductToCart);
    }
}
