using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.Core.Service.Travel.Implementation;
using HCIProject02.GUI.Dialog;
using HCIProject02.GUI.Dialog.Implementations;
using HCIProject02.GUI.ViewModel;
using Microsoft.Maps.MapControl.WPF;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace HCIProject02.GUI.Features.ClientInterface.Attractions
{
    internal class UpdateAttractionViewModel : ViewModelBase
    {
        #region Properties

        private Attraction? _attraction;
        public Attraction? Attraction
        {
            get => _attraction;
            set
            {
                _attraction = value;
                UpdatedAttraction = new Attraction(_attraction);
                OnPropertyChanged(nameof(Attraction));

            }
        }

        public Attraction _updatedAttraction;
        public Attraction UpdatedAttraction
        {
            get => _updatedAttraction;
            set
            {
                _updatedAttraction = value;
                OnPropertyChanged(nameof(UpdateAttraction));
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
        public ICommand UpdateAttractionCommand { get; }
        #endregion

        #region Services
        private IAttractionService attractionService;
        private readonly IDialogService _dialogService;
        #endregion


        private void UpdateAttraction()
        {
            if (string.IsNullOrEmpty(UpdatedAttraction.Name))
            {
                ErrorMessage = "Field (Name) is required";
                return;
            }
            if (string.IsNullOrEmpty(UpdatedAttraction.Description))
            {
                ErrorMessage = "Field (Description) is required";
                return;
            }
            if (string.IsNullOrEmpty(UpdatedAttraction.Address))
            {
                ErrorMessage = "Field (Address) is required";
                return;
            }
            if (string.IsNullOrEmpty(UpdatedAttraction.ImagePath))
            {
                ErrorMessage = "Image of attraction is required";
                return;
            }


            var yesNoDialog = new YesNoDialogViewModel("Attraction updating confirmation", "Are you sure you want to update this attraction ?");

            _dialogService.ShowDialog(yesNoDialog, result =>
            {
                if (result == null)
                {
                    return;
                }
                if ((bool)result)
                {
                    Attraction.Name = UpdatedAttraction.Name; 
                    Attraction.Longitude = PinLocation.Longitude;
                    Attraction.Latitude = PinLocation.Latitude;
                    Attraction.ImagePath = UpdatedAttraction.ImagePath;
                    Attraction.Address = UpdatedAttraction.Address;
                    Attraction.Description = UpdatedAttraction.Description;

                    Attraction? attraction = attractionService.Update(Attraction);
                    if (attraction != null)
                    {
                        OkDialogViewModel okDialog = new OkDialogViewModel("Message", "Attraction updated.");
                        _dialogService.ShowDialog(okDialog, result => { }, true);
                    }
                }
            }, true);
            


        }

        public UpdateAttractionViewModel(IAttractionService attractionService, IDialogService dialogService)
        {
            this.attractionService = attractionService;
            _dialogService = dialogService;
            UpdateAttractionCommand = new RelayCommand(obj => UpdateAttraction());


        }
    }
}
