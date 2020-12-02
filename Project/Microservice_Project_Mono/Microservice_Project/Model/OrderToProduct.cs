using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OrderService.Model
{
    [DataContract]
    public class OrderToProduct
    {
        public OrderToProduct() { }
        public OrderToProduct(int idOrder, int idProduct, int numBoughtUnits)
        {
            IdOrder = idOrder;
            IdProduct = idProduct;
            NumBoughtUnits = numBoughtUnits;
        }

        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public int NumBoughtUnits { get; set; }
    }
}
