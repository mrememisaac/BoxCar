using BoxCar.Integration.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Integration.MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(IntegrationBaseMessage message, string topicName);
    }
}
