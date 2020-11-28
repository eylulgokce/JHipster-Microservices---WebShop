using PaymentService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Database
{
    public interface IPaymentDatabase
    {
        void AddPayment(Payment payment);
        IEnumerable<Payment> ListAllPayments(int idCustomer);
    }
}
