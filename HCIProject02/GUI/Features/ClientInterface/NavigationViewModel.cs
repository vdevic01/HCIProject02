using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Ninject;
using HCIProject02.GUI.DTO;
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
        public ICommand MyBookingsCommand {get;}
        public ICommand DestinationsCommand { get; }

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
        private void NavigateToMyBookingsView()
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            MyBookingsViewModel viewModel = ServiceLocator.Get<MyBookingsViewModel>();
            viewModel.AuthenticatedUser = AuthenticatedUser;
            SwitchCurrentViewModel(viewModel);
        }
        private void NavigateToDestinationsView()
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            DestinationsViewModel viewModel = ServiceLocator.Get<DestinationsViewModel>();
            SwitchCurrentViewModel(viewModel);
        }
        public NavigationViewModel()
        {
            _returnButtonVisibility = Visibility.Collapsed;
            RegisterHandlers();
            NavigateToDestinationsView();
            DestinationsCommand = new RelayCommand(obj => NavigateToDestinationsView());
            MyBookingsCommand = new RelayCommand(obj => NavigateToMyBookingsView());
            LogoutCommand = new RelayCommand(obj => LogoutUser());
        }
        private void RegisterHandlers()
        {            
            Navigator.RegisterHandler(ViewType.ArrangementView, (obj) =>
            {
                if(obj == null)
                {
                    return;
                }
                ReturnButtonVisibility = Visibility.Visible;
                NavigatorEventDTO navigatorEventDTO = (NavigatorEventDTO)obj;
                Arrangement arrangement = (Arrangement)navigatorEventDTO.Payload;
                ViewType eventInvoker = navigatorEventDTO.EventInvoker;
                ReturnCommand = new RelayCommand(param =>
                {
                    ReturnButtonVisibility = Visibility.Collapsed;
                    switch (eventInvoker)
                    {
                        case ViewType.DestinationsView:
                            NavigateToDestinationsView();
                            break;
                        case ViewType.MyBookingsView:
                            NavigateToMyBookingsView();
                            break;
                    }
                });
                ArrangementViewModel viewModel = ServiceLocator.Get<ArrangementViewModel>();
                ((ArrangementViewModel)viewModel).Arrangement = arrangement;
                ((ArrangementViewModel)viewModel).AuthenticatedUser = AuthenticatedUser;
                SwitchCurrentViewModel(viewModel);
            });
        }
    }
}
