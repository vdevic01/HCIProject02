using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.Core.Service.Travel.Implementation;
using HCIProject02.GUI.ViewModel;
using Microsoft.Maps.MapControl.WPF;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.ClientInterface.Restaurants
{
    internal class UpdateRestaurantViewModel : ViewModelBase, INotifyPropertyChanged
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
        private Location _pinLocation;
        public Location PinLocation
        {
            get { return _pinLocation; }
            set
            {
                _pinLocation = value;
                OnPropertyChanged(nameof(PinLocation));


            }
        }

        #endregion

        #region Commands
        public ICommand UpdateRestaurantCommand { get; }
        #endregion

        #region Services
        private IRestaurantService restaurantService;
        #endregion
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateRestaurant()
        {
            Log.Information(Restaurant.ToString());
            Restaurant? restaurant = restaurantService.Update(Restaurant);
            if (restaurant != null)
            {
                MessageBox.Show("Restaurant updated.");
            }
            
        }
        

        public UpdateRestaurantViewModel(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
  
            UpdateRestaurantCommand = new RelayCommand(obj => UpdateRestaurant());


        }
    }
}
