using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.AgentInterface.Report
{
    public class ReportViewModel : ViewModelBase
    {
        #region Properties

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private bool _showReportGrid;
        public bool ShowReportGrid
        {
            get => _showReportGrid;
            set
            {
                _showReportGrid = value;
                OnPropertyChanged(nameof(ShowReportGrid));
            }
        }



        private bool _monthlyReport;
        public bool MonthlyReport 
        {
            get => _monthlyReport;
            set
            {
                _monthlyReport = value;
                OnPropertyChanged(nameof(MonthlyReport));
            }
        }

        private bool _arrangementReport;
        public bool ArrangementReport
        {
            get => _arrangementReport;
            set
            {
                _arrangementReport = value;
                OnPropertyChanged(nameof(ArrangementReport));
            }
        }

        private string _totalNumberSold;
        public string TotalNumberSold
        {
            get => _totalNumberSold;
            set
            {
                _totalNumberSold = value;
                OnPropertyChanged(nameof(TotalNumberSold));
            }
        }

        private string _totalProfits;
        public string TotalProfits
        {
            get => _totalProfits;
            set
            {
                _totalProfits = value;
                OnPropertyChanged(nameof(TotalProfits));
            }
        }

        private string _reportTitle;
        public string ReportTitle
        {
            get => _reportTitle;
            set
            {
                _reportTitle = value;
                OnPropertyChanged(nameof(ReportTitle));
            }
        }

        private string _mostPopular;
        public string MostPopular
        {
            get => _mostPopular;
            set
            {
                _mostPopular = value;
                OnPropertyChanged(nameof(MostPopular));
            }
        }


        private List<Arrangement> _arrangements;
        public List<Arrangement> Arrangements
        {
            get => _arrangements;
            set
            {
                _arrangements = value;
                OnPropertyChanged(nameof(Arrangements));
            }
        }
        private Arrangement _selectedArrangement;
        public Arrangement SelectedArrangement
        {
            get => _selectedArrangement;
            set
            {
                _selectedArrangement = value;
                OnPropertyChanged(nameof(SelectedArrangement));
            }
        }


        #endregion
        #region Services
        private readonly IArrangementService _arrangementService;
        private readonly IBookingService _bookingService;
        #endregion

        #region Commands
        public ICommand MonthlyReportCommand { get; set; }
        public ICommand ArrangementReportCommand { get; set; }
        #endregion



        private void GenerateMonthlyReport()
        {
            List<Booking> bookings =  _bookingService.GetBookingByMonth(SelectedDate);
            TotalNumberSold = "Sold arrangements: " + bookings.Count();
            double profit = 0;
            
            foreach(Booking booking in bookings)
            {
                profit += booking.Price;
            }
            TotalProfits = "Total profits: " + profit;
            Arrangement? mostCommonArrangement = bookings
               .GroupBy(booking => booking.Arrangement)
               .OrderByDescending(group => group.Count())
               .Select(group => group.Key)
               .FirstOrDefault();
            MostPopular = "Most popular arrangement: " + mostCommonArrangement.Name;
            ReportTitle = "Report for " + SelectedDate.Month.ToString() + " of " + SelectedDate.Year.ToString();
            MonthlyReport = true;
            ShowReportGrid = true;
        }

        private void GenerateArrangementReport()
        {
            Console.WriteLine(this._selectedArrangement) ;
            List<Booking> bookings = _bookingService.GetBookingsByArrangement(SelectedArrangement);
            TotalNumberSold = "Sold arrangements: " + bookings.Count();
            double profit = 0;

            foreach (Booking booking in bookings)
            {
                profit += booking.Price;
            }
            TotalProfits = "Total profits: " + profit;

            // Find the most popular month and year
            var groupedBookings = bookings
                .GroupBy(booking => new { Month = booking.BookingTime.Month, Year = booking.BookingTime.Year })
                .OrderByDescending(group => group.Count())
                .FirstOrDefault();
            MostPopular = "Most popular month: " + groupedBookings.Key.Month.ToString() + " of " + groupedBookings.Key.Year.ToString();
            ReportTitle = "Report for " + SelectedArrangement.Name;
            ArrangementReport = true;
            ShowReportGrid = true;
        }



        public ReportViewModel(IArrangementService arrangementService, IBookingService bookingService)
        {
            _arrangementService = arrangementService;
            _bookingService = bookingService;
            _arrangements = arrangementService.GetAll();
            SelectedDate = DateTime.Today;
            MonthlyReport = false;
            ArrangementReport = false;
            ShowReportGrid = false;
            MonthlyReportCommand = new RelayCommand(obj => GenerateMonthlyReport());
            ArrangementReportCommand = new RelayCommand(obj => GenerateArrangementReport());
            SelectedArrangement = Arrangements.First();
        }




    }
}
