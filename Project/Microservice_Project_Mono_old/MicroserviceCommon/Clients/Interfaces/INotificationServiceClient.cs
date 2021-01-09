namespace MicroserviceCommon.Clients.Interfaces
{
    public interface INotificationServiceClient
    {
        public void PublishNotificationInfo(string message);
    }
}
