using HCIProject02.Core.Model;
using HCIProject02.Core.Ninject;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HCIProject02.GUI.Features.ClientInterface
{
    /// <summary>
    /// Interaction logic for UpdateHotelView.xaml
    /// </summary>
    public partial class UpdateHotelView : UserControl
    {
        private UpdateHotelViewModel viewModel { get; set; }
        private string mapKey { get; set; }

        public UpdateHotelView()
        {
            InitializeComponent();

            UpdateHotelViewModel viewModel = ServiceLocator.Get<UpdateHotelViewModel>();
            this.viewModel = viewModel;

            mapKey = ConfigurationManager.AppSettings["MapKey"];
            myMap.CredentialsProvider = new Microsoft.Maps.MapControl.WPF.ApplicationIdCredentialsProvider(mapKey);
            myMap.Center = new Location(44.7866, 20.4489);

        }




        private async void MapMouseClick(object sender, MouseButtonEventArgs e)
        {
            Point mousePosition = e.GetPosition(myMap);
            Location clickedLocation = myMap.ViewportPointToLocation(mousePosition);


            Pushpin pin = new Pushpin();
            pin.Location = clickedLocation;
            myMap.Children.Add(pin);
            if (this.viewModel.Hotel != null)
            {
                this.viewModel.Hotel.Latitude = clickedLocation.Latitude;
                this.viewModel.Hotel.Longitude = clickedLocation.Longitude;
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
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                string imagePath = files[0];

                ImagePath.ImageSource = new BitmapImage(new Uri(imagePath));



            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
           
                string imagePath = openFileDialog.FileName;

                ImagePath.ImageSource = new BitmapImage(new Uri(imagePath));


            }
        }
    }
    }
