using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Ninject;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.Dialog;
using HCIProject02.GUI.Dialog.Implementations;
using HCIProject02.GUI.DTO;
using HCIProject02.GUI.Features.ClientInterface;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.AgentInterface
{
    public class HotelManagementViewModel : ViewModelBase
    {
        #region Properties
        private User _authenticatedUser;
        public User AuthenticatedUser{
            get => _authenticatedUser;
            set
            {
                _authenticatedUser = value;
                Agent client = (Agent)_authenticatedUser;
            }
        }
        private Hotel _selectedHotel;
        public Hotel SelectedHotel
        {
            get => _selectedHotel;
            set
            {
                _selectedHotel = value;
                OnPropertyChanged(nameof(SelectedHotel));
            }
        }

        private ObservableCollection<Hotel> _hotels;
        public ObservableCollection<Hotel> Hotels
        {
            get => _hotels;
            set
            {
                _hotels = value;
                OnPropertyChanged(nameof(Hotels));
            }
        }
        #endregion
        #region Services
        private readonly IHotelService _hotelService;
        private readonly IDialogService _dialogService;
        #endregion

        #region Commands
        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DetailsCommand { get; set; }
        public object ReturnButtonVisibility { get; private set; }
        #endregion


        private void NavigateToCreateHotelView()
        {
            Navigator.FireEvent(ViewType.NewHotelView);
        }
        private void NavigateToUpdateHotelView()
        {
            if(SelectedHotel != null)
            {
                NavigatorEventDTO dto = new NavigatorEventDTO(SelectedHotel, ViewType.HotelManagementView);
                Navigator.FireEvent(ViewType.UpdateHotelView, dto);
            }
            
        }
        private void NavigateToDetailsHotelView()
        {
            if(SelectedHotel != null)
            {
                NavigatorEventDTO dto = new NavigatorEventDTO(SelectedHotel, ViewType.HotelManagementView);
                Navigator.FireEvent(ViewType.InfoHotelView, dto);
            }
            
        }

        private void DeleteDialog()
        {
            if(SelectedHotel != null)
            {
                var yesNoDialog = new YesNoDialogViewModel("Confirm Deletion", "Are you sure you want to delete this hotel");
                _dialogService.ShowDialog(yesNoDialog, result =>
                {
                    if (result == null)
                    {
                        return;
                    }
                    if ((bool)result)
                    {
                        OkDialogViewModel okDialog;
                        try
                        {
                            _hotelService.Delete(SelectedHotel);
                            okDialog = new OkDialogViewModel("Message", "Successfuly deleted");
                            _hotels.Remove(SelectedHotel);
                            OnPropertyChanged(nameof(Hotels));
                        }
                        catch (Exception e)
                        {
                            okDialog = new OkDialogViewModel("Message", e.Message);
                        }
                        _dialogService.ShowDialog(okDialog, result => { }, true);
                    }
                }, true);
            }
            
        }







        public HotelManagementViewModel(IHotelService hotelService, IDialogService dialogService)
        {
            CreateCommand = new RelayCommand(obj => NavigateToCreateHotelView());
            UpdateCommand = new RelayCommand(obj => NavigateToUpdateHotelView());
            DetailsCommand = new RelayCommand(obj => NavigateToDetailsHotelView());
            DeleteCommand = new RelayCommand(obj => DeleteDialog());
            _hotelService = hotelService;
            _dialogService = dialogService;
            _hotels = new ObservableCollection<Hotel>();
            var hotels = hotelService.GetAll().ToList();
            foreach (var hotel in hotels)
            {
                _hotels.Add(hotel);
            }
        }

        
    }
}
