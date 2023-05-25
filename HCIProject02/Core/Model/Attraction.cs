using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Model
{
    public class Attraction : PointOfInterest
    {
        private IList<Arrangement> _arrangements;
        public virtual IList<Arrangement> Arrangements { get => _arrangements; set => OnPropertyChanged(ref _arrangements, value); }
        public Attraction() { }
        public Attraction(Attraction other) : base(other)
        {
            Arrangements = other.Arrangements;
        }
    }
}
