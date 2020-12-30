using CostumerService.Database;
using CostumerService.Model;
using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.ErrorHandling;
using Microsoft.AspNetCore.Mvc;


namespace CostumerService.Controllers
{
    [Route("customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerDatabase _customerDatabase;
        private readonly INotificationServiceClient _notificationServiceClient;

        public CustomerController(ICustomerDatabase customerDatabase, INotificationServiceClient notificationServiceClient)
        {
            _customerDatabase = customerDatabase;
            _notificationServiceClient = notificationServiceClient;
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer costumer)
        {
            try
            {
                _customerDatabase.InsertCustomer(costumer.FirstName, costumer.Surname, costumer.Email, costumer.Address, costumer.City, costumer.Country);
                _notificationServiceClient.PublishNotificationInfo($"Customer {costumer.FirstName} {costumer.Surname} was successfully inserted!");
            }
            catch (BaseMicroserviceException ex)
            {
                return ex.ToActionResult();
            }

            return new OkResult();
        }
    }
}
