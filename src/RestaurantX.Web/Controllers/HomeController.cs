using Newtonsoft.Json;
using RestaurantX.Web.Adapters;
using RestaurantX.Web.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace RestaurantX.Web.Controllers
{
    public class HomeController : Controller
    {
        IViewPresenter _viewPresenter;

        public HomeController() : this (new ViewPresenter())
        {

        }
        public HomeController(IViewPresenter viewPresenter)
        {
            _viewPresenter = viewPresenter;
        }

        public ActionResult Index()
        {
            var rnd = new Random();
            int pizzas = rnd.Next(1, 10);
            var vm = new OrderViewModel {
                Pizzas = pizzas
            };

            return View("Index", vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Reports()
        {
            var vm = _viewPresenter.GetIncomingStockReport();
            return View("Reports", vm);
        }

        [HttpPost]
        public ActionResult SubmitOrder(FormCollection form)
        {
            int numberOfTirmisus = 0;
            foreach (var key in form.AllKeys)
            {
                if (key.ToLower() == "order")
                {
                    int.TryParse(form[key], out numberOfTirmisus);
                    break;
                }
            }

            List<string> toAdd = new List<string>();
            for (int i = 0; i < numberOfTirmisus; i++)
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

            var rnd = new Random();
            int pizzas = rnd.Next(1, 10);
            var vm = new OrderViewModel
            {
                Pizzas = pizzas
            };

            return View("Index", vm);
        }
    }
}