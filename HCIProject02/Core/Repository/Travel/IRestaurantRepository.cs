using HCIProject02.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Repository
{
    public interface IRestaurantRepository : ICrudRepository<Restaurant>
    {
        public Restaurant? FindRestaurantByName(string name);

    }
}
