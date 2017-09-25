using Restaurants.Corp.Domain;
using System.Data.Entity;

namespace Restaurants.Corp.Dal
{
    public class RestaurantCorpContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
    }
}
