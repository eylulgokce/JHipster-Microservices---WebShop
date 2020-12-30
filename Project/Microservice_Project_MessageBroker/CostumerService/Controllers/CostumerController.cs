using CostumerService.Database;
using CostumerService.Model;
using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.ErrorHandling;
using Microsoft.AspNetCore.Mvc;


namespace CostumerService.Controllers
{
    [Route("costumer")]
    [ApiController]
    public class CostumerController : ControllerBase
    {
        private ICostumerDatabase _costumerDatabase;
        private readonly INotificationsManager _notificationServiceClient;

        public CostumerController(ICostumerDatabase costumerDatabase, INotificationsManager notificationServiceClient)
        {
            _costumerDatabase = costumerDatabase;
            _notificationServiceClient = notificationServiceClient;
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] Costumer costumer)
        {
            try
            {
                _costumerDatabase.insertCostumer(costumer.Firstname, costumer.Surname, costumer.Email, costumer.Address, costumer.City, costumer.Country);
                _notificationServiceClient.PublishNotificationInfo($"Customer {costumer.Firstname} {costumer.Surname} was successfully inserted!");
            }
            catch (BaseMicroserviceException ex)
            {
                return ex.ToActionResult();
            }

            return new OkResult();
        }

    }
}
