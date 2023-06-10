using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Ninject;
using HCIProject02.Core.Service.Travel;
using HCIProject02.Core.Service.Travel.Implementation;
using HCIProject02.GUI.DTO;
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

        private readonly IAttractionService attractionService;
        private readonly IHotelService hotelService;
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
            //TODO VRATITI KAKO JE BILO
        {
            /*            ReturnButtonVisibility = Visibility.Collapsed;
                        MyBookingsViewModel viewModel = ServiceLocator.Get<MyBookingsViewModel>();
                        viewModel.AuthenticatedUser = AuthenticatedUser;
                        SwitchCurrentViewModel(viewModel);*/


            ReturnButtonVisibility = Visibility.Collapsed;

            UpdateAttractionViewModel viewModel = ServiceLocator.Get<UpdateAttractionViewModel>();
            Attraction? res = attractionService.GetAttractionByName("Hram Svetog Save");
            viewModel.PinLocation = new Location(latitude: (double)res.Latitude, longitude: (double)res.Longitude);
            viewModel.Attraction = res;
            SwitchCurrentViewModel(viewModel);

            /*            NewRestaurantViewModel viewModel = ServiceLocator.Get<NewRestaurantViewModel>();
                        SwitchCurrentViewModel(viewModel);*/

            /*            InfoAttractionViewModel viewModel = ServiceLocator.Get<InfoAttractionViewModel>();
                        Attraction? res = attractionService.GetAttractionByName("Hram Svetog Save");
                        viewModel.Attraction = res;*/
            /*
                        UpdateHotelViewModel viewModel = ServiceLocator.Get<UpdateHotelViewModel>();
                        Hotel? res = hotelService.GetHotelByName("Reks hotel");
                        viewModel.Hotel = res;
                        viewModel.PinLocation = new Location(latitude: (double)res.Latitude, longitude: (double)res.Longitude);
            */
            SwitchCurrentViewModel(viewModel);

        }
        private void NavigateToDestinationsView()
        {
            ReturnButtonVisibility = Visibility.Collapsed;
            DestinationsViewModel viewModel = ServiceLocator.Get<DestinationsViewModel>();
            SwitchCurrentViewModel(viewModel);
        }
        //TODO: KONSTRUKTOR TREBA DA JE PRAZAN
        public NavigationViewModel(IAttractionService attractionService, IHotelService hotelService)
        {
            _returnButtonVisibility = Visibility.Collapsed;
            this.attractionService = attractionService;
            this.hotelService = hotelService;
            RegisterHandlers();
            NavigateToDestinationsView();
            DestinationsCommand = new RelayCommand(obj => NavigateToDestinationsView());
            MyBookingsCommand = new RelayCommand(obj => NavigateToMyBookingsView());
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

            Navigator.RegisterHandler(ViewType.UpdateHotelView, (obj) =>
            {
                registerHandlerForUpdatingHotel(obj);
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
