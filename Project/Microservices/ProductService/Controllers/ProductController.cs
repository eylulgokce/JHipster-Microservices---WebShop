using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductService.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Controllers
{
    [Route("products")] // localhost:5000/abcde
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductDatabase _productDatabase;

        public ProductController(IProductDatabase productDatabase)
        {
            _productDatabase = productDatabase;
        }

        /*
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return _productDatabase.GetAllProducts();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }

}
