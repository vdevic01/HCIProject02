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
    public class HotelCardDTO
    {
        public Hotel Hotel { get; set; }
        public ICommand BookCommand { get; set; }

        public HotelCardDTO(Hotel hotel, ICommand bookCommand)
        {
            Hotel = hotel;
            BookCommand = bookCommand;
        }
    }
    public class AllHotelsViewModel : ViewModelBase
    {
        private readonly IHotelService hotelService;

        private List<HotelCardDTO> _hotels;
        private List<HotelCardDTO> _filteredHotels;
        public List<HotelCardDTO> FilteredHotels
        {
            get => _filteredHotels;
            set
            {
                _filteredHotels = value;
                OnPropertyChanged(nameof(FilteredHotels));
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
                    FilteredHotels = _hotels.Where(hotelDTO => hotelDTO.Hotel.Name.ToLower().StartsWith(value.ToLower())).ToList();
                }
            }
        }
        public ICommand ClearSearchBoxCommand { get; }
        public AllHotelsViewModel(IHotelService hotelService)
        {
            ClearSearchBoxCommand = new RelayCommand(obj =>
            {
                SearchText = "";
            });
            this.hotelService = hotelService;
            _hotels = hotelService.GetAll().Select(hotel =>
            {
                ICommand command = new RelayCommand(obj =>
                {
                    NavigatorEventDTO dto = new NavigatorEventDTO(hotel, ViewType.AllHotelsView);
                    Navigator.FireEvent(ViewType.InfoHotelView, dto);
                });
                HotelCardDTO card = new HotelCardDTO(hotel, command);
                return card;
            }).ToList();
            _filteredHotels = _hotels;
        }
    }
}
