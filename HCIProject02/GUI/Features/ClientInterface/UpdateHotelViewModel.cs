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

    #endregion

        #region Commands
    public ICommand UpdateHotelCommand { get; }
        #endregion

        #region Services
        private IHotelService hotelService;
        #endregion

        private void UpdateHotel()
        {
            Log.Information(Hotel.ToString());
            Hotel? hotel = this.hotelService.Update(Hotel);
           if (hotel != null)
            {
                MessageBox.Show("Hotel updated.");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
           
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public UpdateHotelViewModel(IHotelService hotelService)
        {
            this.hotelService = hotelService;
            UpdateHotelCommand = new RelayCommand(obj => UpdateHotel());
         
            
        }


    }
}
