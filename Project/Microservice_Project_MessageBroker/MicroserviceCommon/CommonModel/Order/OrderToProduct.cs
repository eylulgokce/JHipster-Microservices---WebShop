using System.Runtime.Serialization;

namespace MicroserviceCommon.CommonModel.Order
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

        [DataMember(Name="idOrder")]
        public int IdOrder { get; set; }
        
        [DataMember(Name="idProduct")]
        public int IdProduct { get; set; }
        
        [DataMember(Name="numBoughtUnits")]
        public int NumBoughtUnits { get; set; }
    }
}
