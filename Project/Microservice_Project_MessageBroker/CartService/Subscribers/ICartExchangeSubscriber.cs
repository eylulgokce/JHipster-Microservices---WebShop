using MicroserviceCommon.CommonModel.Cart;

namespace CartService.Subscribers
{
    public interface ICartExchangeSubscriber
    {
        void OnAddProductToCart(SelectProductRequest selectProductRequest);
    }
}
