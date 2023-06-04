using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Ninject;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using System.Windows;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.AgentInterface
{
    public class AgentNavigationViewModel : NavigableViewModel
    {
        #region Properties
        public User? AuthenticatedUser { get; set; }

        private Visibility _returnButtonVisibility;
        public Visibility ReturnButtonVisibility
        {
            get => _returnButtonVisibility;
            set
            {
                _returnButtonVisibility = value;
                OnPropertyChanged(nameof(ReturnButtonVisibility));
            }
        }

        #endregion

        #region Commands
        public ICommand LogoutCommand { get; }
        public ICommand HotelManagementCommand { get; }
        private ICommand _returnCommand;
        public ICommand ReturnCommand
        {
            get => _returnCommand;
            set
            {
                _returnCommand = value;
                OnPropertyChanged(nameof(ReturnCommand));
            }
        }
        #endregion

        private void LogoutUser()
        {
            AuthenticatedUser = null;
            Navigator.FireEvent(ViewType.LoginView);
        }
        private void NavigateToHotelManagementView()
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            HotelManagementViewModel viewModel = ServiceLocator.Get<HotelManagementViewModel>();
            viewModel.AuthenticatedUser = AuthenticatedUser;
            SwitchCurrentViewModel(viewModel);
        }
        public AgentNavigationViewModel() {
            _returnButtonVisibility = Visibility.Collapsed;

            LogoutCommand = new RelayCommand(obj => LogoutUser());
            HotelManagementCommand = new RelayCommand(obj => NavigateToHotelManagementView());
        }
    }
}
