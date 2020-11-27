using MicroserviceCommon.Model;
using System.Collections.Generic;

namespace CartService.Model
{
    public class Cart
    {
        public Cart(int customerId, List<Product> cart)
        {
            CustomerId = customerId;
            CustomerCart = cart;
        }

        public int CustomerId { get; set; }
        public List<Product> CustomerCart { get; set; }
    }
}
