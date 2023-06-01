using HCIProject02.Core.Persistance;
using HCIProject02.Core.Repository.Travel;
using HCIProject02.Core.Repository.Travel.Implementation;
using HCIProject02.Core.Repository.Users;
using HCIProject02.Core.Repository.Users.Implementation;
using HCIProject02.Core.Service.Travel;
using HCIProject02.Core.Service.Travel.Implementation;
using HCIProject02.Core.Service.Users;
using HCIProject02.Core.Service.Users.Implementation;
using HCIProject02.GUI.Dialog;
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
            Bind<DatabaseContext>().To<DatabaseContext>().InSingletonScope().WithConstructorArgument(0);

            #region Repository
            Bind(typeof(IUserRepository)).To(typeof(UserRepository));
            Bind(typeof(IArrangementRepository)).To(typeof(ArrangementRepository));
            Bind(typeof(IBookingRepository)).To(typeof(BookingRepository));
            Bind(typeof(IHotelRepository)).To(typeof(HotelRepository));
            #endregion

            #region Service
            Bind(typeof(IUserService)).To(typeof(UserService));
            Bind(typeof(IArrangementService)).To(typeof(ArrangementService));
            Bind(typeof(IDialogService)).To(typeof(DialogService));
            Bind(typeof(IBookingService)).To(typeof(BookingService));
            Bind(typeof(IHotelService)).To(typeof(HotelService));
            #endregion
        }
    }
}
