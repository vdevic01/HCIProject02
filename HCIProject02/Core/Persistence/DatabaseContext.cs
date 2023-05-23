using HCIProject02.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Persistance
{
    internal class DatabaseContext : DbContext
    {
        public string DbPath { get; }
        private static string filename = "travelagency.db";

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Agent> Agents { get; set; }

        public DatabaseContext()
        {
            var folder = Directory.GetCurrentDirectory();
            DbPath = Path.Join(folder, "Core\\Persistence");
            DbPath = Path.Join(DbPath, filename);
        }

        public DatabaseContext(int dummyArg)
        {
            var folder = Directory.GetCurrentDirectory();
            DbPath = Path.Join(folder, "..\\..\\..\\Core/Persistence");
            DbPath = Path.Join(DbPath, filename);
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            var folder = Environment.SpecialFolder.Desktop;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, filename);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseLazyLoadingProxies().UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
