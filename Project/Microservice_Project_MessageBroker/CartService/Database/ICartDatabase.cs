using CartService.Model;
using MicroserviceCommon.CommonModel.Cart;

namespace CartService.Database
{
    public interface ICartDatabase
    {
        Cart GetCostumerCart(int idCustomer);
        void AddProductToCart(int idCustomer, SelectedProduct selectedProduct);
    }
}
