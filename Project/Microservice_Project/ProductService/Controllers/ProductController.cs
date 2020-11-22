using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductService.Database;
using ProductService.Model;

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
    }
}
