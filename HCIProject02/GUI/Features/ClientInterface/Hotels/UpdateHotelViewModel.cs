using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.ViewModel;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Castle.DynamicProxy;
using System.ComponentModel;
using Serilog;
using HCIProject02.GUI.Dialog;
using HCIProject02.GUI.Dialog.Implementations;
using HCIProject02.Core.Service.Travel.Implementation;

namespace HCIProject02.GUI.Features.ClientInterface
{
    internal class UpdateHotelViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region Properties

        public event EventHandler<EventArgs> PinLocationChanged;

        private Hotel? _hotel;
        public Hotel? Hotel
        {
            get => _hotel;
            set
            {
                _hotel = value;
                OnPropertyChanged(nameof(Hotel));

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

        private Location _mapCenter;
        public Location MapCenter
        {
            get => _mapCenter;
            set
            {
                _mapCenter = value;
                OnPropertyChanged(nameof(MapCenter));
            }
        }

        #endregion

        #region Commands
        public ICommand UpdateHotelCommand { get; }
        #endregion

        #region Services
        private IHotelService hotelService;
        private readonly IDialogService _dialogService;
        #endregion


        private void UpdateHotel()
        {
 
            Hotel.Latitude = PinLocation.Latitude;
            Hotel.Longitude = PinLocation.Longitude;



            var yesNoDialog = new YesNoDialogViewModel("Hotel updating confirmation", "Are you sure you want to update this hotel ?");

            _dialogService.ShowDialog(yesNoDialog, result =>
            {
                if (result == null)
                {
                    return;
                }
                if ((bool)result)
                {

                    Hotel? hotel = hotelService.Update(Hotel);
                    if (hotel != null)
                    {
                        OkDialogViewModel okDialog = new OkDialogViewModel("Message", "Hotel updated.");
                        _dialogService.ShowDialog(okDialog, result => { }, true);
                    }
                }
            }, true);



         
        }


        public UpdateHotelViewModel(IHotelService hotelService, IDialogService dialogService)
        {
            this.hotelService = hotelService;
            _dialogService = dialogService;
            UpdateHotelCommand = new RelayCommand(obj => UpdateHotel());
            
        }


    }
}
