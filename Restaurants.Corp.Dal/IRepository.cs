using Restaurants.Corp.Domain;
using System;
using System.Collections.Generic;

namespace Restaurants.Corp.Dal
{
    public interface IRepository
    {
        Restaurant GetRestaurantByName(string restuarantName);
        MenuItem GetMenuItemByName(string item);
        IList<Order> GetRecentOrdersForRestaurantByCorrelationId(Guid correlationId);
        Order GetOrderByCorrelationId(Guid guid);

        void AddOrder(Order newOrder);
        void Save();
        
    }
}
