using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.DTO;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.ClientInterface
{
    public class AttractionCardDTO
    {
        public Attraction Attraction { get; set; }
        public ICommand BookCommand { get; set; }

        public AttractionCardDTO(Attraction attraction, ICommand bookCommand)
        {
            Attraction = attraction;
            BookCommand = bookCommand;
        }
    }
    public class AllAttractionsViewModel : ViewModelBase
    {
        private readonly IAttractionService attractionService;

        private List<AttractionCardDTO> _attractions;
        private List<AttractionCardDTO> _filteredAttractions;
        public List<AttractionCardDTO> FilteredAttractions
        {
            get => _filteredAttractions;
            set
            {
                _filteredAttractions = value;
                OnPropertyChanged(nameof(FilteredAttractions));
            }
        }

        private String? _searchText;
        public String? SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                if (value != null)
                {
                    FilteredAttractions = _attractions.Where(attractionDTO => attractionDTO.Attraction.Name.ToLower().StartsWith(value.ToLower())).ToList();
                }
            }
        }
        public ICommand ClearSearchBoxCommand { get; }
        public AllAttractionsViewModel(IAttractionService attractionService)
        {
            ClearSearchBoxCommand = new RelayCommand(obj =>
            {
                SearchText = "";
            });
            this.attractionService = attractionService;
            _attractions = attractionService.GetAll().Select(attraction =>
            {
                ICommand command = new RelayCommand(obj =>
                {
                    NavigatorEventDTO dto = new NavigatorEventDTO(attraction, ViewType.AllAttractionsView);
                    Navigator.FireEvent(ViewType.InfoAttractionView, dto);
                });
                AttractionCardDTO card = new AttractionCardDTO(attraction, command);
                return card;
            }).ToList();
            _filteredAttractions = _attractions;
        }
    }
}
