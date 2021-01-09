using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MicroserviceCommon.Rest
{
    /// <summary>
    /// Wraps all useful information about invoked REST request.
    /// </summary>
    public class RestResponse<TResponseResult>
    {

        public string RawRequestBody { get; set; }

        public List<Parameter> RequestParameters { get; set; }

        public string Uri { get; set; }

        public Method HttpMethod { get; set; }

        public string RawResponseBody { get; set; }

        public bool IsSuccessful { get; set; }

        public Exception Exception { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public IList<Parameter> ResponseHeaders { get; set; }

        public TResponseResult Result { get; set; }
    }
}
