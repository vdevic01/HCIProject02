using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Users;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.LoginAndRegister
{
    public class LoginViewModel : ViewModelBase
    {
        #region Commands
        public ICommand NavigateToRegisterViewCommand { get; }
        public ICommand LoginUserCommand { get; }
        #endregion

        #region Properties
        private string? _email;
        public string? Email
        {
            get => _email;
            set
            {
                _email= value;
                OnPropertyChanged(nameof(Email));
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

        #region Services
        private readonly IUserService userService;
        #endregion
        private void LoginUser()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Field (Email) is required";
                return;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Field (Password) is required";
                return;
            }
            User? user = userService.Authenticate(Email, Password);
            if(user == null)
            {
                ErrorMessage = "Email or Password is not correct";
                return;
            }
            Navigator.FireEvent(ViewType.ClientHome);
        }

        private void NavigateToRegisterView()
        {
            Navigator.FireEvent(ViewType.RegisterView);
        }
        public LoginViewModel(IUserService userService)
        {
            this.userService = userService;
            NavigateToRegisterViewCommand = new RelayCommand(obj => NavigateToRegisterView());
            LoginUserCommand = new RelayCommand(obj => LoginUser());
        }
    }
}
