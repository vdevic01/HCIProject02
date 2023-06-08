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

namespace HCIProject02.GUI.Features.AgentInterface.Arrangements.DragAndDrop
{
    /// <summary>
    /// Interaction logic for AttractionListingView.xaml
    /// </summary>
    public partial class AttractionListingView : UserControl
    {
        public static readonly DependencyProperty IncomingAttractionItemProperty =
            DependencyProperty.Register("IncomingAttractionItem", typeof(object), typeof(AttractionListingView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object IncomingAttractionItem
        {
            get { return (object)GetValue(IncomingAttractionItemProperty); }
            set { SetValue(IncomingAttractionItemProperty, value); }
        }

        public static readonly DependencyProperty RemovedAttractionItemProperty =
            DependencyProperty.Register("RemovedAttractionItem", typeof(object), typeof(AttractionListingView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object RemovedAttractionItem
        {
            get { return (object)GetValue(RemovedAttractionItemProperty); }
            set { SetValue(RemovedAttractionItemProperty, value); }
        }

        public static readonly DependencyProperty AttractionItemDropCommandProperty =
            DependencyProperty.Register("AttractionItemDropCommand", typeof(ICommand), typeof(AttractionListingView),
                new PropertyMetadata(null));

        public ICommand AttractionItemDropCommand
        {
            get { return (ICommand)GetValue(AttractionItemDropCommandProperty); }
            set { SetValue(AttractionItemDropCommandProperty, value); }
        }

        public static readonly DependencyProperty AttractionItemRemovedCommandProperty =
            DependencyProperty.Register("AttractionItemRemovedCommand", typeof(ICommand), typeof(AttractionListingView),
                new PropertyMetadata(null));

        public ICommand AttractionItemRemovedCommand
        {
            get { return (ICommand)GetValue(AttractionItemRemovedCommandProperty); }
            set { SetValue(AttractionItemRemovedCommandProperty, value); }
        }

        public static readonly DependencyProperty AttractionItemInsertedCommandProperty =
            DependencyProperty.Register("AttractionItemInsertedCommand", typeof(ICommand), typeof(AttractionListingView),
                new PropertyMetadata(null));

        public ICommand AttractionItemInsertedCommand
        {
            get { return (ICommand)GetValue(AttractionItemInsertedCommandProperty); }
            set { SetValue(AttractionItemInsertedCommandProperty, value); }
        }

        public static readonly DependencyProperty InsertedAttractionItemProperty =
            DependencyProperty.Register("InsertedAttractionItem", typeof(object), typeof(AttractionListingView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object InsertedAttractionItem
        {
            get { return (object)GetValue(InsertedAttractionItemProperty); }
            set { SetValue(InsertedAttractionItemProperty, value); }
        }

        public static readonly DependencyProperty TargetAttractionItemProperty =
            DependencyProperty.Register("TargetAttractionItem", typeof(object), typeof(AttractionListingView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object TargetAttractionItem
        {
            get { return (object)GetValue(TargetAttractionItemProperty); }
            set { SetValue(TargetAttractionItemProperty, value); }
        }

        public AttractionListingView()
        {
            InitializeComponent();
        }

        private void AttractionItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed &&
                sender is FrameworkElement frameworkElement)
            {
                object attractionItem = frameworkElement.DataContext;

                DragDropEffects dragDropResult = DragDrop.DoDragDrop(frameworkElement,
                    new DataObject(DataFormats.Serializable, attractionItem),
                    DragDropEffects.Move);

                if (dragDropResult == DragDropEffects.None)
                {
                    AddAttractionItem(attractionItem);
                }
            }
        }

        private void AttractionItem_DragOver(object sender, DragEventArgs e)
        {
            if (AttractionItemInsertedCommand?.CanExecute(null) ?? false)
            {
                if (sender is FrameworkElement element)
                {
                    TargetAttractionItem = element.DataContext;
                    InsertedAttractionItem = e.Data.GetData(DataFormats.Serializable);

                    AttractionItemInsertedCommand?.Execute(null);
                }
            }
        }

        private void AttractionItemList_DragOver(object sender, DragEventArgs e)
        {
            object attractionItem = e.Data.GetData(DataFormats.Serializable);
            AddAttractionItem(attractionItem);
        }

        private void AddAttractionItem(object attractionItem)
        {
            if (AttractionItemDropCommand?.CanExecute(null) ?? false)
            {
                IncomingAttractionItem = attractionItem;
                AttractionItemDropCommand?.Execute(null);
            }
        }

        private void AttractionItemList_DragLeave(object sender, DragEventArgs e)
        {
            HitTestResult result = VisualTreeHelper.HitTest(lvItems, e.GetPosition(lvItems));

            if (result == null)
            {
                if (AttractionItemRemovedCommand?.CanExecute(null) ?? false)
                {
                    RemovedAttractionItem = e.Data.GetData(DataFormats.Serializable);
                    AttractionItemRemovedCommand?.Execute(null);
                }
            }
        }
    }
}
