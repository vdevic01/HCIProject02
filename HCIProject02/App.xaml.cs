using HCIProject02.Core.Persistance;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HCIProject02
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)

        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("log.txt")
            .CreateLogger();
            using (DatabaseContext db = new DatabaseContext(0))
            {
              //  DatabaseContextSeed.Seed(db);
            }

           
        }
    }
}
