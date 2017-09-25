using Restaurants.Corp.Domain;
using System.Collections.Generic;

namespace Restaurants.Corp.Core.Models
{
    public class RestaurantOrder
    {
        public int RestuarantId { get; set; }
        public List<Order> Orders { get; set; }
    }
}
