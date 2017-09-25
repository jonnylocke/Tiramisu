using System.Collections.Generic;
using Restaurants.Corp.Domain;
using Restaurants.Corp.Core.Dtos;

namespace Restaurants.Corp.Core.BusinessLogic.Interfaces
{
    public interface IStockQuantityControl
    {
        RestaurantStock GetStockItems(IList<Order> orders);
    }
}
