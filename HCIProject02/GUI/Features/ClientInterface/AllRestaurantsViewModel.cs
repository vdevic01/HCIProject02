using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.DTO;
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
    public class RestaurantCardDTO
    {
        public Restaurant Restaurant { get; set; }
        public ICommand DetailsCommand { get; set; }

        public RestaurantCardDTO(Restaurant restaurant, ICommand detailsCommand)
        {
            Restaurant = restaurant;
            DetailsCommand = detailsCommand;
        }
    }
    public class AllRestaurantsViewModel : ViewModelBase
    {
        private readonly IRestaurantService restaurantService;

        private List<RestaurantCardDTO> _restaurants;
        private List<RestaurantCardDTO> _filteredRestaurants;
        public List<RestaurantCardDTO> FilteredRestaurants
        {
            get => _filteredRestaurants;
            set
            {
                _filteredRestaurants = value;
                OnPropertyChanged(nameof(FilteredRestaurants));
            }
        }

        private String? _searchText;
        public String? SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                if (value != null)
                {
                    FilteredRestaurants = _restaurants.Where(restaurantDTO => restaurantDTO.Restaurant.Name.ToLower().Contains(value.ToLower())).ToList();
                }
            }
        }
        public ICommand ClearSearchBoxCommand { get; }
        public AllRestaurantsViewModel(IRestaurantService restaurantService)
        {
            ClearSearchBoxCommand = new RelayCommand(obj =>
            {
                SearchText = "";
            });
            this.restaurantService = restaurantService;
            _restaurants = restaurantService.GetAll().Select(restaurant=>
            {
                ICommand command = new RelayCommand(obj =>
                {
                    NavigatorEventDTO dto = new NavigatorEventDTO(restaurant, ViewType.AllRestaurantsView);
                    Navigator.FireEvent(ViewType.InfoRestaurantView, dto);
                });
                RestaurantCardDTO card = new RestaurantCardDTO(restaurant, command);
                return card;
            }).ToList();
            _filteredRestaurants = _restaurants;
        }
    }
}
