using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.GUI.Features.ClientInterface.Restaurants
{
    internal class InfoRestaurantViewModel : ViewModelBase
    {

        #region Properties
        private Restaurant? _restaurant;
        public Restaurant? Restaurant
        {
            get => _restaurant;
            set
            {
                _restaurant = value;
                OnPropertyChanged(nameof(Restaurant));

            }
        }
        #endregion

        #region Services
        private IRestaurantService restaurantService;
        #endregion


        public InfoRestaurantViewModel(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }
    }
}
