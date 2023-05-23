using HCIProject02.GUI.Features.LoginAndRegister;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Ninject
{
    class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<LoginViewModel>().To<LoginViewModel>();
        }
    }
}
