using HCIProject02.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Repository.Users
{
    public interface IUserRepository : ICrudRepository<User>
    {
        User? FindUserByEmail(string email);
    }
}
