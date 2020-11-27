using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace MicroserviceCommon.ErrorHandling
{
    [DataContract]
    public class MicroserviceExceptionResponse
    {
        public MicroserviceExceptionResponse(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }

    public class BaseMicroserviceException : Exception
    {
        public BaseMicroserviceException(HttpStatusCode httpStatusCode, string message) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        public IActionResult ToActionResult()
        {
            var response = new MicroserviceExceptionResponse(Message);

            if (HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return new BadRequestObjectResult(response);
            }
            else if (HttpStatusCode == HttpStatusCode.NotFound)
            {
                return new NotFoundObjectResult(response);
            }

            throw new Exception("Unknown error type!");
        }
    }

    public class BadRequestMicroserviceException : BaseMicroserviceException
    {
        public BadRequestMicroserviceException(string message) : base(HttpStatusCode.BadRequest, message)
        {
        }
    }

    public class NotFoundMicroserviceException : BaseMicroserviceException
    {
        public NotFoundMicroserviceException(string message) : base(HttpStatusCode.NotFound, message)
        {
        }
    }
}
