using Newtonsoft.Json;
using Restaurants.Corp.Dal;
using Restaurants.Corp.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tiramisu
{
    class Program
    {
        static void Main(string[] args)
        {
            Setup();
        }

        private static void Setup()
        {
            const string orderQueue = @".\private$\RestaurantOrders";

            if (!MessageQueue.Exists(orderQueue))
                MessageQueue.Create(orderQueue);
            
            const string orderEvents = @".\private$\orderEvents";
            if (!MessageQueue.Exists(orderEvents))
                MessageQueue.Create(orderEvents);

            const string stockOrders = @".\private$\StockOrders";
            if (!MessageQueue.Exists(stockOrders))
                MessageQueue.Create(stockOrders);

            if (!Directory.Exists("c:\\temp"))
                Directory.CreateDirectory("c:\\temp");
        }
    }
}
