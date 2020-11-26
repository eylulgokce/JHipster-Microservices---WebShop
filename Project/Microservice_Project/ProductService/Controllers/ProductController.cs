using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductService.Database;
using ProductService.Model;
using ProductService.Model.Requests;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("products")] // localhost:5000/abcde
    public class ProductController : Controller
    {
        private IProductDatabase _productDatabase;

        public ProductController(IProductDatabase productDatabase)
        {
            _productDatabase = productDatabase;
        }

        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return _productDatabase.GetAllProducts();
        }

        [HttpPut]
        public IActionResult SellProducts([FromBody] SellProductRequest request)
        {
            try
            {
                _productDatabase.SellProduct(request.IdProduct, request.NumSoldUnits);
            }
            catch(Exception ex)
            {
                return new ConflictObjectResult(ex.Message);
            }

            return new OkObjectResult(null);
        }
    }
}
