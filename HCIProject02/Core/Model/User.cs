using HCIProject02.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Model
{
    public abstract class User : BaseObservableEntity
    {

        #region Properties        
        private string _emailAddress;
        public string EmailAddress { get => _emailAddress; set => OnPropertyChanged(ref _emailAddress, value); }

        private string _password;
        public string Password { get => _password; set => OnPropertyChanged(ref _password, value); }

        private string _firstName;
        public string FirstName { get => _firstName; set => OnPropertyChanged(ref _firstName, value); }

        private string _lastName;
        public string LastName { get => _lastName; set => OnPropertyChanged(ref _lastName, value); }
        public string FullName => $"{_firstName} {_lastName}";

        private Role _role;
        public Role Role { get => _role; set => OnPropertyChanged(ref _role, value); }
        #endregion

        #region Constructors
        public User() { }

        public User(User other) : base(other) {
            EmailAddress = other.EmailAddress;
            Password = other.Password;
            FirstName = other.FirstName;
            LastName = other.LastName;
            Role = other.Role;
        }
        #endregion
    }
}
