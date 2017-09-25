using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Corp.Core.Dtos;

namespace Restaurants.Corp.Core.BusinessLogic.Interfaces
{
    public interface IRestaurant
    {
        void SetStockQuantity(RestaurantStock stock);
    }
}
