namespace MicroserviceCommon.Clients.Interfaces
{
    public interface INotificationServiceClient
    {
        public void PublishNotification(string level, string message);
    }
}
