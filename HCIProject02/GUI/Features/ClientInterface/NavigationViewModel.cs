using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Ninject;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.ClientInterface
{
    public class NavigationViewModel : NavigableViewModel
    {
        #region Properties
        public User? AuthenticatedUser { get; set; }

        #endregion

        #region Commands
        public ICommand LogoutCommand { get; }
        #endregion

        private void LogoutUser()
        {
            AuthenticatedUser = null;
            Navigator.FireEvent(ViewType.LoginView);
        }
        public NavigationViewModel()
        {
            RegisterHandlers();
            Navigator.FireEvent(ViewType.DestinationsView);
            LogoutCommand = new RelayCommand(obj => LogoutUser());
        }
        private void RegisterHandlers()
        {
            object viewModel;
            Navigator.RegisterHandler(ViewType.DestinationsView, () =>
            {
                viewModel = ServiceLocator.Get<DestinationsViewModel>();
                SwitchCurrentViewModel(viewModel);
            });
        }
    }
}
