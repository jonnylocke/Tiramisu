using System;

namespace Restaurants.Corp.Domain.Messaging
{
    public interface IMessage
    {
        Guid CorrelationId { get; set; }
        string DataAsString { get; set; }
    }
}
