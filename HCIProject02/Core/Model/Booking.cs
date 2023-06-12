using HCIProject02.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Model
{
    public class Booking : BaseObservableEntity
    {
        private Client _passenger;
        public virtual Client Passenger { get => _passenger; set => OnPropertyChanged(ref _passenger, value); }

        private Arrangement _arrangement;
        public virtual Arrangement Arrangement { get => _arrangement; set => OnPropertyChanged(ref _arrangement, value);}

        private DateTime _bookingTime;
        public DateTime BookingTime { get => _bookingTime; set => OnPropertyChanged(ref _bookingTime, value); }

        private double _price;
        public double Price { get => _price; set => OnPropertyChanged(ref _price, value); }

        public Booking()
        {

        }

        public Booking(Booking other) : base(other)
        {
            Passenger = other.Passenger;
            Arrangement = other.Arrangement;
            BookingTime = other.BookingTime;
            Price = other.Price;
        }
    }
}
