using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.ViewModel;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace HCIProject02.GUI.Features.ClientInterface.Restaurants
{
    internal class NewRestaurantViewModel : ViewModelBase
    {
        #region Properties
        private string? _filePath;
        public string? FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        private string? _name;
        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string? _description;
        public string? Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string? _address;
        public string? Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string? _errorMessage;
        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }


        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set { _longitude = value; OnPropertyChanged(nameof(Longitude)); }
        }

        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set { _latitude = value; OnPropertyChanged(nameof(Latitude)); }
        }

        #endregion


        #region Services
        private IRestaurantService restaurantService;
        #endregion

        #region Commands
        public ICommand AddNewRestaurantCommand { get; }
        #endregion


        private void AddNewRestaurant()
        {
            if (string.IsNullOrEmpty(Name))
            {
                ErrorMessage = "Field (Name) is required";
                return;
            }
            if (string.IsNullOrEmpty(Description))
            {
                ErrorMessage = "Field (Description) is required";
                return;
            }
            if (string.IsNullOrEmpty(Address))
            {
                ErrorMessage = "Field (Address) is required";
                return;
            }
            if (string.IsNullOrEmpty(FilePath))
            {
                ErrorMessage = "Image of Restaurant is required";
                return;
            }

            Restaurant restaurant = new Restaurant
            {
                Address = Address,
                Description = Description,
                Name = Name,
                ImagePath = FilePath,
                Longitude = Longitude,
                Latitude = Latitude
            };
            Restaurant? r = restaurantService.Create(restaurant);
            if (r != null)
            {
                MessageBox.Show("Restaurant created");
            }



        

        }

        public NewRestaurantViewModel(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;

            AddNewRestaurantCommand = new RelayCommand(obj => AddNewRestaurant());

        }


    }
}
