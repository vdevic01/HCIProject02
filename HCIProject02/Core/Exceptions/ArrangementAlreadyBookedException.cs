using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Exceptions
{
    class ArrangementAlreadyBookedException : Exception
    {
        public ArrangementAlreadyBookedException() : base() { }
        public ArrangementAlreadyBookedException(string message) : base(message) { }
        public ArrangementAlreadyBookedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
