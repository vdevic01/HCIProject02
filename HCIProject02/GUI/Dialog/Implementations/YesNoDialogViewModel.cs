using HCIProject02.Commands;
using HCIProject02.GUI.Dialog;
using HCIProject02.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCIProject02.GUI.Dialog.Implementations
{
    public class YesNoDialogViewModel : DialogViewModelBase
    {
        private string _message;
        public string Message
        {
            get => _message;
            set { OnPropertyChanged(ref _message, value);}
        }
        public ICommand YesButtonCommand { get; }
        public ICommand NoButtonCommand { get; }
        public YesNoDialogViewModel(string title, string message) : base(title, 300, 175, System.Windows.ResizeMode.NoResize)
        {
            _message = message;
            YesButtonCommand = new RelayCommand(obj =>
            {
                var dialog = (DialogWindow)obj;
                dialog.DialogResult = true;
                dialog.Close();
            });
            NoButtonCommand = new RelayCommand(obj =>
            {
                var dialog = (DialogWindow)obj;
                dialog.DialogResult = false;
                dialog.Close();
            });
        }
    }
}
