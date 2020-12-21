using System.Collections.Generic;
using CartService.Database;
using MicroserviceCommon.CommonModel.Cart;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [Route("myCart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartDatabase _cartDatabase;

        public CartController(ICartDatabase cartDatabase)
        {
            _cartDatabase = cartDatabase;
        }

        [HttpGet]
        public IEnumerable<SelectedProduct> GetCostumerCart([FromQuery] int idCustomer)
        {
            return _cartDatabase.GetCostumerCart(idCustomer).CustomerCart;
        }

        [HttpPut]
        public IActionResult AddProductToCart([FromBody] SelectProductRequest request)
        {
            _cartDatabase.AddProductToCart(request.IdCustomer, request.SelectedProduct);
            return new OkObjectResult(null);
        }
    }
}
