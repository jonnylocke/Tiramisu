using RestaurantCorp.Adapters;
using System.Web.Mvc;

namespace Restaurant.Corp.Reports.Controllers
{
    public class HomeController : Controller
    {
        IViewPresenter _viewPresenter;

        public HomeController() : this(new ViewPresenter())
        {

        }
        public HomeController(IViewPresenter viewPresenter)
        {
            _viewPresenter = viewPresenter;
        }

        public ActionResult Index()
        {
            return View();
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
            var vm =  _viewPresenter.GetStockItemToOrder();
            return View("Reports", vm);
        }
    }
}