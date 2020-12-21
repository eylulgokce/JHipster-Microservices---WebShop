using CartService.Model;
using MicroserviceCommon.CommonModel.Cart;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CartService.Database
{
    public class CartDatabase : ICartDatabase
    {
        private static readonly ConcurrentDictionary<int, Cart> carts = new ConcurrentDictionary<int, Cart>();

        public void AddProductToCart(int idCustomer, SelectedProduct selectedProduct)
        {
            var cart = EnsureCustomerCartExists(idCustomer);
            cart.CustomerCart.Add(selectedProduct);
        }

        public Cart GetCostumerCart(int idCustomer)
        {
            return EnsureCustomerCartExists(idCustomer);
        }

        private Cart EnsureCustomerCartExists(int idCustomer)
        {
            if (carts.ContainsKey(idCustomer))
            {
                return carts[idCustomer];
            }

            var newCart = new Cart(idCustomer, new List<SelectedProduct>());
            carts.TryAdd(idCustomer, newCart);
            return newCart;
        }
    }
}
