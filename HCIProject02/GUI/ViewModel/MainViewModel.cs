using HCIProject02.GUI.Features.LoginAndRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.GUI.ViewModel
{
    class MainViewModel : NavigableViewModel
    {

        public LoginViewModel LoginViewModelLogin { get; set; }

        public MainViewModel(LoginViewModel loginViewModel) {
            LoginViewModelLogin = loginViewModel;
            SwitchCurrentViewModel(loginViewModel);
            RegisterHandler();
        }
        private void RegisterHandler()
        {

        }
    }
}
