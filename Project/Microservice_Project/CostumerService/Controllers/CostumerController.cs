using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CostumerService.Database;
using Microsoft.AspNetCore.Mvc;


namespace CostumerService.Controllers
{
    [Route("costumer")]
    [ApiController]
    public class CostumerController : ControllerBase
    {
        private ICostumerDatabase _costumerDatabase;

        public CostumerController(ICostumerDatabase costumerDatabase)
        {
            _costumerDatabase = costumerDatabase;
        }

      
    }
}
