using RestaurantCorp.Adapters.Dtos;
using System.Configuration;
using System.Messaging;

namespace RestaurantCorp.Adapters
{
    public class ViewData : IViewData
    {
        public void SaveOrder(Order order)
        {
            MessageQueue msMq = null;

            msMq = new MessageQueue(ConfigurationManager.AppSettings["OrdersQueue"]);
            msMq.Send(order);
        }
    }
}
