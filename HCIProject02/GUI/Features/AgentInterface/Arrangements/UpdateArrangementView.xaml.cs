
using HCIProject02.Core.Ninject;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace HCIProject02.GUI.Features.AgentInterface.Arrangements
{
    /// <summary>
    /// Interaction logic for UpdateArrangementView.xaml
    /// </summary>
    public partial class UpdateArrangementView : UserControl
    {
        
        public UpdateArrangementView()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void OnImageDropped(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop) && DataContext is UpdateArrangementViewModel)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                string imagePath = files[0];
                var dataContext = DataContext as UpdateArrangementViewModel;
                dataContext.SelectedArrangement.ImagePath = imagePath;

                ImageBorder.Background = new ImageBrush(new BitmapImage(new Uri(imagePath)));
                ImageButton.Visibility = Visibility.Collapsed;
                /*DropText.Visibility = Visibility.Collapsed;*/


            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == true && DataContext is UpdateArrangementViewModel)
            {
                string imagePath = openFileDialog.FileName;
                var dataContext = DataContext as UpdateArrangementViewModel;
                dataContext.SelectedArrangement.ImagePath = imagePath;
                ImageBorder.Background = new ImageBrush(new BitmapImage(new Uri(imagePath)));
                ImageButton.Visibility = Visibility.Collapsed;
                /*DropText.Visibility = Visibility.Collapsed;*/

            }
        }


    }
}
