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
    public class LoginViewModel : ViewModelBase
    {
        public ICommand NavigateToRegisterViewCommand { get; }

        private void NavigateToRegisterView()
        {
            Navigator.FireEvent(ViewType.RegisterView);
        }
        public LoginViewModel()
        {
            NavigateToRegisterViewCommand = new RelayCommand(obj => NavigateToRegisterView());
        }
    }
}
