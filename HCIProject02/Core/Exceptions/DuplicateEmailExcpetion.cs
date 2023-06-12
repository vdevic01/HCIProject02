using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Exceptions
{
    public class DuplicateEmailExcpetion : Exception
    {
        public DuplicateEmailExcpetion() : base() { }
        public DuplicateEmailExcpetion(string message) : base(message) { }
        public DuplicateEmailExcpetion(string message, Exception innerException) : base(message, innerException) { }
    }
}
