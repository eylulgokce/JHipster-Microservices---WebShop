using ProductService.Model;
using System.Collections.Generic;

namespace ProductService.Database
{
    public interface IProductDatabase
    {
        IEnumerable<Product> GetAllProducts();
        void SellProduct(int idProduct, int numSoldUnits);
    }
}
