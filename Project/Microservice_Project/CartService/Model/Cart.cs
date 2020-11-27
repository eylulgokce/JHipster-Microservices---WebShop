using ProductService.Model;
using System.Collections.Generic;

namespace CartService.Model
{
    public class Cart
    {
        public Cart(int costumerID, List<Product> cart)
        {
            CostumerID = costumerID;
            costumerCart = cart;
        }

        public int CostumerID { get; set; }
        public List<Product> costumerCart { get; set; }
    }
}
