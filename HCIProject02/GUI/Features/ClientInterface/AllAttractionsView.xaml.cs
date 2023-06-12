using HCIProject02.HelpSystem;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCIProject02.GUI.Features.ClientInterface
{
    /// <summary>
    /// Interaction logic for AllAttractionsView.xaml
    /// </summary>
    public partial class AllAttractionsView : UserControl
    {
        public AllAttractionsView()
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
                    HelpProvider.ShowHelp("Destinations", Window.GetWindow(this));
                else
                    HelpProvider.ShowHelp(str, Window.GetWindow(this));
            }
            else
            {
                HelpProvider.ShowHelp("Destinations", Window.GetWindow(this));
            }
        }
    }
}
