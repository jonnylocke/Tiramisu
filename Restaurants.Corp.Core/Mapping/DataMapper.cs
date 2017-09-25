using Restaurants.Corp.Dal;
using Restaurants.Corp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Corp.Core.Mapping
{
    public class DataMapper : IMapper
    {
        IRepository _repo;

        public DataMapper() : this (new OrderRepository())
        {

        }

        public DataMapper(IRepository repo)
        {
            _repo = repo;
        }

        public Order ToDomainOrder(RestaurantCorp.Adapters.Dtos.Order order)
        {
            var orderItems = new List<OrderItem>();
            if (order.Items != null)
            {
                foreach (var item in order.Items)
                {
                    var orderItem = new OrderItem
                    {
                        MenuItem = _repo.GetMenuItemByName(item).Id
                    };
                    orderItems.Add(orderItem);
                }
                var newOrder = new Order()
                {
                    RestaurantId = _repo.GetRestaurantByName(order.RestuarantName).Id,
                    Items = orderItems,
                    Created = DateTime.Now
                };

                return newOrder;
            }

            return null;
        }
    }
}
