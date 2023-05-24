using HCIProject02.Core.Model;
using HCIProject02.Core.Repository.Users;
using HCIProject02.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Service.Users.Implementation
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        private string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashWithSalt = new byte[36];
            Array.Copy(salt, 0, hashWithSalt, 0, 16);
            Array.Copy(hash, 0, hashWithSalt, 16, 20);
            string passwordHash = Convert.ToBase64String(hash);
            return passwordHash;
        }

        private bool IsValidPassword(string password, string hash)
        {
            byte[] hashBytes = Convert.FromBase64String(hash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hashCheck = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hashCheck[i])
                {
                    return false;
                }
            }
            return true;
        }

        public User Create(User user)
        {
            User duplicate = userRepository.FindUserByEmail(user.EmailAddress);
            if (duplicate != null)
            {
                throw new DuplicateEmailExcpetion();
            }
            string hash = HashPassword(user.Password);
            user.Password = hash;
            return userRepository.Create(user);
        }

        public User? Authenticate(string email, string password)
        {
            User? user = userRepository.FindUserByEmail(email);
            if (user == null)
            {
                return null;
            }
            if(!IsValidPassword(password, user.Password))
            {
                return null;
            }
            return user;
        }
    }
}
