using RestaurantCorp.Adapters.Dtos;

namespace RestaurantCorp.Adapters
{
    public interface IViewData
    {
        void SaveOrder(Order order);
    }
}
