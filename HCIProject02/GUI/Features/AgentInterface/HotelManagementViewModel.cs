using HCIProject02.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.GUI.Features.AgentInterface
{
    public class HotelManagementViewModel
    {
        private User _authenticatedUser;
        public User AuthenticatedUser
        {
            get => _authenticatedUser;
            set
            {
                _authenticatedUser = value;
                Agent client = (Agent)_authenticatedUser;
            }
        }
        public HotelManagementViewModel()
        {

        }
    }
}
