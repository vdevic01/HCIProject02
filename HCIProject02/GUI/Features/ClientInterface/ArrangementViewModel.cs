﻿using HCIProject02.Commands;
using HCIProject02.Core.Exceptions;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.Dialog;
using HCIProject02.GUI.Dialog.Implementations;
using HCIProject02.GUI.DTO;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.ClientInterface
{


    class ArrangementViewModel : ViewModelBase
    {
        #region Properties
        private Arrangement? _arrangement;
        public Arrangement? Arrangement
        {
            get => _arrangement;
            set
            {
                _arrangement = value;
                OnPropertyChanged(nameof(Arrangement));
                _pointsOfInterest.Add(Arrangement.Hotel);
                this.Attractions = new List<AttractionCardDTO>();
                foreach (var attr in Arrangement.Attractions)
                {
                    ICommand command = new RelayCommand(obj =>
                    {
                        NavigatorEventDTO dto = new NavigatorEventDTO(attr, ViewType.ArrangementView);
                        Navigator.FireEvent(ViewType.InfoAttractionView, dto);
                    });
                    AttractionCardDTO card = new AttractionCardDTO(attr, command);
                    Attractions.Add(card);
                    AttractionLocations.Add(new Location((double)attr.Latitude, (double)attr.Longitude));
                    _pointsOfInterest.Add(attr);
                }
            }
        }

        public Visibility BookButtonVisibility { get; set; }

        private User _authenticatedUser;
        public User AuthenticatedUser
        {
            get => _authenticatedUser;
            set
            {
                _authenticatedUser = value;
                if(value.Role == Role.Agent)
                {
                    BookButtonVisibility = Visibility.Hidden;
                }
                else
                {
                    BookButtonVisibility = Visibility.Visible;
                }
            }
        }

        private List<AttractionCardDTO> _attractions;
        public List<AttractionCardDTO> Attractions
        {
            get => _attractions;
            set
            {
                _attractions = value;
                OnPropertyChanged(nameof(Attractions));
            }
        }

        private PointOfInterest _selectedPoint;
        public PointOfInterest SelectedPoint
        {
            get => _selectedPoint;
            set
            {
                _selectedPoint = value;
                OnPropertyChanged(nameof(SelectedPoint));
                ShowDetails();
            }
        }

        private LocationCollection _attractionLocations;
        public LocationCollection AttractionLocations
        {
            get => _attractionLocations;
            set
            {
                _attractionLocations = value;
                OnPropertyChanged(nameof(AttractionLocations));
            }
        }

        private ObservableCollection<PointOfInterest> _pointsOfInterest;
        public ObservableCollection<PointOfInterest> PointsOfInterest
        {
            get => _pointsOfInterest;
            set
            {
                _pointsOfInterest = value;
                OnPropertyChanged(nameof(PointsOfInterest));
            }
        }

        #endregion

        #region Commands
        public ICommand BookCommand { get; }
        public ICommand AttractionCommand { get; }
        public ICommand HotelCommand { get; }
        #endregion

        #region Services
        private readonly IDialogService _dialogService;
        private readonly IBookingService _bookingService;
        private readonly IAttractionService _attractionService;
        #endregion


        private void ShowDetails()
        {
            if (SelectedPoint is Hotel)
            {
                NavigatorEventDTO dto = new NavigatorEventDTO(SelectedPoint, ViewType.MapView);
                Navigator.FireEvent(ViewType.InfoHotelView, dto);
            }
            else if (SelectedPoint is Attraction)
            {
                NavigatorEventDTO dto = new NavigatorEventDTO(SelectedPoint, ViewType.MapView);
                Navigator.FireEvent(ViewType.InfoAttractionView, dto);
            }
        }

        private void NavigateToHotelView()
        {
            NavigatorEventDTO dto = new NavigatorEventDTO(Arrangement.Hotel, ViewType.ArrangementView);
            Navigator.FireEvent(ViewType.InfoHotelView, dto);
        }

        private void NavigateToAttractionView(Attraction attraction)
        {
            NavigatorEventDTO dto = new NavigatorEventDTO(attraction, ViewType.ArrangementView);
            Navigator.FireEvent(ViewType.InfoAttractionView, dto);
        }

        public ArrangementViewModel(IDialogService dialogService, IBookingService bookingService, IAttractionService attractionService)
        {
            _bookingService = bookingService;
            _dialogService = dialogService;
            _attractionService = attractionService;
            HotelCommand = new RelayCommand(obj => NavigateToHotelView());
            AttractionCommand = new RelayCommand(obj => NavigateToAttractionView(obj as Attraction));
            _pointsOfInterest = new ObservableCollection<PointOfInterest>();
            _attractionLocations = new LocationCollection();
            BookCommand = new RelayCommand(obj =>
            {
                var yesNoDialog = new YesNoDialogViewModel("Arrangement booking confirmation", "Are you sure you want to book this arrangement?");
                _dialogService.ShowDialog(yesNoDialog, result =>
                {
                    if(result == null)
                    {
                        return;
                    }
                    if ((bool)result)
                    {
                        OkDialogViewModel okDialog;
                        try
                        {
                            _bookingService.BookArrangement(Arrangement, (Client)AuthenticatedUser);
                            okDialog = new OkDialogViewModel("Message", "Booking was successful.");
                        }
                        catch (ArrangementAlreadyBookedException)
                        {
                            okDialog = new OkDialogViewModel("Message", "You have already booked this arrangement.");
                        }
                        _dialogService.ShowDialog(okDialog, result => { }, true);
                    }
                }, true);
            });
            
        }
    }
}
