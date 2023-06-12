using HCIProject02.Core.Ninject;
using HCIProject02.HelpSystem;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
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

namespace HCIProject02.GUI.Features.ClientInterface.Restaurants
{
    /// <summary>
    /// Interaction logic for UpdateRestaurantView.xaml
    /// </summary>
    public partial class UpdateRestaurantView : UserControl
    {

        private string mapKey { get; set; }
        public UpdateRestaurantView()
        {
            InitializeComponent();

            mapKey = ConfigurationManager.AppSettings["MapKey"];
            myMap.CredentialsProvider = new Microsoft.Maps.MapControl.WPF.ApplicationIdCredentialsProvider(mapKey);
         

        }

        private async void MapMouseClick(object sender, MouseButtonEventArgs e)
        {
            Point mousePosition = e.GetPosition(myMap);
            Location clickedLocation = myMap.ViewportPointToLocation(mousePosition);


     

            if (myMap.DataContext is UpdateRestaurantViewModel viewModel)
            {
                // Pristupite viewModel.PinLocation i postavite vrijednost
                viewModel.PinLocation = clickedLocation;

                // Ažurirajte Pushpin lokaciju
                PushPin.Location = clickedLocation;
            }


            string requestUrl = $"https://dev.virtualearth.net/REST/v1/Locations/{clickedLocation.Latitude},{clickedLocation.Longitude}?key={mapKey}";

            using (HttpClient client = new HttpClient())
            {
                string responseString = await client.GetStringAsync(requestUrl);
                var responseJson = JObject.Parse(responseString);


                if (responseJson["statusDescription"].ToString() == "OK")
                {
                    var address = responseJson["resourceSets"][0]["resources"][0]["address"];
                    string formattedAddress = address["formattedAddress"].ToString();
                    addressTextBox.Text = formattedAddress;


                }
            }
        }

        private void OnImageDropped(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && DataContext is UpdateRestaurantViewModel)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                string imagePath = files[0];
                var dataContext = DataContext as UpdateRestaurantViewModel;
                dataContext.Restaurant.ImagePath = imagePath;
                imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
               


            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == true && DataContext is UpdateRestaurantViewModel)
            {

                string imagePath = openFileDialog.FileName;

                imageBrush.ImageSource = new BitmapImage(new Uri(imagePath));
                var dataContext = DataContext as UpdateRestaurantViewModel;
                dataContext.Restaurant.ImagePath = imagePath;

            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);

            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);

                if (str == "index")
                    HelpProvider.ShowHelp("UpdateRestaurant01", Window.GetWindow(this));
                else
                    HelpProvider.ShowHelp(str, Window.GetWindow(this));
            }
            else
            {
                HelpProvider.ShowHelp("UpdateRestaurant01", Window.GetWindow(this));
            }
        }

    }
}
