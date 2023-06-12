using HCIProject02.Commands;
using HCIProject02.Core.Exceptions;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Users;
using HCIProject02.GUI.Dialog;
using HCIProject02.GUI.Dialog.Implementations;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.LoginAndRegister
{
    public class RegisterViewModel : ViewModelBase
    {
        

        #region Services
        private readonly IUserService userService;
        private readonly IDialogService _dialogService;
        #endregion

        #region Properties
        private string? _email;
        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string? _fristName;
        public string? FirstName
        {
            get => _fristName;
            set
            {
                _fristName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        private string? _lastName;
        public string? LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        private string? _password;
        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private string? _passwordConfirm;
        public string? PasswordConfirm
        {
            get => _passwordConfirm;
            set
            {
                _passwordConfirm = value;
                OnPropertyChanged(nameof(PasswordConfirm));
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
        public ICommand NavigateToLoginViewCommand { get; }

        public ICommand RegisterUserCommand { get; }


        #endregion
        private void NavigateToLoginView()
        {
            Navigator.FireEvent(ViewType.LoginView);
        }
        private void RegisterUser()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                ErrorMessage = "Field (First Name) is required";
                return;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                ErrorMessage = "Field (Last Name) is required";
                return;
            }
            if (string.IsNullOrEmpty(Email))
            {
                ErrorMessage = "Field (Email) is required";
                return;
            }
            try
            {
                var _ = new MailAddress(Email).Address;
            }
            catch (FormatException)
            {
                ErrorMessage = "Email address is invalid";
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Field (Password) is required";
                return;
            }
            if(!Regex.IsMatch(Password, "^(?=.*\\d).{6,}$"))
            {
                ErrorMessage = "Password must be at least 6 characters long\nand contains at least 1 digit";
                return;
            }
            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                ErrorMessage = "Field (Confirm Password) is required";
                return;
            }
            if(Password != PasswordConfirm)
            {
                ErrorMessage = "Passwords does not match";
                return;
            }
            User user = new Client { FirstName = FirstName, LastName = LastName, Password = Password, EmailAddress = Email };
            try
            {
                userService.Create(user);
            }
            catch (DuplicateEmailExcpetion)
            {
                ErrorMessage = "User with this email already exists";
                return;
            }

            OkDialogViewModel dialog = new OkDialogViewModel("Message", "Account created.");
            _dialogService.ShowDialog(dialog, result => { }, true);
            Navigator.FireEvent(ViewType.LoginView);
        }


        public RegisterViewModel(IUserService userService, IDialogService dialogService)

        {
            NavigateToLoginViewCommand = new RelayCommand(obj => NavigateToLoginView());
            RegisterUserCommand = new RelayCommand(obj => RegisterUser());
            this.userService = userService;
            _dialogService = dialogService;
        }
    }
}
