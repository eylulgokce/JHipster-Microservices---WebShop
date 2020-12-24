using System.Runtime.Serialization;

namespace ProductService.Model.Requests
{
    [DataContract]
    public class SellProductRequest
    {
        public int IdProduct { get; set; }
        public int NumSoldUnits { get; set; }
    }
}
