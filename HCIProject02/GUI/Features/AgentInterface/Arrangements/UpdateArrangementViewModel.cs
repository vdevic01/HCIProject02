using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.Dialog;
using HCIProject02.GUI.Dialog.Implementations;
using HCIProject02.GUI.Features.AgentInterface.Arrangements.DragAndDrop;
using HCIProject02.GUI.ViewModel;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HCIProject02.GUI.Features.AgentInterface.Arrangements
{
    internal class UpdateArrangementViewModel : ViewModelBase
    {
        #region Properties

        private Arrangement _selectedArrangement;
        public Arrangement SelectedArrangement
        {
            get => _selectedArrangement;
            set
            {
                _selectedArrangement = value;
                OnPropertyChanged(nameof(SelectedArrangement));
                Image = new ImageBrush(new BitmapImage(new Uri(SelectedArrangement.ImagePath)));
                AttractionListingViewModel chosenAttractionsViewModel = new AttractionListingViewModel();
                AttractionListingViewModel allAttractionsViewModel = new AttractionListingViewModel();
                List<Attraction> attractions = _attractionService.GetAll();
                foreach (var attraction in attractions)
                {
                    if (SelectedArrangement.Attractions.Contains(attraction))
                    {
                        chosenAttractionsViewModel.AddAttractionItem(new AttractionItemViewModel(attraction));
                    }
                    else
                    {
                        allAttractionsViewModel.AddAttractionItem(new AttractionItemViewModel(attraction));
                    }

                }
                this.AllAttractions = allAttractionsViewModel;
                this.ChosenAttractions = chosenAttractionsViewModel;
            }
        }
        public AttractionListingViewModel AllAttractions { get; set; }
        public AttractionListingViewModel ChosenAttractions { get; set; }

        private ImageBrush _image;
        public ImageBrush Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        private List<Hotel> _allHotels;
        public List<Hotel> AllHotels
        {
            get => _allHotels;
            set
            {
                _allHotels= value;
                OnPropertyChanged(nameof(AllHotels));
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
        public ICommand UpdateArrangementCommand { get; }
        #endregion

        #region Services
        private IArrangementService _arrangementService;
        private IHotelService _hotelService;
        private IAttractionService _attractionService;
        private readonly IDialogService _dialogService;
        #endregion



        private void UpdateArrangement()
        {
            
            if (string.IsNullOrEmpty(SelectedArrangement.Name))
            {
                ErrorMessage = "Field (Name) is required";
                return;
            }
            if (string.IsNullOrEmpty(SelectedArrangement.Description))
            {
                ErrorMessage = "Field (Description) is required";
                return;
            }
            if (string.IsNullOrEmpty(SelectedArrangement.TripPlan))
            {
                ErrorMessage = "Field (Trip Plan) is required";
                return;
            }
            if (string.IsNullOrEmpty(SelectedArrangement.ImagePath))
            {
                ErrorMessage = "Image of arrangement is required";
                return;
            }
            if (SelectedArrangement.Hotel == null)
            {
                ErrorMessage = "Field (Hotel) is required";
                return;
            }
            if (SelectedArrangement.DepartureTime > SelectedArrangement.ReturnTime)
            {
                ErrorMessage = "Departure time can not be after return time";
                return;
            }
            if (string.IsNullOrEmpty(SelectedArrangement.Price.ToString()) || SelectedArrangement.Price <= 0)
            {
                ErrorMessage = "Field (Price) is required";
                return;
            }



            var yesNoDialog = new YesNoDialogViewModel("Arrangement updating confirmation", "Are you sure you want to update this attraction ?");

            _dialogService.ShowDialog(yesNoDialog, result =>
            {
                if (result == null)
                {
                    return;
                }
                if ((bool)result)
                {
                    List<Attraction> attractions = new List<Attraction>();
                    foreach (var attraction in ChosenAttractions.AttractionItemViewModels)
                    {
                        attractions.Add(attraction.AttractionItem);
                    }
                    SelectedArrangement.Attractions = attractions;
                    Arrangement? arrangement = _arrangementService.Update(SelectedArrangement);
                    if (arrangement != null)
                    {
                        OkDialogViewModel okDialog = new OkDialogViewModel("Message", "Arrangement updated.");
                        _dialogService.ShowDialog(okDialog, result => { }, true);
                    }
                }
            }, true);



        }



        public UpdateArrangementViewModel(IHotelService hotelService, IArrangementService arrangementService, IDialogService dialogService, IAttractionService attractionService)
        {
            _arrangementService = arrangementService;
            _hotelService = hotelService;
            _dialogService = dialogService;
            _attractionService = attractionService;
            UpdateArrangementCommand = new RelayCommand(obj => UpdateArrangement());
            AllHotels = hotelService.GetAll();

            
        }
    }
}
