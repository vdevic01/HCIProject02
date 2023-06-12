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
    public class ArrangementManagementViewModel : ViewModelBase
    {
        #region Properites
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

        private ObservableCollection<Arrangement> _arrangements;
        public ObservableCollection<Arrangement> Arrangements
        {
            get => _arrangements;
            set
            {
                _arrangements = value;
                OnPropertyChanged(nameof(Arrangements));
            }
        }
        #endregion
        #region Services
        private readonly IArrangementService _arrangementService;
        private readonly IDialogService _dialogService;
        #endregion

        #region Commands
        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DetailsCommand { get; set; }
        #endregion

        private void DeleteDialog()
        {
            if (SelectedArrangement != null)
            {
                var yesNoDialog = new YesNoDialogViewModel("Confirm Deletion", "Are you sure you want to delete this arrangement");
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
                            _arrangementService.Delete(SelectedArrangement);
                            okDialog = new OkDialogViewModel("Message", "Successfuly deleted");
                            _arrangements.Remove(SelectedArrangement);
                            OnPropertyChanged(nameof(Arrangement));
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

        private void NavigateToCreateArrangementView()
        {
            Navigator.FireEvent(ViewType.NewArrangementView);
        }

        private void NavigateToDetailsArrangementView()
        {
            if (SelectedArrangement != null)
            {
                NavigatorEventDTO dto = new NavigatorEventDTO(SelectedArrangement, ViewType.ArrangementManagementView);
                Navigator.FireEvent(ViewType.ArrangementView, dto);
            }

        }
        private void NavigateToUpdateArrangementView()
        {
            if (SelectedArrangement != null)
            {
                NavigatorEventDTO dto = new NavigatorEventDTO(SelectedArrangement, ViewType.ArrangementManagementView);
                Navigator.FireEvent(ViewType.UpdateArrangementView, dto);
            }

        }

        public ArrangementManagementViewModel(IArrangementService arrangementService, IDialogService dialogService)
        {
            DetailsCommand = new RelayCommand(obj => NavigateToDetailsArrangementView());
            CreateCommand = new RelayCommand(obj => NavigateToCreateArrangementView());
            DeleteCommand = new RelayCommand(obj => DeleteDialog());
            UpdateCommand = new RelayCommand(obj => NavigateToUpdateArrangementView());
            _arrangementService = arrangementService;
            _dialogService = dialogService;
            _arrangements = new ObservableCollection<Arrangement>();
            var arrangements = _arrangementService.GetAll();
            foreach (var arrangement in arrangements)
            {
                _arrangements.Add(arrangement);
            }
        }

    }
}
