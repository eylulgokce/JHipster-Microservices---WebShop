using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.Configuration;
using MicroserviceCommon.Rest;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace MicroserviceCommon.Clients
{
    public class NotificationServiceClient : INotificationServiceClient
    {
        private const string LevelInfo = "info";

        private string _endpoint;

        public NotificationServiceClient(IOptions<NotificationConfiguration> notificationConfiguration)
        {
            _endpoint = notificationConfiguration.Value.Endpoint;
        }

        public void PublishNotificationInfo(string message)
        {
            var client = new RestApiClient(_endpoint);
            var body = new JObject();
            body["level"] = LevelInfo;
            body["message"] = message;

            var response = client.PostRequestSimple<string>("notifications", body);
            int a = 4;
        }
    }
}
