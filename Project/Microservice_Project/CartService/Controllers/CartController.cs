using System.Collections.Concurrent;
using System.Collections.Generic;
using CartService.Model;
using CartService.Model.Requests;
using MicroserviceCommon.Clients.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [Route("myCart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private static readonly ConcurrentDictionary<int, Cart> carts = new ConcurrentDictionary<int, Cart>();
        private readonly INotificationServiceClient _notificationServiceClient;

        public CartController(INotificationServiceClient notificationServiceClient)
        {
            _notificationServiceClient = notificationServiceClient;
        }
        
        [HttpGet]
        public IEnumerable<SelectedProduct> GetCostumerCart([FromQuery] int idCustomer)
        {
            var cart = EnsureCustomerCartExists(idCustomer);
            return cart.CustomerCart;
        }

        [HttpPut]
        public IActionResult AddProductToCart([FromBody] SelectProductRequest request)
        {
            var cart = EnsureCustomerCartExists(request.IdCustomer);
            cart.CustomerCart.Add(request.SelectedProduct);
            _notificationServiceClient.PublishNotificationInfo($"Product {request.SelectedProduct.IdProduct} Added to Cart!");
            return new OkObjectResult(null);
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
