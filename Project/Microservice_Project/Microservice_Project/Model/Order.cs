using OrderService.Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OrderService
{
    [DataContract]
    public class Order
    {
        public Order(int costumerid, decimal totalprice, DateTime date)
        {
            Costumerid = costumerid;
            Totalprice = totalprice;
            OrderDate = date;
        }

        public int Costumerid { get; set; }
        public decimal Totalprice { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderToProduct> OrderToProducts { get; set; }
    }
}
