﻿using HCIProject02.GUI.ViewModel;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Ninject
{
    class ServiceLocator
    {
        private static IKernel _kernel = new StandardKernel(new ServiceModule());

        public static T Get<T>() => _kernel.Get<T>();

        public MainViewModel MainViewModel
        {
            get => _kernel.Get<MainViewModel>();
        }

        public static void Reset()
        {
            _kernel = new StandardKernel(new ServiceModule());
        }
    }
}
