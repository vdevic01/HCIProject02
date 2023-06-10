using HCIProject02.Core.Ninject;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace HCIProject02.GUI.Features.AgentInterface.Arrangements
{
    public partial class NewArrangementView : UserControl
    {
        private NewArrangementViewModel NewArrangementViewModel { get; set; }
        public NewArrangementView()
        {
            NewArrangementViewModel viewModel = ServiceLocator.Get<NewArrangementViewModel>();
            NewArrangementViewModel = viewModel;
            DataContext = viewModel;

            InitializeComponent();

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void OnImageDropped(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                string imagePath = files[0];
                this.NewArrangementViewModel.FilePath = imagePath;

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
                string imagePath = openFileDialog.FileName;
                this.NewArrangementViewModel.FilePath = imagePath;
                ImageBorder.Background = new ImageBrush(new BitmapImage(new Uri(imagePath)));
                ImageButton.Visibility = Visibility.Collapsed;
                DropText.Visibility = Visibility.Collapsed;

            }
        }
        

    }



}
