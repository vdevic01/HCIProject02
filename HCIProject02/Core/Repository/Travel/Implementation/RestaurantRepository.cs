using HCIProject02.Core.Model;
using HCIProject02.Core.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Repository.Travel.Implementation
{
    public class RestaurantRepository : CrudRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(DatabaseContext context) : base(context)
        {
        }

        public Restaurant? FindRestaurantByName(string name)
        {
            return _entities.FirstOrDefault(restaurant => restaurant.Name == name);
        }


    }
}
