using MicroserviceCommon.Clients.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceCommon.Clients
{
    public class NotificationServiceClient : INotificationServiceClient
    {
        public void PublishNotification(string level, string message)
        {
            throw new NotImplementedException();
        }
    }
}
