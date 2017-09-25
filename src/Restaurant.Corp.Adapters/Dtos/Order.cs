using System.Collections.Generic;

namespace RestaurantCorp.Adapters.Dtos
{
    public class Order
    {
        public string RestuarantName { get; set; }
        public List<string> Items { get; set; }
    }
}
