using HCIProject02.Core.Model;
using HCIProject02.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Service.Travel.Implementation
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository) {
            this.restaurantRepository = restaurantRepository;
        }

        public Restaurant Create(Restaurant restaurant)
        {
            return this.restaurantRepository.Create(restaurant);
        }

        public Restaurant? GetRestaurantByName(string name)
        {
            return this.restaurantRepository.FindRestaurantByName(name);
        }

        public Restaurant? Update(Restaurant restaurant)
        {
            return this.restaurantRepository.Update(restaurant);
        }
    }
}
