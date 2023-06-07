using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.Core.Service.Travel.Implementation;
using HCIProject02.GUI.ViewModel;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.ClientInterface.Attractions
{
    internal class NewAttractionViewModel : ViewModelBase
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




        private IList<Arrangement> _arrangements;
        #endregion


        #region Services
        private IAttractionService attractionService;
        private IArrangementService arrangementService;
        #endregion

        #region Commands
        public ICommand AddNewAttractionCommand { get; }
        #endregion



        private void AddNewAttraction()
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


        

            Attraction attraction = new Attraction
            {
                Address = Address,
                Description = Description,
                Name = Name,
                ImagePath = FilePath,
                Longitude = Longitude,
                Latitude = Latitude,
                Arrangements = _arrangements
            };

            Log.Information(attraction.ToString());

            Attraction? a = attractionService.Create(attraction);
            if (a != null)
            {
                MessageBox.Show("Attraction created");
            }


        }



        public NewAttractionViewModel(IAttractionService attractionService, IArrangementService arrangementService)
        {
            this.attractionService = attractionService;
            this.arrangementService = arrangementService;
            this._arrangements = new List<Arrangement>();
            AddNewAttractionCommand = new RelayCommand(obj => AddNewAttraction());
        }

    }
}
