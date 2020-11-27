using System.Collections.Generic;
using CartService.Model;
using CartService.Model.Requests;
using Microsoft.AspNetCore.Mvc;
using ProductService.Model;

namespace CartService.Controllers
{
    [Route("myCart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private Cart cart;
        private Dictionary<int, Cart> carts;

        [HttpGet]
        public IEnumerable<Product> GetCostumerCart()
        {
            return cart.costumerCart;
        }

        [HttpPut]
        public IActionResult AddProductToCart([FromBody] SelectProductRequest request)
        {
            cart.costumerCart.Add(request.Product);

            return new OkObjectResult(null);
        }

    }
}
