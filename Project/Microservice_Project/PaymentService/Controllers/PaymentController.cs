using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Database;

namespace PaymentService.Controllers
{
    [Route("payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IPaymentDatabase _paymentDatabase;

        public PaymentController(IPaymentDatabase paymentDatabase)
        {
            _paymentDatabase = paymentDatabase;
        }


    }
}
