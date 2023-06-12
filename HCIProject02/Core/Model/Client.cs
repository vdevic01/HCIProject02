using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Model
{
    public class Client : User
    {
        private IList<Arrangement> _arrangements;
        public virtual IList<Arrangement> Arrangements { get => _arrangements; set => OnPropertyChanged(ref _arrangements, value); }
        public Client() { }
    }
}
