using HCIProject02.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Service.Travel
{
    public interface IHotelService
    {
        public Hotel Create(Hotel hotel);
        public Hotel? Update(Hotel hotel);
        public Hotel GetHotelByName(string name);

    }
}
