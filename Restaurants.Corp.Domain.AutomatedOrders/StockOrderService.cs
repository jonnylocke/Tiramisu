using Restaurants.Corp.Domain.Messaging;
using System.Configuration;
using System.Messaging;
using System;
using Restaurants.Corp.Dal;
using Restaurants.Corp.Core.BusinessLogic.Concrete;

namespace Restaurants.Corp.Domain.StockOrders
{
    public class StockOrderService
    {
        public void Start()
        {
            GetOrdersTaken();
        }

        public void Stop()
        {
        }

        private void GetOrdersTaken()
        {
            var queuePath = ConfigurationManager.AppSettings["OrderEvents"];
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
                    typeof(Messaging.Message)
                });

                var message = (Messaging.Message)sourceQueue.EndReceive(e.AsyncResult).Body;
                
                HandleMessage(message, new OrderRepository());

            }
            catch (Exception ex)
            {
            }
            finally
            {
                sourceQueue.BeginReceive();
            }
        }

        private void HandleMessage(IMessage message, IRepository repo)
        {
            var stockOrderingProcess = new StockOrderingProcess(new OrderRepository(), new StockQuantityControl(), new EventPublisher())
            {
                OrderMechanism = new ManualOrder()
            };
            stockOrderingProcess.UpdateStock(message);
        }
    }
}
