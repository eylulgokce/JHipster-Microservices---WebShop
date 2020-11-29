using System.Collections.Generic;

namespace CartService.Model
{
    public class Cart
    {
        public Cart(int customerId, List<SelectedProduct> cart)
        {
            CustomerId = customerId;
            CustomerCart = cart;
        }

        public int CustomerId { get; set; }
        public List<SelectedProduct> CustomerCart { get; set; }
    }
}
