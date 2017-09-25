using Restaurants.Corp.Core.BusinessLogic.Concrete;
using Restaurants.Corp.Core.Restaurants;
using Restaurants.Corp.Dal;
using Restaurants.Corp.Domain.Messaging;
using System;
using System.Configuration;
using System.Messaging;

namespace Stock.Supplier.Dispatcher
{
    public class WarehouseDispatcher
    {
        public void Start()
        {
            GetStockOrders();
        }

        public void Stop()
        {
        }


        private void GetStockOrders()
        {
            var queuePath = ConfigurationManager.AppSettings["StockOrders"];
            MessageQueue queue = new MessageQueue(queuePath);
            queue.ReceiveCompleted += QueueMessageReceived;
            queue.BeginReceive();
        }

        private void QueueMessageReceived(object source, ReceiveCompletedEventArgs e)
        {

            MessageQueue sourceQueue = null;
            try
            {
                sourceQueue = (MessageQueue)source;
                sourceQueue.Formatter = new XmlMessageFormatter(new Type[]
                {
                    typeof(Restaurants.Corp.Domain.Messaging.Message)
                });

                var message = (Restaurants.Corp.Domain.Messaging.Message)sourceQueue.EndReceive(e.AsyncResult).Body;

                HandleMessage(message);

            }
            catch (Exception ex)
            {
            }
            finally
            {
                sourceQueue.BeginReceive();
            }
        }

        private void HandleMessage(IMessage message)
        {
            var notification = new StockControlNotification(new OrderRepository(), new StockQuantityControl())
            {
                Restaurant = new RestaurantX()
            };
            notification.Update(message);
        }
    }
}