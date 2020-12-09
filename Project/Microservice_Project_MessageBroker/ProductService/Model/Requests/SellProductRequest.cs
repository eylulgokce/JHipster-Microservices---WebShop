using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ProductService.Model.Requests
{
    [DataContract]
    public class SellProductRequest
    {
        public int IdProduct { get; set; }
        public int NumSoldUnits { get; set; }
    }
}
