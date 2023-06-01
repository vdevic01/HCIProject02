using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Model
{
    public class Hotel : PointOfInterest
    {
        private int _numberOfStars;
        public int NumberOfStars { get => _numberOfStars; set => OnPropertyChanged(ref _numberOfStars, value); }

        public Hotel()
        {

        }


        public Hotel(Hotel other) : base(other)
        {
            NumberOfStars = other.NumberOfStars;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
