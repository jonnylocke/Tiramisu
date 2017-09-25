using Restaurant.Corp.Adapters.Dtos;

namespace RestaurantCorp.Adapters
{
    public interface IViewPresenter
    {
        StockReportModel GetStockItemToOrder();
    }
}
