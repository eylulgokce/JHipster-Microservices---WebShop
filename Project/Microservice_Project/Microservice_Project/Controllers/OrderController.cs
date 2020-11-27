using MicroserviceCommon.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using OrderService.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderService.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderDatabase _orderDatabase;

        public OrderController(IOrderDatabase orderDatabase)
        {
            _orderDatabase = orderDatabase;
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            try
            {
                _orderDatabase.AddOrder(order);
            }
            catch(BaseMicroserviceException ex)
            {
                return ex.ToActionResult();
            }

            return new AcceptedResult();
        }
    }
}
