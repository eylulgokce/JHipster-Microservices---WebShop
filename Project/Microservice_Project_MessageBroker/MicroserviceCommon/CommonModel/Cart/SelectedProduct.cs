using System.Runtime.Serialization;

namespace MicroserviceCommon.CommonModel.Cart
{
    [DataContract]
    public class SelectedProduct
    {
        public SelectedProduct() { }

        public SelectedProduct(int idProduct, int numUnits)
        {
            IdProduct = idProduct;
            NumUnits = numUnits;
        }

        [DataMember(Name = "idProduct")]
        public int IdProduct { get; set; }

        [DataMember(Name = "numUnits")]
        public int NumUnits { get; set; }
    }
}
