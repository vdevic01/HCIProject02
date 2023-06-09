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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
                OnPropertyChanged(nameof(Attraction));

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
        public ICommand UpdateAttractionCommand { get; }
        #endregion

        #region Services
        private IAttractionService attractionService;
        private readonly IDialogService _dialogService;
        #endregion


        private void UpdateAttraction()
        {
            Log.Information(Attraction.ToString());
            Attraction? attraction = attractionService.Update(Attraction);
            if (attraction != null)
            {
                OkDialogViewModel okDialog = new OkDialogViewModel("Message", "Attraction updated.");
                _dialogService.ShowDialog(okDialog, result => { }, true);
            }

        }

        public UpdateAttractionViewModel(IAttractionService attractionService, IDialogService dialogService)
        {
            this.attractionService = attractionService;

            UpdateAttractionCommand = new RelayCommand(obj => UpdateAttraction());


        }
    }
}
