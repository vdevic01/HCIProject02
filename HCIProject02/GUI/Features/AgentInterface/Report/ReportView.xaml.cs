using HCIProject02.HelpSystem;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.AgentInterface.Report
{
    public partial class ReportView : UserControl
    { 

        public ReportView()
        {
            InitializeComponent();
           
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                if (str == "index")
                    HelpProvider.ShowHelp("Reports01", Window.GetWindow(this));
                else
                    HelpProvider.ShowHelp(str, Window.GetWindow(this));
            }
            else
            {
                HelpProvider.ShowHelp("Reports01", Window.GetWindow(this));
            }
        }





    }
}
