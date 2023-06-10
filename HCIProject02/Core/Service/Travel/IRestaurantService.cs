using HCIProject02.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Service.Travel
{
    public interface IRestaurantService
    {
        public Restaurant Create(Restaurant restaurant);
        public Restaurant? Update(Restaurant restaurant);

        public List<Restaurant> GetAll();
        public Restaurant? GetRestaurantByName(string name);
        public Restaurant Delete(Restaurant restaurant);

    }
}
