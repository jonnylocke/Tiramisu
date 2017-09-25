using Restaurants.Corp.Core.Dtos;
using System.IO;
using Newtonsoft.Json;

namespace Restaurants.Corp.Domain.StockOrders
{
    public class ManualOrder : IStockOrderMechanism
    {
        public void PlaceStockOrder(RestaurantStock stock)
        {
            var path = "c:\\temp\\stockorder.json";

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, stock);
            }
        }
    }
}
