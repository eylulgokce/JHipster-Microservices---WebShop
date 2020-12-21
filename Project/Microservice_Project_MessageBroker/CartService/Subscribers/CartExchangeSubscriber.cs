using CartService.Database;
using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.CommonModel.Cart;

namespace CartService.Subscribers
{
    public class CartExchangeSubscriber : ICartExchangeSubscriber
    {
        private readonly ICartDatabase _cartDatabase;

        public CartExchangeSubscriber(ICartBrokerClient cartBrokerClient, ICartDatabase cartDatabase)
        {
            _cartDatabase = cartDatabase;
            cartBrokerClient.Subscribe("cart-service", OnAddProductToCart);
        }

        public void OnAddProductToCart(SelectProductRequest selectProductRequest)
        {
            _cartDatabase.AddProductToCart(selectProductRequest.IdCustomer, selectProductRequest.SelectedProduct);
        }
    }
}
