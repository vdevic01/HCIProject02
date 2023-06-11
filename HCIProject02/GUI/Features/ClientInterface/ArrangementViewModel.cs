﻿using HCIProject02.Commands;
using HCIProject02.Core.Exceptions;
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
        #endregion

        #region Commands
        public ICommand BookCommand { get; }
        #endregion

        #region Services
        private readonly IDialogService _dialogService;
        private readonly IBookingService _bookingService;
        #endregion

        public ArrangementViewModel(IDialogService dialogService, IBookingService bookingService)
        {
            _bookingService = bookingService;
            _dialogService = dialogService;
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
