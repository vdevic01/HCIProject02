using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.DTO;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
                Arrangements = new List<ArrangementCardDTO>();
                foreach (var arr in Attraction.Arrangements)
                {
                    ICommand command = new RelayCommand(obj =>
                    {
                        NavigatorEventDTO dto = new NavigatorEventDTO(arr, ViewType.InfoAttractionView);
                        Navigator.FireEvent(ViewType.ArrangementView, dto);
                    });
                    ArrangementCardDTO card = new ArrangementCardDTO(arr, command);
                    Arrangements.Add(card);
                }

            }
        }

        private List<ArrangementCardDTO> _arrangements;
        public List<ArrangementCardDTO> Arrangements
        {
            get => _arrangements;
            set
            {
                _arrangements = value;
                OnPropertyChanged(nameof(Arrangements));
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
