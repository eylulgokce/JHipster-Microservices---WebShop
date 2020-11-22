using ProductService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Database
{
    public interface IProductDatabase
    {
        IEnumerable<Product> GetAllProducts();
    }
}
