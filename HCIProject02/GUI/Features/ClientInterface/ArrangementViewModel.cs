using HCIProject02.Core.Model;
using HCIProject02.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.GUI.Features.ClientInterface
{
    class ArrangementViewModel : ViewModelBase
    {
        private Arrangement? _arrangement;
        public Arrangement? Arrangement
        {
            get => _arrangement;
            set
            {
                _arrangement = value;
                OnPropertyChanged(nameof(Arrangement));
            }
        }
        public ArrangementViewModel()
        {

        }
    }
}
