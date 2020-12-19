using MicroserviceCommon.Messages.Interfaces;

namespace MicroserviceCommon.Messages
{
    public class RabbitMessagePublisher : IMessagePublisher
    {
        public void PublishMessage(string topic, string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
