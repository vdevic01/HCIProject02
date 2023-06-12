using HCIProject02.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Service.Travel
{
    public interface IBookingService
    {
        public Booking BookArrangement(Arrangement arrangement, Client passenger);
        public List<Booking> GetBookingsForUser(Client user);

        public List<Booking> GetBookingsByArrangement(Arrangement arrangement);

        public List<Booking> GetBookingByMonth(DateTime dateTime);
    }
}
