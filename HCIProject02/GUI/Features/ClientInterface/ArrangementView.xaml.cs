using HCIProject02.Core.Model;
using HCIProject02.HelpSystem;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for ArrangementView.xaml
    /// </summary>
    public partial class ArrangementView : UserControl
    {

        private string mapKey { get; set; }

        public ArrangementView()
        {
            InitializeComponent();
            mapKey = ConfigurationManager.AppSettings["MapKey"];
            myMap.CredentialsProvider = new Microsoft.Maps.MapControl.WPF.ApplicationIdCredentialsProvider(mapKey);
            myMap.Center = new Location(44.7866, 20.4489);  // Koordinate za Beograd
            myMap.ZoomLevel = 12;  // Nivo zumiranja za prikaz Beograda
        }

        private void Pushpin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var pushpin = (Pushpin)sender;
            var selectedObject = pushpin.Tag;
            var dataContext = DataContext as ArrangementViewModel;
            if (selectedObject is Hotel)
            {
                dataContext.SelectedPoint = (Hotel)selectedObject;
            }
            else if (selectedObject is Attraction)
            {
                dataContext.SelectedPoint = (Attraction)selectedObject;
            }

        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                if (str == "index")
                    HelpProvider.ShowHelp("ArrangementInfo", Window.GetWindow(this));
                else
                    HelpProvider.ShowHelp(str, Window.GetWindow(this));
            }
            else
            {
                HelpProvider.ShowHelp("SignUp", Window.GetWindow(this));
            }
        }
    }
}
