using System;

namespace Restaurants.Corp.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int MenuItem { get; set; }
        public Order OrderId { get; set; }
    }
}
