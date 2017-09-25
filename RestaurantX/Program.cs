using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace RestaurantX
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Place Orders here for Restaurant X" + Environment.NewLine);
            Console.Write(Environment.NewLine);
            Console.Write("Enter a number of Tiramisu's: ");
            int numberOfDeserts = int.Parse(Console.ReadLine());

            List<string> toAdd = new List<string>();
            for (int i = 0; i < numberOfDeserts; i++)
            {
                toAdd.Add("Tiramisu");
            }
            var data = new RestaurantCorp.Adapters.Dtos.Order
            {
                RestuarantName = "RestaurantX",
                Items = toAdd
            };
            var dataString = JsonConvert.SerializeObject(data);

            string response;
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                response = client.UploadString(new Uri("http://localhost:53406/api/orders"), "POST", dataString);
            }
            Console.WriteLine(response);
            Console.ReadKey();
        }
    }
}
