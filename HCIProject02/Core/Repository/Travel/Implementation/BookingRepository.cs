using HCIProject02.Core.Model;
using HCIProject02.Core.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Repository.Travel.Implementation
{
    public class BookingRepository : CrudRepository<Booking>, IBookingRepository
    {
        public BookingRepository(DatabaseContext context) : base(context)
        {
        }
        public List<Booking> GetBookingsForPassenger(Client passenger)
        {
            return _entities.Where(booking => booking.Passenger.Id == passenger.Id).ToList();
        }
    }
}
