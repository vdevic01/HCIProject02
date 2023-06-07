using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.ViewModel;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.GUI.Features.ClientInterface.Attractions
{
    internal class InfoAttractionViewModel : ViewModelBase
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

        #endregion

        #region Services
        private IAttractionService attractionService;
        #endregion

        public InfoAttractionViewModel(IAttractionService attractionService)
        {
            this.attractionService = attractionService;
            
        }
    }
}
