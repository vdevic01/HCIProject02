using HCIProject02.Core.Model;
using System;
using HCIProject02.GUI.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCIProject02.Core.Service.Travel;
using HCIProject02.Commands;

namespace HCIProject02.GUI.Features.ClientInterface
{
    internal class InfoHotelViewModel : ViewModelBase
    {

        #region Properties

       

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
        #endregion

        #region Services
        private IHotelService hotelService;
        #endregion

        public InfoHotelViewModel(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }


    }
}
