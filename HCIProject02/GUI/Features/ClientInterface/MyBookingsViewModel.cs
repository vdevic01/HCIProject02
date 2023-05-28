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
    public class BookingDTO
    {
        public Booking Booking { get; set; }
        public ICommand SeeDetailsCommand { get; set; }

        public BookingDTO(Booking booking, ICommand seeDetailsCommand)
        {
            Booking = booking;
            SeeDetailsCommand = seeDetailsCommand;
        }
    }
    public class MyBookingsViewModel : ViewModelBase
    {
        #region Properties
        private User _authenticatedUser;
        public User AuthenticatedUser
        {
            get => _authenticatedUser;
            set
            {
                _authenticatedUser = value;
                Client client = (Client)_authenticatedUser;
                Bookings = _bookingService.GetBookingsForUser(client).Select(booking =>
                {
                    ICommand command = new RelayCommand(obj =>
                    {
                        NavigatorEventDTO dto = new NavigatorEventDTO(booking.Arrangement, ViewType.MyBookingsView);
                        Navigator.FireEvent(ViewType.ArrangementView, dto );
                    });
                    BookingDTO tableDTO = new BookingDTO(booking, command);
                    return tableDTO;
                }).ToList();
            }
        }

        private List<BookingDTO> _bookings;
        public List<BookingDTO> Bookings
        {
            get => _bookings;
            set
            {
                _bookings = value;
                OnPropertyChanged(nameof(Bookings));
            }
        }
        #endregion
        #region Services
        private readonly IBookingService _bookingService;
        #endregion
        public MyBookingsViewModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
    }
}
