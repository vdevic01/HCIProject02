using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Ninject;
using HCIProject02.GUI.DTO;
using HCIProject02.GUI.Features.AgentInterface.Arrangements;
using HCIProject02.GUI.Features.AgentInterface.Management;
using HCIProject02.GUI.Features.ClientInterface;
using HCIProject02.GUI.Features.ClientInterface.Attractions;
using HCIProject02.GUI.Features.ClientInterface.Restaurants;
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

        public ICommand AttractionManagementCommand { get; }
        public ICommand ArrangementManagementCommand { get; }
        public ICommand RestaurantManagementCommand { get; }
        public ICommand MapCommand { get; }

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
            SwitchCurrentViewModel(viewModel);
        }

        private void NavigateToAttractionManagementView()
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            AttractionManagementViewModel viewModel = ServiceLocator.Get<AttractionManagementViewModel>();
            SwitchCurrentViewModel(viewModel);
        }

        private void NavigateToRestaurantManagementView()
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            RestaurantManagementViewModel viewModel = ServiceLocator.Get<RestaurantManagementViewModel>();
            SwitchCurrentViewModel(viewModel);
        }

        private void NavigateToArrangementManagementView()
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            ArrangementManagementViewModel viewModel = ServiceLocator.Get<ArrangementManagementViewModel>();
            SwitchCurrentViewModel(viewModel);
        }

        private void NavigateToMapView()
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            AgentMapViewModel viewModel = ServiceLocator.Get<AgentMapViewModel>();
            SwitchCurrentViewModel(viewModel);
        }
        public AgentNavigationViewModel() {
            RegisterHandlers();
            _returnButtonVisibility = Visibility.Collapsed;
            LogoutCommand = new RelayCommand(obj => LogoutUser());
            HotelManagementCommand = new RelayCommand(obj => NavigateToHotelManagementView());
            RestaurantManagementCommand = new RelayCommand(obj => NavigateToRestaurantManagementView());
            ArrangementManagementCommand = new RelayCommand(obj => NavigateToArrangementManagementView());
            AttractionManagementCommand = new RelayCommand(obj => NavigateToAttractionManagementView());
            MapCommand = new RelayCommand(obj => NavigateToMapView());
            NavigateToMapView();
        }

        private void RegisterHandlers()
        {
            Navigator.RegisterHandler(ViewType.NewHotelView, () =>
            {
                ReturnButtonVisibility = Visibility.Visible;
                NewHotelViewModel viewModel = ServiceLocator.Get<NewHotelViewModel>();
                SwitchCurrentViewModel(viewModel);
                ReturnCommand = new RelayCommand(obj => NavigateToHotelManagementView());
            });
            Navigator.RegisterHandler(ViewType.UpdateHotelView, obj =>
            {
                NavigatorEventDTO hotelInfo = (NavigatorEventDTO)obj;
                ReturnButtonVisibility = Visibility.Visible;
                UpdateHotelViewModel viewModel = ServiceLocator.Get<UpdateHotelViewModel>();
                viewModel.Hotel = (Hotel?)hotelInfo.Payload;
                SwitchCurrentViewModel(viewModel);
                ReturnCommand = new RelayCommand(obj => NavigateToHotelManagementView());
            });
            Navigator.RegisterHandler(ViewType.InfoHotelView, obj =>
            {
                NavigatorEventDTO hoteInfo = (NavigatorEventDTO)obj;
                ReturnButtonVisibility = Visibility.Visible;
                InfoHotelViewModel viewModel = ServiceLocator.Get<InfoHotelViewModel>();
                viewModel.Hotel = (Hotel)hoteInfo.Payload;
                SwitchCurrentViewModel(viewModel);
                ReturnCommand = new RelayCommand(obj => NavigateToHotelManagementView());
            });
            Navigator.RegisterHandler(ViewType.ArrangementView, obj =>
            {
                NavigatorEventDTO arrangementInfo = (NavigatorEventDTO)obj;
                ReturnButtonVisibility = Visibility.Visible;
                ArrangementViewModel viewModel = ServiceLocator.Get<ArrangementViewModel>();
                viewModel.Arrangement = (Arrangement?)arrangementInfo.Payload;
                SwitchCurrentViewModel(viewModel);
                ReturnCommand = new RelayCommand(obj => NavigateToArrangementManagementView());
            });
            Navigator.RegisterHandler(ViewType.NewArrangementView, () =>
            {
                ReturnButtonVisibility = Visibility.Visible;
                NewArrangementViewModel viewModel = ServiceLocator.Get<NewArrangementViewModel>();
                SwitchCurrentViewModel(viewModel);
                ReturnCommand = new RelayCommand(obj => NavigateToArrangementManagementView());
            });
            Navigator.RegisterHandler(ViewType.NewRestaurantView, () =>
            {
                ReturnButtonVisibility = Visibility.Visible;
                NewRestaurantViewModel viewModel = ServiceLocator.Get<NewRestaurantViewModel>();
                SwitchCurrentViewModel(viewModel);
                ReturnCommand = new RelayCommand(obj => NavigateToRestaurantManagementView());
            });
            Navigator.RegisterHandler(ViewType.UpdateRestaurantView, obj =>
            {
                NavigatorEventDTO restaurantInfo = (NavigatorEventDTO)obj;
                ReturnButtonVisibility = Visibility.Visible;
                UpdateRestaurantViewModel viewModel = ServiceLocator.Get<UpdateRestaurantViewModel>();
                viewModel.Restaurant = (Restaurant?)restaurantInfo.Payload;
                SwitchCurrentViewModel(viewModel);
                ReturnCommand = new RelayCommand(obj => NavigateToRestaurantManagementView());
            });
            Navigator.RegisterHandler(ViewType.InfoRestaurantView, obj =>
            {
                NavigatorEventDTO restaurantInfo = (NavigatorEventDTO)obj;
                ReturnButtonVisibility = Visibility.Visible;
                InfoRestaurantViewModel viewModel = ServiceLocator.Get<InfoRestaurantViewModel>();
                viewModel.Restaurant = (Restaurant)restaurantInfo.Payload;
                SwitchCurrentViewModel(viewModel);
                ReturnCommand = new RelayCommand(obj => NavigateToRestaurantManagementView());
            });
            Navigator.RegisterHandler(ViewType.NewAttractionView, () =>
            {
                ReturnButtonVisibility = Visibility.Visible;
                NewAttractionViewModel viewModel = ServiceLocator.Get<NewAttractionViewModel>();
                SwitchCurrentViewModel(viewModel);
                ReturnCommand = new RelayCommand(obj => NavigateToAttractionManagementView());
            });
            Navigator.RegisterHandler(ViewType.UpdateAttractionView, obj =>
            {
                NavigatorEventDTO attractionInfo = (NavigatorEventDTO)obj;
                ReturnButtonVisibility = Visibility.Visible;
                UpdateAttractionViewModel viewModel = ServiceLocator.Get<UpdateAttractionViewModel>();
                viewModel.Attraction = (Attraction?)attractionInfo.Payload;
                SwitchCurrentViewModel(viewModel);
                ReturnCommand = new RelayCommand(obj => NavigateToAttractionManagementView());
            });
            Navigator.RegisterHandler(ViewType.InfoAttractionView, obj =>
            {
                NavigatorEventDTO attractionInfo = (NavigatorEventDTO)obj;
                ReturnButtonVisibility = Visibility.Visible;
                InfoAttractionViewModel viewModel = ServiceLocator.Get<InfoAttractionViewModel>();
                viewModel.Attraction = (Attraction)attractionInfo.Payload;
                SwitchCurrentViewModel(viewModel);
                ReturnCommand = new RelayCommand(obj => NavigateToAttractionManagementView());
            });
        }
    }
}
