using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Model
{
    public class Restaurant : PointOfInterest
    {
        public Restaurant() { }
        public Restaurant(Restaurant other) : base(other)
        {
        }
    }
}
