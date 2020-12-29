using System.Collections.Generic;
using MicroserviceCommon.CommonModel;

namespace PaymentService.Database
{
    public interface IPaymentDatabase
    {
        void AddPayment(Payment payment);
        IEnumerable<Payment> ListAllPayments(int idCustomer);
    }
}
