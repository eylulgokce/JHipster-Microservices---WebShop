using PaymentService.Model;
using System.Collections.Generic;

namespace PaymentService.Database
{
    public interface IPaymentDatabase
    {
        void AddPayment(Payment payment);
        IEnumerable<Payment> ListAllPayments(int idCustomer);
    }
}
