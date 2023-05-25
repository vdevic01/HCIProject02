using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Model
{
    public class Destination : PointOfInterest
    {
        public Destination() { }
        public Destination(PointOfInterest other) : base(other)
        {
        }
    }
}
