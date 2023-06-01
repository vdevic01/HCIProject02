using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
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
using System.Windows;  // Za Point klasu
using System.Windows.Controls;  // Za Label klasu
using Microsoft.Maps.MapControl.WPF;  // Za Map kontrolu i druge mape klase
using Microsoft.Maps.MapControl.WPF.Core;  // Za CredentialsProvider, Location, Pushpin klase
using Microsoft.EntityFrameworkCore.Metadata;
using System.Xml.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace HCIProject02.GUI.Features.ClientInterface
{
    /// <summary>
    /// Interaction logic for NewHotelView.xaml
    /// </summary>
    /// 

    public partial class NewHotelView : UserControl
    {

        private string mapKey { get; set; }

        public NewHotelView()
        {
            InitializeComponent();
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
