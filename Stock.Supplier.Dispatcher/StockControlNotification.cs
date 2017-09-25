using System;
using Restaurants.Corp.Core.BusinessLogic.Interfaces;
using Restaurants.Corp.Dal;
using Restaurants.Corp.Domain.Messaging;
using System.Collections.Generic;
using Restaurants.Corp.Domain;
using System.Linq;

namespace Stock.Supplier.Dispatcher
{
    public class StockControlNotification
    {
        private IRepository _repo;
        private IStockQuantityControl _stockQuantityControl;

        public StockControlNotification(IRepository repo, IStockQuantityControl stockQuantityControl)
        {
            _repo = repo;
            _stockQuantityControl = stockQuantityControl;
        }

        public IRestaurant Restaurant { get; set; }

        public void Update(IMessage message)
        {
            Guid correlationId;
            Guid.TryParse(message.CorrelationId.ToString(), out correlationId);

            if (!string.IsNullOrWhiteSpace(message.DataAsString) 
                && message.DataAsString.ToLower() == "manualorder")
            {
                var orders = GetLastMonthsOrdersForRestaurantX(correlationId, _repo);

                if (orders != null)
                {
                    var stock = _stockQuantityControl.GetStockItems(orders);
                    Restaurant.SetStockQuantity(stock);
                    Console.WriteLine("Restaurant notified of stock shipped");
                }
            }
        }



        private IList<Order> GetLastMonthsOrdersForRestaurantX(Guid correlationId, IRepository repo)
        {
            var result = repo.GetRecentOrdersForRestaurantByCorrelationId(correlationId);

            if (HasOneMonthOfOrders(result.Count))
                return LastMonthsOrders(result);

            return null;
        }

        private IList<Order> LastMonthsOrders(IList<Order> orders)
        {
            var orderByDate = orders.OrderByDescending(x => x.Created);
            return orderByDate.Take(5).ToList();
        }

        private bool HasOneMonthOfOrders(int numberOfOrders)
        {
            const int aFixedNumberToFakeAMonthsWorthOfData = 5;
            return (numberOfOrders % aFixedNumberToFakeAMonthsWorthOfData) == 0;
        }
    }
}