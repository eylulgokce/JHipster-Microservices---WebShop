using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;

namespace MicroserviceCommon.Rest
{
    public class RestApiClient
    {
        private readonly IRestClient _restClient;

        public RestApiClient(string apiEndpoint)
        {
            _restClient = new RestClient(apiEndpoint);
        }

        public RestResponse<TResponseType> PostRequestSimple<TResponseType>(string uri, object body) where TResponseType : class
        {
            var request = new RestRequest(uri, Method.POST);
            var jsonBody = JsonConvert.SerializeObject(body, Formatting.None);
            request.AddJsonBody(jsonBody);

            return InvokeWebRequest<TResponseType>(request);
        }

        private RestResponse<TResponseType> InvokeWebRequest<TResponseType>(IRestRequest request) where TResponseType : class
        {
            var completeUrl = "TODO";

            var result = new RestResponse<TResponseType>()
            {
                Uri = completeUrl,
                HttpMethod = request.Method
            };

            var requestJsonBody = request.Parameters?.FirstOrDefault(p => p.Type == ParameterType.RequestBody)?.Value as string;
            result.RawRequestBody = requestJsonBody;
            result.RequestParameters = request.Parameters;

            try
            {
                var traceSuffix = string.IsNullOrEmpty(requestJsonBody) ? "" : $" with {requestJsonBody}";

                var response = _restClient.Execute(request);
                result.StatusCode = response.StatusCode;
                result.ResponseHeaders = response.Headers;
                result.RawResponseBody = response.Content;

                if (response.ResponseStatus == ResponseStatus.Completed && response.IsSuccessful)
                {
                    if (typeof(TResponseType) == typeof(string))
                        result.Result = (TResponseType)(object)result.RawResponseBody;
                    else
                        result.Result = JsonConvert.DeserializeObject<TResponseType>(result.RawResponseBody);
                }
                else
                {
                    var msg = $"{request.Method} failure [{response.StatusCode}] on '{completeUrl}': {response.Content}";
                }

                result.IsSuccessful = response.IsSuccessful;
            }
            catch (Exception e)
            {
                result.Exception = e;
            }

            return result;
        }
    }
}
