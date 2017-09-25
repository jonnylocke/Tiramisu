using System;
using System.Collections.Generic;
using Restaurants.Corp.Dal;
using Restaurants.Corp.Domain.Messaging;
using Restaurants.Corp.Core.BusinessLogic.Interfaces;
using System.Configuration;

namespace Restaurants.Corp.Domain.StockOrders
{
    public class StockOrderingProcess
    {
        private IRepository _repo;
        private IStockQuantityControl _stockQuantityControl;
        IPublisher _eventPublisher;

        public StockOrderingProcess(IRepository repo, IStockQuantityControl stockQuantityControl, IPublisher eventPublisher)
        {
            _repo = repo;
            _stockQuantityControl = stockQuantityControl;
            _eventPublisher = eventPublisher;
        }

        public IStockOrderMechanism OrderMechanism { get; set; }

        public void UpdateStock(IMessage message)
        {
            Guid correlationId;
            Guid.TryParse(message.CorrelationId.ToString(), out correlationId);
            var order = _repo.GetOrderByCorrelationId(message.CorrelationId);

            if (order != null)
            {
                var stock = _stockQuantityControl.GetStockItems(new List<Order> { order });
                OrderMechanism.PlaceStockOrder(stock);

                // in reality this event would not be done here but raised when the suppliers ships the stock
                // it is here for the purposes of the demo and assuming that the stock IS shipped by the supplier
                _eventPublisher.RaiseEvent(order.CorrelationId, "ManualOrder", ConfigurationManager.AppSettings["StockOrders"]);
            }
        }




        

        
    }
}
