using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.GUI.Dialog;
using HCIProject02.GUI.Dialog.Implementations;
using HCIProject02.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.AgentInterface.Arrangements
{
    internal class UpdateArrangementViewModel : ViewModelBase
    {
        #region Properties

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

        private string? _errorMessage;
        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        #endregion

        #region Commands
        public ICommand UpdateArrangementCommand { get; }
        #endregion

        #region Services
        private IArrangementService arrangementService;
        private readonly IDialogService _dialogService;
        #endregion


        private void UpdateArrangement()
        {
            
            if (string.IsNullOrEmpty(SelectedArrangement.Name))
            {
                ErrorMessage = "Field (Name) is required";
                return;
            }
            if (string.IsNullOrEmpty(SelectedArrangement.Description))
            {
                ErrorMessage = "Field (Description) is required";
                return;
            }
            if (string.IsNullOrEmpty(SelectedArrangement.TripPlan))
            {
                ErrorMessage = "Field (Trip Plan) is required";
                return;
            }
            if (string.IsNullOrEmpty(SelectedArrangement.ImagePath))
            {
                ErrorMessage = "Image of arrangement is required";
                return;
            }
            if (SelectedArrangement.Hotel == null)
            {
                ErrorMessage = "Field (Hotel) is required";
                return;
            }
            if (string.IsNullOrEmpty(SelectedArrangement.Price.ToString()) || SelectedArrangement.Price <= 0)
            {
                ErrorMessage = "Field (Price) is required";
                return;
            }



            var yesNoDialog = new YesNoDialogViewModel("Arrangement updating confirmation", "Are you sure you want to update this attraction ?");

            _dialogService.ShowDialog(yesNoDialog, result =>
            {
                if (result == null)
                {
                    return;
                }
                if ((bool)result)
                {

                    Arrangement? arrangement = arrangementService.Update(SelectedArrangement);
                    if (arrangement != null)
                    {
                        OkDialogViewModel okDialog = new OkDialogViewModel("Message", "Arrangement updated.");
                        _dialogService.ShowDialog(okDialog, result => { }, true);
                    }
                }
            }, true);



        }



        public UpdateArrangementViewModel(IArrangementService arrangementService, IDialogService dialogService)
        {
            this.arrangementService = arrangementService;
            _dialogService = dialogService;
            UpdateArrangementCommand = new RelayCommand(obj => UpdateArrangement());


        }
    }
}
