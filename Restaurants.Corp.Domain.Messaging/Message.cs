using System;

namespace Restaurants.Corp.Domain.Messaging
{
    [Serializable]
    public class Message : IMessage
    {
        public Guid CorrelationId { get; set; }
        public string DataAsString { get; set; }
    }
}
