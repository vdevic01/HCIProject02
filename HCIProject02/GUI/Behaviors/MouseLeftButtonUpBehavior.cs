using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProject02.GUI.Behaviors
{
    public static class MouseLeftButtonUpBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(MouseLeftButtonUpBehavior),
                new PropertyMetadata(null, OnCommandPropertyChanged));

        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        private static void OnCommandPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is Control control)
            {
                if (e.OldValue != null)
                {
                    control.MouseLeftButtonUp -= Control_MouseLeftButtonUp;
                }

                if (e.NewValue != null)
                {
                    control.MouseLeftButtonUp += Control_MouseLeftButtonUp;
                }
            }
        }

        private static void Control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Control control && control.DataContext is ICommand command)
            {
                if (command.CanExecute(null))
                {
                    command.Execute(null);
                }
            }
        }
    }
}
