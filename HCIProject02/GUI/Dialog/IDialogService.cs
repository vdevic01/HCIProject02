using HCIProject02.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.GUI.Dialog
{
    public interface IDialogService
    {
        public void ShowDialog(DialogViewModelBase dialogViewModel, Action<bool?> callback, bool isTrueDialog);
    }
}
