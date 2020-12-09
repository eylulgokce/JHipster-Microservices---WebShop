using System;

namespace MicroserviceCommon.Messages.Interfaces
{
    interface IMessageSubscriber
    {
        void Subscribe(string topic, Action<string> onReceivedMessageAction);
    }
}
