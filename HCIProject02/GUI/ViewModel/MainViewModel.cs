using HCIProject02.Core.Model;
using HCIProject02.Core.Ninject;
using HCIProject02.GUI.Features.AgentInterface;
using HCIProject02.GUI.Features.ClientInterface;
using HCIProject02.GUI.Features.LoginAndRegister;
using HCIProject02.Navigation;
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
            object viewModel;
            Navigator.RegisterHandler(ViewType.RegisterView, () =>
            {
                viewModel = ServiceLocator.Get<RegisterViewModel>();
                SwitchCurrentViewModel(viewModel);
            });
            Navigator.RegisterHandler(ViewType.LoginView, () =>
            {
                viewModel = ServiceLocator.Get<LoginViewModel>();
                SwitchCurrentViewModel(viewModel);
            });
            Navigator.RegisterHandler(ViewType.ClientHome, obj =>
            {
                User user = (User)obj;
                viewModel = ServiceLocator.Get<NavigationViewModel>();
                ((NavigationViewModel)viewModel).AuthenticatedUser = user;
                SwitchCurrentViewModel(viewModel);
            });
            Navigator.RegisterHandler(ViewType.AgentHome, obj =>
            {
                User user = (User)obj;
                viewModel = ServiceLocator.Get<AgentNavigationViewModel>();
                ((AgentNavigationViewModel)viewModel).AuthenticatedUser = user;
                SwitchCurrentViewModel(viewModel);
            });

        }
    }
}
