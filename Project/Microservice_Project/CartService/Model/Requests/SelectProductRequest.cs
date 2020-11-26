using ProductService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CartService.Model.Requests
{
    [DataContract]
    public class SelectProductRequest
    {
        public int IdCustomer { get; set; }
        public Product Product { get; set; }

    }
}
