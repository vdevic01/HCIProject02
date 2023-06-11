using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.ViewModel;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
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



        private bool _showChart;
        public bool ShowChart
        {
            get => _showChart;
            set
            {
                _showChart = value;
                OnPropertyChanged(nameof(ShowChart));
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

        private List<string> _monthlyReportValues;
        public List<string> MonthlyReportValues
        {
            get => _monthlyReportValues;
            set
            {
                _monthlyReportValues = value;
                OnPropertyChanged(nameof(MonthlyReportValues));
            }
        }

        private List<string> _xAxisValues;
        public List<string> XAxisValues
        {
            get => _xAxisValues;
            set
            {
                _xAxisValues= value;
                OnPropertyChanged(nameof(XAxisValues));
            }
        }

        private SeriesCollection _chartSeries;
        public SeriesCollection ChartSeries
        {
            get => _chartSeries;
            set
            {
                _chartSeries = value;
                OnPropertyChanged(nameof(ChartSeries));
            }
        }

        private Func<double, string> _formatter;
        public Func<double, string> Formatter
        {
            get => _formatter;
            set
            {
                _formatter = value;
                OnPropertyChanged(nameof(Formatter));
            }
        }




        private List<string> _allArrangements;
        public List<string> AllArrangements
        {
            get => _allArrangements;
            set
            {
                _allArrangements = value;
                OnPropertyChanged(nameof(AllArrangements));
            }
        }
        private string _selectedArrangement;
        public string SelectedArrangement
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
            TotalProfits = "Total profits: " + profit + " EUR";
            if(bookings.Count > 0) {
               Arrangement? mostCommonArrangement = bookings
              .GroupBy(booking => booking.Arrangement)
              .OrderByDescending(group => group.Count())
              .Select(group => group.Key)
              .FirstOrDefault();
                MostPopular = "Most popular arrangement: " + mostCommonArrangement.Name;
                GenerateMonthlyChartData(bookings);
                ShowChart = true;
            }
            else
            {
                MostPopular = "";
                ShowChart = false;
            }
           
            ReportTitle = "Report for " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(SelectedDate.Month) + " of " + SelectedDate.Year.ToString() + ".";
            ShowReportGrid = true;
        }

        private void GenerateArrangementReport()
        {
            if (SelectedArrangement == null)
            {
                return;
            }
            Arrangement arrangement = _arrangementService.GetArrangementByName(SelectedArrangement);
            List<Booking> bookings = _bookingService.GetBookingsByArrangement(arrangement);
            TotalNumberSold = "Sold arrangements: " + bookings.Count();
            double profit = 0;

            foreach (Booking booking in bookings)
            {
                profit += booking.Price;
            }
            TotalProfits = "Total profits: " + profit + " EUR";
            if(bookings.Count > 0)
            {
                var groupedBookings = bookings
                .GroupBy(booking => new { Month = booking.BookingTime.Month, Year = booking.BookingTime.Year })
                .OrderByDescending(group => group.Count())
                .FirstOrDefault();
                MostPopular = "Most popular month: " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupedBookings.Key.Month) + " of " + groupedBookings.Key.Year.ToString() + ".";
                GenerateArrangementChartData(bookings);
                ShowChart = true;
            }
            else
            {
                MostPopular = "";
                ShowChart = false;
            }
            ReportTitle = "Report for " + SelectedArrangement;
            ShowReportGrid = true;

        }

        private void GenerateArrangementChartData(List<Booking> bookings)
        {
            var groupedBookings = bookings
                .GroupBy(booking => new { Month = booking.BookingTime.Month, Year = booking.BookingTime.Year })
                .OrderBy(group => group.Key.Year)
                .ThenBy(group => group.Key.Month)
                .ToList();
            List<string> xAxisValues = groupedBookings
                .Select(group => $"{CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(group.Key.Month)} {group.Key.Year}")
                .ToList();
            List<int> yAxisValues = groupedBookings
                .Select(group => group.Count())
                .ToList();
            var series = new SeriesCollection
    {
        new LineSeries
        {
            Title = "Sold Arrangements",
            Values = new ChartValues<int>(yAxisValues)
        }
    };
            ChartSeries = series;
            XAxisValues = xAxisValues;

            // Formatter for y-axis labels
            Formatter = value => value.ToString();

            OnPropertyChanged(nameof(ChartSeries));
            OnPropertyChanged(nameof(XAxisValues));
        }


        private void GenerateMonthlyChartData(List<Booking> bookings)
        {
            var arrangementCounts = bookings
                .GroupBy(booking => booking.Arrangement)
                .Select(group => new { Arrangement = group.Key, Count = group.Count() })
                .ToList();
            List<string>? arrangementNames = arrangementCounts.Select(item => item.Arrangement.Name).ToList();
            List<int>? counts = arrangementCounts.Select(item => item.Count).ToList();

            SeriesCollection? series = new SeriesCollection
        {
            new ColumnSeries
            {
                Title = "Sold Arrangements",
                Values = new ChartValues<ObservableValue>(counts.Select(count => new ObservableValue(count)))
            }
        };
            ChartSeries = series;
            XAxisValues = arrangementNames;

            // Formatter for y-axis labels
            Formatter = value => value.ToString();

            OnPropertyChanged(nameof(ChartSeries));
            OnPropertyChanged(nameof(XAxisValues));
        }



        public ReportViewModel(IArrangementService arrangementService, IBookingService bookingService)
        {
            _arrangementService = arrangementService;
            _bookingService = bookingService;
            AllArrangements = arrangementService.GetAll().Select(arrangement => arrangement.Name).ToList();
            SelectedDate = DateTime.Today;
            ShowChart = false;
            ArrangementReport = false;
            ShowReportGrid = false;
            MonthlyReportCommand = new RelayCommand(obj => GenerateMonthlyReport());
            ArrangementReportCommand = new RelayCommand(obj => GenerateArrangementReport());
            //SelectedArrangement = Arrangements.First();


        }




    }
}
