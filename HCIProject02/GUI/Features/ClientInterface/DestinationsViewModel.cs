using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.Core.Service.Users;
using HCIProject02.GUI.DTO;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.ClientInterface
{
    public class ArrangementCardDTO
    {
        public Arrangement Arrangement { get; set; }
        public ICommand BookCommand { get; set; }

        public ArrangementCardDTO(Arrangement arrangement, ICommand bookCommand)
        {
            Arrangement = arrangement;
            BookCommand = bookCommand;
        }
    }
    public class DestinationsViewModel : ViewModelBase
    {
        private readonly IArrangementService arrangementService;

        private List<ArrangementCardDTO> _arrangements;
        private List<ArrangementCardDTO> _filteredArrangements;
        public List<ArrangementCardDTO> FilteredArrangements
        {
            get => _filteredArrangements;
            set
            {
                _filteredArrangements = value;
                OnPropertyChanged(nameof(FilteredArrangements));
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
                    FilteredArrangements = _arrangements.Where(arrangementDTO => arrangementDTO.Arrangement.Name.ToLower().StartsWith(value.ToLower())).ToList();
                }
            }
        }
        public ICommand ClearSearchBoxCommand { get; }
        public DestinationsViewModel(IArrangementService arrangementService)
        {
            ClearSearchBoxCommand = new RelayCommand(obj =>
            {
                SearchText = "";
            });
            this.arrangementService = arrangementService;
            _arrangements = arrangementService.GetAll().Select(arrangement =>
            {
                ICommand command = new RelayCommand(obj =>
                {
                    NavigatorEventDTO dto = new NavigatorEventDTO(arrangement, ViewType.DestinationsView);
                    Navigator.FireEvent(ViewType.ArrangementView, dto);
                });
                ArrangementCardDTO card = new ArrangementCardDTO(arrangement, command);
                return card;
            }).ToList();
            _filteredArrangements = _arrangements;
        }
    }
}
