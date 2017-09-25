using Restaurants.Corp.Core.Mapping;
using Restaurants.Corp.Dal;
using System;
using System.Configuration;
using System.Messaging;
using Restaurants.Corp.Domain.Messaging;

namespace Restaurants.Corp.Domain.Orders
{
    public class OrdersService
    {
        public void Start()
        {
            ProcessOrders();
        }

        public void Stop()
        {
        }

        

        private void ProcessOrders()
        {
            var queuePath = ConfigurationManager.AppSettings["OrdersQueue"];
            MessageQueue queue = new MessageQueue(queuePath);
            queue.ReceiveCompleted += QueueMessageReceived;
            queue.BeginReceive();
        }

        public void QueueMessageReceived(object source, ReceiveCompletedEventArgs e)
        {
            MessageQueue sourceQueue = null;
            try
            {
                sourceQueue = (MessageQueue)source;
                sourceQueue.Formatter = new XmlMessageFormatter(new Type[]
                {
                    typeof(RestaurantCorp.Adapters.Dtos.Order)
                });

                var message = (RestaurantCorp.Adapters.Dtos.Order)sourceQueue.EndReceive(e.AsyncResult).Body;
                var mapper = new DataMapper();
                var newOrder = mapper.ToDomainOrder(message);
                newOrder.CorrelationId = Guid.NewGuid();
                HandleMessage(newOrder, new OrderRepository());

                //TODO: move this to listen for data changes and then raise events based on, 
                // e.g. like SqlCacheDependency, EventStore, SNS etc
                RaiseEvent(newOrder, new EventPublisher());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sourceQueue.BeginReceive();
            }
        }

        private void RaiseEvent(Order newOrder, IPublisher eventPublisher)
        {
            eventPublisher.RaiseEvent(newOrder.CorrelationId, ConfigurationManager.AppSettings["OrderEvents"]);
        }

        private void HandleMessage(Order order, IRepository repo)
        {
            if (order != null)
            {
                try
                {
                    repo.AddOrder(order);
                    repo.Save();

                    Console.WriteLine($"Order Saved: '{order.CorrelationId.ToString()}'");
                }
                catch (Exception ex)
                {
                    // TODO: log and send to the error queue
                }
            }
        }
    }
}
