using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.Dialog;
using HCIProject02.GUI.Dialog.Implementations;
using HCIProject02.GUI.DTO;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.AgentInterface.Management
{
    public class RestaurantManagementViewModel : ViewModelBase
    {
        #region Properties
        private Restaurant _selectedRestaurant;
        public Restaurant SelectedRestaurant
        {
            get => _selectedRestaurant;
            set
            {
                _selectedRestaurant = value;
                OnPropertyChanged(nameof(SelectedRestaurant));
            }
        }

        private ObservableCollection<Restaurant> _restaurants;
        public ObservableCollection<Restaurant> Restaurants
        {
            get => _restaurants;
            set
            {
                _restaurants = value;
                OnPropertyChanged(nameof(Restaurant));
            }
        }
        #endregion
        #region Services
        private readonly IRestaurantService _restaurantService;
        private readonly IDialogService _dialogService;
        #endregion

        #region Commands
        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DetailsCommand { get; set; }
        public object ReturnButtonVisibility { get; private set; }
        #endregion


        private void NavigateToCreateRestaurantView()
        {
            Navigator.FireEvent(ViewType.NewRestaurantView);
        }
        private void NavigateToUpdateRestaurantView()
        {
            if (SelectedRestaurant != null)
            {
                NavigatorEventDTO dto = new NavigatorEventDTO(SelectedRestaurant, ViewType.RestaurantManagementView);
                Navigator.FireEvent(ViewType.UpdateRestaurantView, dto);
            }

        }
        private void NavigateToDetailsRestaurantView()
        {
            if (SelectedRestaurant != null)
            {
                NavigatorEventDTO dto = new NavigatorEventDTO(SelectedRestaurant, ViewType.RestaurantManagementView);
                Navigator.FireEvent(ViewType.InfoRestaurantView, dto);
            }

        }

        private void DeleteDialog()
        {
            if (SelectedRestaurant != null)
            {
                var yesNoDialog = new YesNoDialogViewModel("Confirm Deletion", "Are you sure you want to delete this restaurant");
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
                            _restaurantService.Delete(SelectedRestaurant);
                            okDialog = new OkDialogViewModel("Message", "Successfuly deleted");
                            _restaurants.Remove(SelectedRestaurant);
                            OnPropertyChanged(nameof(Restaurant));
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


        public RestaurantManagementViewModel(IRestaurantService restaurantService, IDialogService dialogService)
        {
            CreateCommand = new RelayCommand(obj => NavigateToCreateRestaurantView());
            UpdateCommand = new RelayCommand(obj => NavigateToUpdateRestaurantView());
            DetailsCommand = new RelayCommand(obj => NavigateToDetailsRestaurantView());
            DeleteCommand = new RelayCommand(obj => DeleteDialog());
            _restaurantService = restaurantService;
            _dialogService = dialogService;
            _restaurants = new ObservableCollection<Restaurant>();
            var restaurants = restaurantService.GetAll();
            foreach (var restaurant in restaurants)
            {
                _restaurants.Add(restaurant);
            }
        }
    }
}
