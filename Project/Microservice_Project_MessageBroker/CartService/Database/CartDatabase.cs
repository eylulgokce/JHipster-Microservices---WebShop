using System;
using CartService.Model;
using MicroserviceCommon.CommonModel.Cart;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace CartService.Database
{
    public class CartDatabase : ICartDatabase
    {
        private readonly ConcurrentDictionary<int, Cart> _carts = new ConcurrentDictionary<int, Cart>();

        private readonly ILogger<CartDatabase> _logger;
        
        public CartDatabase(ILogger<CartDatabase> logger)
        {
            _logger = logger;
        }

        private static int selectProductRequestNumber = 0;
        private static DateTime startTime;

        public void AddProductToCart(int idCustomer, SelectedProduct selectedProduct)
        {
            if (selectProductRequestNumber == 0)
            {
                startTime = DateTime.Now;
            }

            var cart = EnsureCustomerCartExists(idCustomer);
            cart.CustomerCart.Add(selectedProduct);
            _logger.LogInformation($"Product with ID {selectedProduct.IdProduct} and {selectedProduct.NumUnits} added to customer {idCustomer}'s cart!");

            selectProductRequestNumber++;
            var totalTime = (DateTime.Now - startTime).TotalSeconds;
            var averageTime = totalTime / selectProductRequestNumber;
            _logger.LogInformation($"Average time to process select product request: {averageTime:G2}s");
        }

        public Cart GetCostumerCart(int idCustomer)
        {
            return EnsureCustomerCartExists(idCustomer);
        }

        private Cart EnsureCustomerCartExists(int idCustomer)
        {
            if (_carts.ContainsKey(idCustomer))
            {
                return _carts[idCustomer];
            }

            var newCart = new Cart(idCustomer, new List<SelectedProduct>());
            _carts.TryAdd(idCustomer, newCart);
            return newCart;
        }
    }
}
