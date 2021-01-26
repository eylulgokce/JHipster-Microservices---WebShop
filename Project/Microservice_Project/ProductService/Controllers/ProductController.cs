using System;
using System.Collections.Generic;
using MicroserviceCommon.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductService.Database;
using ProductService.Model.Requests;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("products")] 
    public class ProductController : Controller
    {
        private readonly ILogger _logger;
        private IProductDatabase _productDatabase;

        public ProductController(ILogger<ProductController> logger, IProductDatabase productDatabase)
        {
            _logger = logger;
            _productDatabase = productDatabase;
        }

        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            _logger.LogInformation("Getting all products...");
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
