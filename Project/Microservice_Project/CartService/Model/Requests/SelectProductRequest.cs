using MicroserviceCommon.Model;
using System.Runtime.Serialization;

namespace CartService.Model.Requests
{
    [DataContract]
    public class SelectProductRequest
    {
        public int IdCustomer { get; set; }
        public Product Product { get; set; }
    }
}
