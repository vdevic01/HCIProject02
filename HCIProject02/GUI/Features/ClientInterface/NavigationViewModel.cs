using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Ninject;
using HCIProject02.Core.Service.Travel;
using HCIProject02.Core.Service.Travel.Implementation;
using HCIProject02.GUI.DTO;
using HCIProject02.GUI.Features.AgentInterface;
using HCIProject02.GUI.Features.ClientInterface.Attractions;
using HCIProject02.GUI.Features.ClientInterface.Restaurants;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using Microsoft.Maps.MapControl.WPF;
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
        public ICommand MapCommand{ get; }
        public ICommand DestinationsCommand { get; }
        public ICommand AllRestaurantsCommand { get; }
        public ICommand AllHotelsCommand { get; }
        public ICommand AllAttractionsCommand { get; }

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
        private void NavigateToMyBookingsView()
            //TODO VRATITI KAKO JE BILO
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            MyBookingsViewModel viewModel = ServiceLocator.Get<MyBookingsViewModel>();
            viewModel.AuthenticatedUser = AuthenticatedUser;
            SwitchCurrentViewModel(viewModel);


            ReturnButtonVisibility = Visibility.Collapsed;



        }
        private void NavigateToMapView()
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            AgentMapViewModel viewModel = ServiceLocator.Get<AgentMapViewModel>();
            SwitchCurrentViewModel(viewModel);
        }
        private void NavigateToDestinationsView()
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            DestinationsViewModel viewModel = ServiceLocator.Get<DestinationsViewModel>();
            SwitchCurrentViewModel(viewModel);
        }

        private void NavigateToAllRestaurantsView()
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            AllRestaurantsViewModel viewModel = ServiceLocator.Get<AllRestaurantsViewModel>();
            SwitchCurrentViewModel(viewModel);
        }

        private void NavigateToAllHotelsView()
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            AllHotelsViewModel viewModel = ServiceLocator.Get<AllHotelsViewModel>();
            SwitchCurrentViewModel(viewModel);
        }

        private void NavigateToAllAttractionsView()
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            AllAttractionsViewModel viewModel = ServiceLocator.Get<AllAttractionsViewModel>();
            SwitchCurrentViewModel(viewModel);
        }


        public NavigationViewModel()
        {
            _returnButtonVisibility = Visibility.Collapsed;
         
            RegisterHandlers();
            NavigateToDestinationsView();
            DestinationsCommand = new RelayCommand(obj => NavigateToDestinationsView());
            MyBookingsCommand = new RelayCommand(obj => NavigateToMyBookingsView());
            MapCommand = new RelayCommand(obj => NavigateToMapView());
            AllRestaurantsCommand = new RelayCommand(obj => NavigateToAllRestaurantsView());
            AllHotelsCommand = new RelayCommand(obj => NavigateToAllHotelsView());
            AllAttractionsCommand = new RelayCommand(obj => NavigateToAllAttractionsView());

            LogoutCommand = new RelayCommand(obj => LogoutUser());
        }

        private void registerHandlerForUpdatingHotel(object obj)
        {
            if (obj == null)
            {
                return;
            }
            ReturnButtonVisibility = Visibility.Visible;
            NavigatorEventDTO navigatorEventDTO = (NavigatorEventDTO)obj;
            Hotel hotel = (Hotel)navigatorEventDTO.Payload;
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
            UpdateHotelViewModel viewModel = ServiceLocator.Get<UpdateHotelViewModel>();
            viewModel.Hotel = hotel;
            Location location = new Location(latitude: (double)hotel.Latitude, longitude: (double)hotel.Longitude);
            viewModel.PinLocation = location;

            SwitchCurrentViewModel(viewModel);
        }
        private void RegisterHandlers()
        {
            Navigator.RegisterHandler(ViewType.InfoRestaurantView, obj =>
            {
                NavigatorEventDTO restaurantInfo = (NavigatorEventDTO)obj;
                ReturnButtonVisibility = Visibility.Visible;
                InfoRestaurantViewModel viewModel = ServiceLocator.Get<InfoRestaurantViewModel>();
                viewModel.Restaurant = (Restaurant?)restaurantInfo.Payload;
                SwitchCurrentViewModel(viewModel);
                ReturnCommand = new RelayCommand(obj => NavigateToAllRestaurantsView());
            });
            Navigator.RegisterHandler(ViewType.InfoHotelView, obj =>
            {
                NavigatorEventDTO hotelInfo = (NavigatorEventDTO)obj;
                ReturnButtonVisibility = Visibility.Visible;
                InfoHotelViewModel viewModel = ServiceLocator.Get<InfoHotelViewModel>();
                viewModel.Hotel = (Hotel?)hotelInfo.Payload;
                SwitchCurrentViewModel(viewModel);
                ReturnCommand = new RelayCommand(obj => NavigateToAllHotelsView());
            });
            Navigator.RegisterHandler(ViewType.InfoAttractionView, obj =>
            {
                NavigatorEventDTO attractionInfo = (NavigatorEventDTO)obj;
                ReturnButtonVisibility = Visibility.Visible;
                InfoAttractionViewModel viewModel = ServiceLocator.Get<InfoAttractionViewModel>();
                viewModel.Attraction = (Attraction?)attractionInfo.Payload;
                SwitchCurrentViewModel(viewModel);
                ReturnCommand = new RelayCommand(obj => NavigateToAllAttractionsView());
            });

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
                        case ViewType.MapView:
                            NavigateToMapView();
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
