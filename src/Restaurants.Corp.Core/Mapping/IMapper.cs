using Restaurants.Corp.Domain;

namespace Restaurants.Corp.Core.Mapping
{
    public interface IMapper
    {
        Order ToDomainOrder(RestaurantCorp.Adapters.Dtos.Order order);
    }
}
