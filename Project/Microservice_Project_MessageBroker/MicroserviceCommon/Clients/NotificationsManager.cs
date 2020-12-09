using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.Configuration;
using MicroserviceCommon.Messages.Interfaces;
using MicroserviceCommon.Rest;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace MicroserviceCommon.Clients
{
    public class NotificationsManager : INotificationsManager
    {
        private const string LevelInfo = "info";

        private IMessagePublisher _messagePublisher;

        public NotificationsManager(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public void PublishNotificationInfo(string message)
        {
            var body = new JObject();
            body["level"] = LevelInfo;
            body["message"] = message;

            _messagePublisher.PublishMessage("notifications", body.ToString());
        }
    }
}
