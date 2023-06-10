using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.Features.AgentInterface.Arrangements.DragAndDrop;
using HCIProject02.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.AgentInterface.Arrangements
{
    internal class NewArrangementViewModel : ViewModelBase
    {

        public AttractionListingViewModel AllAttractions { get; }
        public AttractionListingViewModel ChosenAttractions { get; }

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

        private List<Hotel> _allHotels;
        public List<Hotel> AllHotels
        {
            get => _allHotels;
            set
            {
                _allHotels = value;
                OnPropertyChanged(nameof(AllHotels));
            }
        }

        private List<Hotel> _filteredHotels;
        public List<Hotel> FilteredHotels
        {
            get => _filteredHotels;
            set
            {
                _filteredHotels = value;
                OnPropertyChanged(nameof(FilteredHotels));
            }
        }

        private Hotel _selectedHotel;
        public Hotel SelectedHotel
        {
            get => _selectedHotel;
            set
            {
                _selectedHotel = value;
                OnPropertyChanged(nameof(SelectedHotel));
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
        private string? _tripPlan;
        public string? TripPlan
        {
            get => _tripPlan;
            set
            {
                _tripPlan = value;
                OnPropertyChanged(nameof(TripPlan));
            }
        }

        private string _price;
        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private DateTime _returnDate;
        public DateTime ReturnDate
        {
            get => _returnDate;
            set
            {
                _returnDate = value;
                OnPropertyChanged(nameof(ReturnDate));
            }
        }

        private DateTime _departureDate;
        public DateTime DepartureDate
        {
            get => _departureDate;
            set
            {
                _departureDate = value;
                OnPropertyChanged(nameof(DepartureDate));
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
        #endregion

        #region Commands
        public ICommand AddNewArrangementCommand { get; }
        #endregion

        #region Services
        private IArrangementService _arrangementService;
        private IHotelService _hotelService;
        private IAttractionService _attractionService;
        #endregion

        private void AddNewArrangement()
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
            if (string.IsNullOrEmpty(TripPlan))
            {
                ErrorMessage = "Field (Trip Plan) is required";
                return;
            }
            if (string.IsNullOrEmpty(FilePath))
            {
                ErrorMessage = "Image is required";
                return;
            }
            if (string.IsNullOrEmpty(SelectedHotel.Name))
            {
                ErrorMessage = "Pick a hotel";
                return;
            }

            List<Attraction> attractions = new List<Attraction>();
            foreach (var attraction in ChosenAttractions.AttractionItemViewModels)
            {
                attractions.Add(attraction.AttractionItem);
            }


            Arrangement arrangement = new Arrangement
            {
                Name = Name,
                Description = Description,
                TripPlan = TripPlan,
                ImagePath = FilePath,
                DepartureTime = DepartureDate,
                ReturnTime = ReturnDate,
                Price = double.Parse(Price),
                Hotel = SelectedHotel,
                Attractions = attractions
            };
            _arrangementService.Create(arrangement);
            MessageBox.Show("Arrangement created");

        }



        public NewArrangementViewModel(IArrangementService arrangementService, IHotelService hotelService, IAttractionService attractionService)
        {
            _arrangementService = arrangementService;
            _hotelService = hotelService;
            _attractionService = attractionService;
            AllHotels = _hotelService.GetAll();
            FilteredHotels = AllHotels;
            AddNewArrangementCommand = new RelayCommand(obj => AddNewArrangement());
            ChosenAttractions = new AttractionListingViewModel();
            AttractionListingViewModel allAttractionsViewModel = new AttractionListingViewModel();
            List<Attraction> attractions = _attractionService.GetAll();
            foreach(var attraction in attractions)
            {
                allAttractionsViewModel.AddAttractionItem(new AttractionItemViewModel(attraction));
            }
            AllAttractions = allAttractionsViewModel;
            ReturnDate = DateTime.Today;
            DepartureDate = DateTime.Today;

        }

    }
}
