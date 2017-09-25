using Restaurants.Corp.Core.Dtos;

namespace Restaurants.Corp.Domain.StockOrders
{
    public interface IStockOrderMechanism
    {
        void PlaceStockOrder(RestaurantStock stock);
    }
}
