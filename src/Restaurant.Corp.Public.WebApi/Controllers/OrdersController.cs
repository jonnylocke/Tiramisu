using RestaurantCorp.Adapters;
using RestaurantCorp.Adapters.Dtos;
using System.Web.Http;

namespace Restaurant.Corp.Public.WebApi.Controllers
{
    public class OrdersController : ApiController
    {
        IViewData _viewData;

        internal OrdersController() : this (new ViewData())
        {

        }
        public OrdersController(IViewData viewData) 
        {
            _viewData = viewData;
        }

        public IHttpActionResult Post([FromBody]Order order)
        {
            _viewData.SaveOrder(order);
            return Ok("Order Accepted");
        }
    }
}
