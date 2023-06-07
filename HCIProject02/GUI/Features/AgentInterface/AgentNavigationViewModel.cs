using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Ninject;
using HCIProject02.GUI.Features.ClientInterface;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using HCIProject02.GUI.DTO;

namespace HCIProject02.GUI.Features.AgentInterface
{
    class AgentNavigationViewModel : NavigableViewModel
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
            Navigator.RemoveHandler(ViewType.ArrangementView);
            Navigator.FireEvent(ViewType.LoginView);
        }
        private void NavigateToDestinationsView()
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            DestinationsViewModel viewModel = ServiceLocator.Get<DestinationsViewModel>();
            SwitchCurrentViewModel(viewModel);
        }
        public AgentNavigationViewModel()
        {
            _returnButtonVisibility = Visibility.Collapsed;
            RegisterHandlers();
            NavigateToDestinationsView();
            DestinationsCommand = new RelayCommand(obj => NavigateToDestinationsView());
            LogoutCommand = new RelayCommand(obj => LogoutUser());
        }
        private void RegisterHandlers()
        {
            Navigator.RegisterHandler(ViewType.ArrangementView, (obj) =>
            {
                if (obj == null)
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
                    }
                });
                ArrangementViewModel viewModel = ServiceLocator.Get<ArrangementViewModel>();
                viewModel.Arrangement = arrangement;
                viewModel.AuthenticatedUser = AuthenticatedUser;
                SwitchCurrentViewModel(viewModel);
            });
        }
    }
}
