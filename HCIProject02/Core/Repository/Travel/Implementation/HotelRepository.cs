using HCIProject02.Core.Model;
using HCIProject02.Core.Persistance;
using HCIProject02.Core.Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Repository.Travel.Implementation
{
    public class HotelRepository : CrudRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(DatabaseContext context) : base(context)
        {
        }

        public Hotel? FindHotelByName(string name)
        {
            return _entities.FirstOrDefault(hotel => hotel.Name == name);
        }

        public List<Hotel> GetAllHotels()
        {
            return _entities.ToList();
        }
    }
}
