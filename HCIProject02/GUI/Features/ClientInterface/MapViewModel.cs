using HCIProject02.Core.Model;
using HCIProject02.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.GUI.Features.ClientInterface
{
    public class MapViewModel : ViewModelBase
    {
        #region Properties
        private User _authenticatedUser;
        public User AuthenticatedUser
        {
            get => _authenticatedUser;
            set
            {
                _authenticatedUser = value;
                Client client = (Client)_authenticatedUser;
            }
        }
        #endregion
        public MapViewModel() { }
    }
}
