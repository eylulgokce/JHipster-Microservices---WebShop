using System.Collections.Generic;
using CartService.Model;
using CartService.Model.Requests;
using MicroserviceCommon.Configuration;
using MicroserviceCommon.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CartService.Controllers
{
    [Route("myCart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly Dictionary<int, Cart> carts;

        public CartController(IOptions<NotificationConfiguration> config)
        {
            carts = new Dictionary<int, Cart>();
        }
        
        [HttpGet]
        public IEnumerable<Product> GetCostumerCart([FromQuery] int idCustomer)
        {
            var cart = EnsureCustomerCartExists(idCustomer);
            return cart.CustomerCart;
        }

        [HttpPut]
        public IActionResult AddProductToCart([FromBody] SelectProductRequest request)
        {
            var cart = EnsureCustomerCartExists(request.IdCustomer);
            cart.CustomerCart.Add(request.Product);

            return new OkObjectResult(null);
        }

        private Cart EnsureCustomerCartExists(int idCustomer)
        {
            if (carts.ContainsKey(idCustomer))
            {
                return carts[idCustomer];
            }

            var newCart = new Cart(idCustomer, new List<Product>());
            carts.Add(idCustomer, newCart);
            return newCart;
        }

    }
}
