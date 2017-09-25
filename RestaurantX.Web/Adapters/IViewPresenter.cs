using Restaurant.Corp.Adapters.Dtos;

namespace RestaurantX.Web.Adapters
{
    public interface IViewPresenter
    {
        StockReportModel GetIncomingStockReport();
    }
}
