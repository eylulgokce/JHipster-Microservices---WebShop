using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Database;
using PaymentService.Model;

namespace PaymentService.Controllers
{
    [Route("payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentDatabase _paymentDatabase;
        private readonly INotificationsManager _notificationServiceClient;

        public PaymentController(IPaymentDatabase paymentDatabase, INotificationsManager notificationServiceClient)
        {
            _paymentDatabase = paymentDatabase;
            _notificationServiceClient = notificationServiceClient;
        }

        [HttpPost]
        public IActionResult AddPayment([FromBody] Payment payment)
        {
            try
            {
                _paymentDatabase.AddPayment(payment);
                _notificationServiceClient.PublishNotificationInfo($"Payment for order {payment.IdOrder} was successfully processed!");
            }
            catch(BaseMicroserviceException ex)
            {
                return ex.ToActionResult();
            }

            return new OkResult();
        }
    }
}
