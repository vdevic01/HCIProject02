using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.DTO;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.AgentInterface
{
    public class AgentMapViewModel : ViewModelBase
    {
        #region Properties

        private PointOfInterest _selectedPoint;
        public PointOfInterest SelectedPoint
        {
            get => _selectedPoint;
            set
            {
                _selectedPoint = value;
                OnPropertyChanged(nameof(SelectedPoint));
                ShowDetails();
            }
        }



        private bool _showHotels;
        public bool ShowHotels
        {
            get => _showHotels;
            set
            {
                _showHotels = value;
                OnPropertyChanged(nameof(ShowHotels));
                if (_showHotels)
                {
                    List<Hotel>? hotels = _hotelService.GetAll();
                    foreach (Hotel? hotel in hotels)
                    {
                        _pointsOfInterest.Add(hotel);
                    }
                }
                else {
                    RemovePointsOfInterestByType<Hotel>();
                }
            }
        }





        private bool _showAttractions;
        public bool ShowAttractions
        {
            get => _showAttractions;
            set
            {
                _showAttractions = value;
                OnPropertyChanged(nameof(ShowAttractions));
                if (_showAttractions)
                {
                    List<Attraction>? attractions = _attractionService.GetAll();
                    foreach (Attraction? attraction in attractions)
                    {
                        _pointsOfInterest.Add(attraction);
                    }
                }
                else
                {
                    RemovePointsOfInterestByType<Attraction>();
                }
            }
        }

        private bool _showRestaurants;
        public bool ShowRestaurants
        {
            get => _showRestaurants;
            set
            {
                _showRestaurants = value;
                OnPropertyChanged(nameof(ShowRestaurants));
                if (_showRestaurants)
                {
                    List<Restaurant>? restaurants = _restaurantService.GetAll();
                    foreach (Restaurant? restaurant in restaurants)
                    {
                        _pointsOfInterest.Add(restaurant);
                    }
                }
                else
                {
                    RemovePointsOfInterestByType<Restaurant>();
                }
            }
        }



        private ObservableCollection<PointOfInterest> _pointsOfInterest;
        public ObservableCollection<PointOfInterest> PointsOfInterest
        {
            get => _pointsOfInterest;
            set {
                _pointsOfInterest = value;
                OnPropertyChanged(nameof(PointsOfInterest));
            }
        }



        #endregion
        #region Services
        private readonly IHotelService _hotelService;
        private readonly IAttractionService _attractionService;
        private readonly IRestaurantService _restaurantService;
        #endregion

        private void RemovePointsOfInterestByType<T>() where T : PointOfInterest
        {
            for (int i = _pointsOfInterest.Count - 1; i >= 0; i--)
            {
                if (_pointsOfInterest[i] is T)
                {
                    _pointsOfInterest.RemoveAt(i);
                }
            }
        }

        private void ShowDetails()
        {
            if (SelectedPoint is Hotel)
            {
                NavigatorEventDTO dto = new NavigatorEventDTO(SelectedPoint, ViewType.MapView);
                Navigator.FireEvent(ViewType.InfoHotelView, dto);
            }
            else if (SelectedPoint is Restaurant)
            {
                NavigatorEventDTO dto = new NavigatorEventDTO(SelectedPoint, ViewType.MapView);
                Navigator.FireEvent(ViewType.InfoRestaurantView, dto);
            }
            else if (SelectedPoint is Attraction)
            {
                NavigatorEventDTO dto = new NavigatorEventDTO(SelectedPoint, ViewType.MapView);
                Navigator.FireEvent(ViewType.InfoAttractionView, dto);
            }
        }




        public AgentMapViewModel(IHotelService hotelService, IAttractionService attractionService, IRestaurantService restaurantService)
        {
            _hotelService = hotelService;
            _attractionService = attractionService;
            _restaurantService = restaurantService;
            _showHotels = false;
            _showAttractions = false;
            _showRestaurants = false;
            _pointsOfInterest = new ObservableCollection<PointOfInterest>();
        }
    }
}
