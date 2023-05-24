using HCIProject02.Core.Model;
using HCIProject02.Core.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Repository.Users.Implementation
{
    public class UserRepository : CrudRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public User? FindUserByEmail(string email)
        {            
            return _entities.FirstOrDefault(user => user.EmailAddress == email);
        }
    }
}
