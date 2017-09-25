using System;
using System.Linq;
using Restaurants.Corp.Domain;
using System.Collections.Generic;

namespace Restaurants.Corp.Dal
{
    public class OrderRepository : IRepository
    {
        private RestaurantCorpContext dbContext = new RestaurantCorpContext();

        public MenuItem GetMenuItemByName(string item)
        {
            var items = dbContext.MenuItems;

            return items.First(x => x.Name.ToLower() == item.ToLower());
        }

        public Restaurant GetRestaurantByName(string restuarantName)
        {
            return dbContext.Restaurants.FirstOrDefault(x => x.Name.ToLower() == restuarantName.ToLower());
        }

        public void AddOrder(Order newOrder)
        {
            dbContext.Orders.Add(newOrder);
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public IList<Order> GetRecentOrdersForRestaurantByCorrelationId(Guid correlationId)
        {
            var restaurantId = dbContext.Orders.Where(x => x.CorrelationId == correlationId).FirstOrDefault().RestaurantId;
            var result = dbContext.Orders.Where(x => x.RestaurantId == restaurantId).ToList();

            return result;

        }

        public Order GetOrderByCorrelationId(Guid correlationId)
        {
            return dbContext.Orders.Where(x => x.CorrelationId == correlationId).FirstOrDefault();
        }
    }
}
