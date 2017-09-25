using Restaurants.Corp.Core.BusinessLogic.Interfaces;
using System.Collections.Generic;
using Restaurants.Corp.Core.Dtos;
using Restaurants.Corp.Domain;

namespace Restaurants.Corp.Core.BusinessLogic.Concrete
{
    public class StockQuantityControl : IStockQuantityControl
    {
        public RestaurantStock GetStockItems(IList<Order> orders)
        {
            var stock = new RestaurantStock();

            var tiramisuTotal = GetTotalNumberOfTiramisuOrdered(orders);
            stock.Eggs = tiramisuTotal * 3;

            return stock;
        }





        private int GetTotalNumberOfTiramisuOrdered(IList<Order> orders)
        {
            int total = 0;
            foreach (var order in orders)
            {
                foreach (var orderItem in order.Items)
                {
                    if (orderItem.MenuItem == 3)
                        total += 1;
                }
            }

            return total;
        }
    }
}
