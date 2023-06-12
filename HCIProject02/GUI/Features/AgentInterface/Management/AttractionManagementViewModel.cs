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
    public class AttractionManagementViewModel : ViewModelBase
    {
        #region Properties
        private Attraction _selectedAttraction;
        public Attraction SelectedAttraction
        {
            get => _selectedAttraction;
            set
            {
                _selectedAttraction = value;
                OnPropertyChanged(nameof(SelectedAttraction));
            }
        }

        private ObservableCollection<Attraction> _attractions;
        public ObservableCollection<Attraction> Attractions
        {
            get => _attractions;
            set
            {
                _attractions = value;
                OnPropertyChanged(nameof(Attraction));
            }
        }
        #endregion
        #region Services
        private readonly IAttractionService _attractionService;
        private readonly IDialogService _dialogService;
        #endregion

        #region Commands
        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DetailsCommand { get; set; }
        public object ReturnButtonVisibility { get; private set; }
        #endregion


        private void NavigateToCreateAttractionView()
        {
            Navigator.FireEvent(ViewType.NewAttractionView);
        }
        private void NavigateToUpdateAttractionView()
        {
            if (SelectedAttraction != null)
            {
                NavigatorEventDTO dto = new NavigatorEventDTO(SelectedAttraction, ViewType.AttractionManagementView);
                Navigator.FireEvent(ViewType.UpdateAttractionView, dto);
            }

        }
        private void NavigateToDetailsAttractionView()
        {
            if (SelectedAttraction != null)
            {
                NavigatorEventDTO dto = new NavigatorEventDTO(SelectedAttraction, ViewType.AttractionManagementView);
                Navigator.FireEvent(ViewType.InfoAttractionView, dto);
            }

        }

        private void DeleteDialog()
        {
            if (SelectedAttraction != null)
            {
                var yesNoDialog = new YesNoDialogViewModel("Confirm Deletion", "Are you sure you want to delete this attraction");
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
                            _attractionService.Delete(SelectedAttraction);
                            okDialog = new OkDialogViewModel("Message", "Successfuly deleted");
                            _attractions.Remove(SelectedAttraction);
                            OnPropertyChanged(nameof(Attraction));
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


        public AttractionManagementViewModel(IAttractionService attractionService, IDialogService dialogService)
        {
            CreateCommand = new RelayCommand(obj => NavigateToCreateAttractionView());
            UpdateCommand = new RelayCommand(obj => NavigateToUpdateAttractionView());
            DetailsCommand = new RelayCommand(obj => NavigateToDetailsAttractionView());
            DeleteCommand = new RelayCommand(obj => DeleteDialog());
            _attractionService = attractionService;
            _dialogService = dialogService;
            _attractions = new ObservableCollection<Attraction>();
            var hotels = attractionService.GetAll().ToList();
            foreach (var hotel in hotels)
            {
                _attractions.Add(hotel);
            }
        }
    }

}
