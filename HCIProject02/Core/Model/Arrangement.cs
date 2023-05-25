using HCIProject02.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Model
{
    public class Arrangement : BaseObservableEntity
    {
        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        private string _description;
        public string Description { get => _description; set => OnPropertyChanged(ref _description, value); }

        private Hotel _hotel;
        public virtual Hotel Hotel { get => _hotel; set => OnPropertyChanged(ref _hotel, value);}

        private double _price;
        public double Price { get => _price; set => OnPropertyChanged(ref _price, value);}

        private DateTime _departureTime;
        public DateTime DepartureTime { get => _departureTime; set => OnPropertyChanged(ref _departureTime, value); }

        private DateTime _returnTime;
        public DateTime ReturnTime { get => _returnTime; set => OnPropertyChanged(ref _returnTime, value); }

        private IList<Attraction> _attractions;
        public virtual IList<Attraction> Attractions { get => _attractions; set => OnPropertyChanged(ref _attractions, value); }

        private Destination _destination;

        public virtual Destination Destination { get => _destination; set => OnPropertyChanged(ref _destination, value); }

        private string _tripPlan;
        public string TripPlan { get => _tripPlan; set => OnPropertyChanged(ref _tripPlan, value);}

        private IList<Client> _passengers;
        public virtual IList<Client> Passengers { get => _passengers; set => OnPropertyChanged(ref _passengers, value); }

        public Arrangement()
        {

        }
    }
}
