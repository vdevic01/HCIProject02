using HCIProject02.Core.Exceptions;
using HCIProject02.Core.Model;
using HCIProject02.Core.Repository.Travel;
using HCIProject02.Core.Repository.Users;
using HCIProject02.Core.Repository.Users.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Service.Travel.Implementation
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }
        public Hotel Create(Hotel hotel)
        { 
            return hotelRepository.Create(hotel);
        }
        public Hotel? Update(Hotel hotel)
        {
            return hotelRepository.Update(hotel);
        }

        public Hotel GetHotelByName(string name)
        {
            return hotelRepository.FindHotelByName(name);
        }
    }
}
