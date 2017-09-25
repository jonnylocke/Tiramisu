using Restaurants.Corp.Core.BusinessLogic.Interfaces;
using Restaurants.Corp.Core.Dtos;
using System.IO;
using Newtonsoft.Json;

namespace Restaurants.Corp.Core.Restaurants
{
    public class RestaurantX : IRestaurant
    {
        public RestaurantX()
        {
        }

        public void SetStockQuantity(RestaurantStock stock)
        {
            var path = "c:\\temp\\stockshipped.json";

            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, stock);
            }
        }
    }
}