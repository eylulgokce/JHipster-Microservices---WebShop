using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MicroserviceCommon.CommonModel.Order
{
    [DataContract]
    public class Order
    {
        public Order() { }
        public Order(int customerId, decimal totalPrice, DateTime date)
        {
            IdCustomer = customerId;
            TotalPrice = totalPrice;
            OrderDate = date;
        }

        [DataMember(Name="idCustomer")]
        public int IdCustomer { get; set; }

        [DataMember(Name = "totalPrice")]
        public decimal TotalPrice { get; set; }

        [DataMember(Name = "orderDate")]
        public DateTime OrderDate { get; set; }

        [DataMember(Name = "products")]
        public List<OrderToProduct> Products { get; set; }
    }
}
