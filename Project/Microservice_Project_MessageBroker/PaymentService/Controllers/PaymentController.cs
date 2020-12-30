using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.CommonModel;
using MicroserviceCommon.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Database;

namespace PaymentService.Controllers
{
    [Route("payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentDatabase _paymentDatabase;
        private readonly INotificationsManager _notificationsManager;

        public PaymentController(IPaymentDatabase paymentDatabase, INotificationsManager notificationsManager)
        {
            _paymentDatabase = paymentDatabase;
            _notificationsManager = notificationsManager;
        }

        [HttpPost]
        public IActionResult AddPayment([FromBody] Payment payment)
        {
            try
            {
                _paymentDatabase.AddPayment(payment);
                _notificationsManager.PublishNotificationInfo($"Payment for order {payment.IdOrder} was successfully processed!");
            }
            catch(BaseMicroserviceException ex)
            {
                return ex.ToActionResult();
            }

            return new OkResult();
        }
    }
}
