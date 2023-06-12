using HCIProject02.HelpSystem;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCIProject02.GUI.Features.AgentInterface
{
    /// <summary>
    /// Interaction logic for AgentNavigationView.xaml
    /// </summary>
    public partial class AgentNavigationView : UserControl
    {
        public AgentNavigationView()
        {
            InitializeComponent();
        }
        private bool isReversed = false;
        private void AgentNavigationButton_Click(object sender, RoutedEventArgs e)
        {
            var storyboard = (Storyboard)FindResource("AgentNavigationStoryboard");

            var marginFrom = new Thickness(0, 0, -150, 0);
            var marginTo = new Thickness(0);

            if (isReversed)
            {
                marginFrom = new Thickness(0);
                marginTo = new Thickness(0, 0, -150, 0);
            }

            var animation = (ThicknessAnimation)storyboard.Children[0];
            animation.From = marginFrom;
            animation.To = marginTo;

            storyboard.Begin(AgentNavigationPanel);

            isReversed = !isReversed;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, Window.GetWindow(this));
            }
            else
            {
                HelpProvider.ShowHelp("AgentNavigation", Window.GetWindow(this));
            }
        }
    }
}
