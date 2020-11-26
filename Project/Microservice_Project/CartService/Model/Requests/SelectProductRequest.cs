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
        public Product product { get; set; }

    }
}
