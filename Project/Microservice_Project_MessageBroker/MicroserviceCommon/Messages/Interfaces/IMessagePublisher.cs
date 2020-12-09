using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceCommon.Messages.Interfaces
{
    public interface IMessagePublisher
    {
        void PublishMessage(string topic, string message);
    }
}
