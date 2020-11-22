using System;
using System.Runtime.Serialization;

namespace OrderService
{
    [DataContract]
    public class Order
    {
        public Order(int costumerid, int products, decimal totalprice, DateTime date)
        {
            Costumerid = costumerid;
            Products = products;
            Totalprice = totalprice;
            OrderDate = date;
        }

        public int Costumerid { get; set; }
        public int Products { get; set; }
        public decimal Totalprice { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
