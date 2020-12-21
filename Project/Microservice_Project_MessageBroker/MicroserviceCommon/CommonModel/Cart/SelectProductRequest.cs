using System.Runtime.Serialization;

namespace MicroserviceCommon.CommonModel.Cart
{
    [DataContract]
    public class SelectProductRequest
    {
        public SelectProductRequest() {}

        [DataMember(Name="idCustomer")]
        public int IdCustomer { get; set; }

        [DataMember(Name = "selectedProduct")]
        public SelectedProduct SelectedProduct { get; set; }
    }
}
