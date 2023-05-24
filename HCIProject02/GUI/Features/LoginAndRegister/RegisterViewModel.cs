using HCIProject02.Commands;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.LoginAndRegister
{
    internal class RegisterViewModel : ViewModelBase
    {
        public ICommand NavigateToLoginViewCommand { get; }

        private void NavigateToLoginView()
        {
            Navigator.FireEvent(ViewType.LoginView);
        }

        public RegisterViewModel()
        {
            NavigateToLoginViewCommand = new RelayCommand(obj => NavigateToLoginView());
        }
    }
}
