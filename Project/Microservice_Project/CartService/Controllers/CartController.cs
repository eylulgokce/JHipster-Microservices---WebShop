using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartService.Model;
using CartService.Model.Requests;
using Microsoft.AspNetCore.Mvc;
using ProductService.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CartService.Controllers
{
    [Route("myCart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private Cart cart;

        [HttpGet]
        public IEnumerable<Product> GetCostumerCart()
        {
            return cart.costumerCart;
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPut]
        public IActionResult AddProductToCart([FromBody] SelectProductRequest request)
        {
            cart.costumerCart.Add(request.product);

            return new OkObjectResult(null);
        }

    }
}
