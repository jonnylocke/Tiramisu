using System;
using System.Collections.Generic;

namespace Restaurants.Corp.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public Guid CorrelationId { get; set; }
        public DateTime Created { get; set; }

        public virtual List<OrderItem> Items { get; set; }
    }
}
