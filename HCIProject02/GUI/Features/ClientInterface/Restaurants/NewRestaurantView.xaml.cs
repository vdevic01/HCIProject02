using HCIProject02.Core.Ninject;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
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
    /// Interaction logic for NewRestaurantView.xaml
    /// </summary>
    public partial class NewRestaurantView : UserControl
    {

        private string mapKey { get; set; }
        private NewRestaurantViewModel newRestaurantViewModel { get; set; }
        public NewRestaurantView()
        {
            InitializeComponent();

            NewRestaurantViewModel viewModel = ServiceLocator.Get<NewRestaurantViewModel>();
            this.newRestaurantViewModel = viewModel;
            DataContext = viewModel;
            mapKey = ConfigurationManager.AppSettings["MapKey"];
            myMap.CredentialsProvider = new Microsoft.Maps.MapControl.WPF.ApplicationIdCredentialsProvider(mapKey);
            myMap.Center = new Location(44.7866, 20.4489);  // Koordinate za Beograd
            myMap.ZoomLevel = 12;  // Nivo zumiranja za prikaz Beograda
        }

        private async void MapMouseClick(object sender, MouseButtonEventArgs e)
        {

            Point mousePosition = e.GetPosition(myMap);
            Location clickedLocation = myMap.ViewportPointToLocation(mousePosition);


            Pushpin pin = new Pushpin();
            pin.Location = clickedLocation;
            myMap.Children.Add(pin);
            newRestaurantViewModel.Latitude = clickedLocation.Latitude;
            newRestaurantViewModel.Longitude = clickedLocation.Longitude;

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
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Assuming you only want to handle a single image drop, you can access the first file
                string imagePath = files[0];
                newRestaurantViewModel.FilePath = imagePath;

                // Postavite izvor slike na dodatu sliku
                ImageBorder.Background = new ImageBrush(new BitmapImage(new Uri(imagePath)));
                ImageButton.Visibility = Visibility.Collapsed;
                DropText.Visibility = Visibility.Collapsed;


            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
                // Dobijte putanju do izabrane slike
                string imagePath = openFileDialog.FileName;

                // Postavite izvor slike na odabranu putanju
                //((Image)((Button)sender).Content).Source = new BitmapImage(new Uri(imagePath));
                ImageBorder.Background = new ImageBrush(new BitmapImage(new Uri(imagePath)));
                ImageButton.Visibility = Visibility.Collapsed;
                DropText.Visibility = Visibility.Collapsed;

            }
        }
    }
}
