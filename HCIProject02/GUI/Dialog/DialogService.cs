using HCIProject02.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.GUI.Dialog
{
    public class DialogService : IDialogService
    {
        public void ShowDialog(DialogViewModelBase dialogViewModel, Action<bool?> callback, bool isTrueDialog)
        {
            var dialog = new DialogWindow()
            {
                DataContext = dialogViewModel,
            };

            EventHandler closeEventHandler = null;
            closeEventHandler = (s, e) =>
            {
                callback(dialog.DialogResult);
                dialog.Closed -= closeEventHandler;
            };
            dialog.Closed += closeEventHandler;

            if(isTrueDialog)
            {
                dialog.ShowDialog();
            }
            else
            {
                dialog.Show();
            }
        }
    }
}
