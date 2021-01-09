using Newtonsoft.Json;
using OrderService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace OrderService
{
    [DataContract]
    public class Order
    {
        public Order() { }
        public Order(int costumerid, decimal totalprice, DateTime date)
        {
            IdCustomer = costumerid;
            Totalprice = totalprice;
            OrderDate = date;
        }

        public int IdCustomer { get; set; }

        public decimal Totalprice { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderToProduct> Products { get; set; }
    }
}
