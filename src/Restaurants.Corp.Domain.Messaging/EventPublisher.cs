using System;
using System.Messaging;

namespace Restaurants.Corp.Domain.Messaging
{
    public class EventPublisher : IPublisher
    {
        public void RaiseEvent(Guid correlationId, string queue)
        {
            MessageQueue msMq = null;
            var message = new Message()
            {
                CorrelationId = correlationId
            };

            msMq = new MessageQueue(queue);
            msMq.Send(message);
        }

        public void RaiseEvent(Guid correlationId, string data, string queue)
        {
            MessageQueue msMq = null;
            var message = new Message()
            {
                CorrelationId = correlationId,
                DataAsString = data
            };

            msMq = new MessageQueue(queue);
            msMq.Send(message);
        }
    }

    public interface IPublisher
    {
        void RaiseEvent(Guid correlationId, string queue);
        void RaiseEvent(Guid correlationId, string data, string queue);
    }
}
