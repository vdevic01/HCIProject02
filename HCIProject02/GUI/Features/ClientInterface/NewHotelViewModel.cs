using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Users;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;

using System.Windows.Input;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows;
using Serilog;
using HCIProject02.Core.Service.Travel;

namespace HCIProject02.GUI.Features.ClientInterface
{
    internal class NewHotelViewModel : ViewModelBase
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

        private int _rating;
        public int Rating
        {
            get => _rating;
            set { _rating = value; OnPropertyChanged(nameof(Rating)); }
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

        #region Commands
        public ICommand AddNewHotelCommand { get; }
        #endregion

        #region Services
        private IHotelService hotelService;
        #endregion



        private void AddNewHotel()
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
                ErrorMessage = "Image of hotel is required";
                return;
            }


            Hotel hotel = new Hotel { NumberOfStars = Rating, Address = Address, Description = Description, Name = Name,
                                    ImagePath = FilePath, Longitude = Longitude, Latitude = Latitude};

            hotelService.Create(hotel);
            MessageBox.Show("Hotel created");

        }

        public NewHotelViewModel(IHotelService hotelService)
        {
            this.hotelService = hotelService;
            AddNewHotelCommand = new RelayCommand(obj => AddNewHotel());
           
        }

        
/*        public NewHotelViewModel(string imagePath)
        {
            FilePath = imagePath;
            AddNewHotelCommand = new RelayCommand(obj => AddNewHotel());
        }*/


    }
}
