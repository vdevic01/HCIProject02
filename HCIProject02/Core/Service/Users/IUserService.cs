using HCIProject02.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Service.Users
{
    public interface IUserService
    {
        public User Create(User user);
    }
}
