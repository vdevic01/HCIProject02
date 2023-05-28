using HCIProject02.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HCIProject02.GUI.ViewModel
{
    public class DialogViewModelBase : ObservableEntity
    {
        public string Title { get; set; }

        private double _windowWidth;
        public double WindowWidth
        {
            get => _windowWidth;
            set
            {
                OnPropertyChanged(ref _windowWidth, value);
            }
        }

        private double _windowHeight;
        public double WindowHeight
        {
            get => _windowHeight;
            set
            {
                OnPropertyChanged(ref _windowHeight, value);
            }
        }

        private ResizeMode _resizeMode;
        public ResizeMode ResizeMode
        {
            get => _resizeMode;
            set
            {
                OnPropertyChanged(ref _resizeMode, value);
            }
        }

        public DialogViewModelBase(string title, double windowWidth, double windowHeight, ResizeMode resizeMode)
        {
            ResizeMode = resizeMode;
            Title = title;
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
        }
    }
}
