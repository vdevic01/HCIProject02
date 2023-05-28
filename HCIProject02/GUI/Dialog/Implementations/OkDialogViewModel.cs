using HCIProject02.Commands;
using HCIProject02.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HCIProject02.GUI.Dialog.Implementations
{
    class OkDialogViewModel : DialogViewModelBase
    {
        private string _message;
        public string Message
        {
            get => _message;
            set { OnPropertyChanged(ref _message, value); }
        }
        public ICommand OkButtonCommand { get; }
        public OkDialogViewModel(string title, string message) : base(title, 300, 175, ResizeMode.NoResize)
        {
            _message = message;
            OkButtonCommand = new RelayCommand(obj =>
            {
                var dialog = (DialogWindow)obj;
                dialog.DialogResult = true;
                dialog.Close();
            });
        }
    }
}
