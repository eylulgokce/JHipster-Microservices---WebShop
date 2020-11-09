using ProductService.Model;
using System.Collections.Generic;

namespace ProductService.Database
{
    public class FakeProductDatabase : IProductDatabase
    {
        public IEnumerable<Product> GetAllProducts()
        {
            return new List<Product> { new Product("Ferrari Fur Kinder", "Ein tolles Auto fur Kinder", 10.99M, 17) };
        }
    }
}
