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
using System.Windows;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.ClientInterface
{
    public class NavigationViewModel : NavigableViewModel
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
        public NavigationViewModel()
        {
            _returnButtonVisibility = Visibility.Collapsed;
            RegisterHandlers();
            Navigator.FireEvent(ViewType.DestinationsView);
            LogoutCommand = new RelayCommand(obj => LogoutUser());
        }
        private void RegisterHandlers()
        {
            object viewModel;
            Navigator.RegisterHandler(ViewType.DestinationsView, () =>
            {
                ReturnButtonVisibility = Visibility.Collapsed;
                viewModel = ServiceLocator.Get<DestinationsViewModel>();
                SwitchCurrentViewModel(viewModel);
            });
            Navigator.RegisterHandler(ViewType.ArrangementView, (obj) =>
            {
                ReturnButtonVisibility = Visibility.Visible;
                ReturnCommand = new RelayCommand(obj =>
                {
                    DestinationsViewModel destinationsViewModel = ServiceLocator.Get<DestinationsViewModel>();
                    SwitchCurrentViewModel(destinationsViewModel);
                });
                Arrangement arrangement = (Arrangement) obj;
                 viewModel = ServiceLocator.Get<ArrangementViewModel>();
                ((ArrangementViewModel)viewModel).Arrangement = arrangement;
                SwitchCurrentViewModel(viewModel);
            });
        }
    }
}
