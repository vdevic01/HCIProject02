using HCIProject02.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.GUI.ViewModel
{
    class NavigableViewModel : ObservableEntity
    {
        private object _currentViewModel;
        public object CurrentViewModel { get => _currentViewModel; set => OnPropertyChanged(ref _currentViewModel, value); }

        public NavigableViewModel()
        {
        }

        public void SwitchCurrentViewModel(object nextViewModel)
        {
            CurrentViewModel = nextViewModel;
        }
    }
}
