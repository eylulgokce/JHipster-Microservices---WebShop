namespace MicroserviceCommon.Messages.Interfaces
{
    public interface IMessagePublisher
    {
        void PublishMessage(string topic, string message);
    }
}
