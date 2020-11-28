using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Model
{
    public class Payment
    {
        public Payment() { }
        public Payment(int idCostumer, string paymentMethod)
        {
            IdCostumer = idCostumer;
            PaymentMethod = paymentMethod;
        }

        public int IdCostumer { get; set; }
        public string PaymentMethod { get; set; }
    }
}
