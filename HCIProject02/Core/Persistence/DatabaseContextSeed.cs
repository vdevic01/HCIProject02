using HCIProject02.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Persistance
{
    internal class DatabaseContextSeed
    {
        public static void Seed(DatabaseContext context)
        {
            var client01 = new Client { FirstName = "Vlada", LastName = "Devic", EmailAddress = "example@email.com", Password = "test123" };
            context.Users.Add(client01);
            context.SaveChanges();
        }
    }
}
