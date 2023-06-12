using HCIProject02.GUI.Features.AgentInterface.Arrangements.DragAndDrop.Commands;
using HCIProject02.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.AgentInterface.Arrangements.DragAndDrop
{
    public class AttractionListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<AttractionItemViewModel> _attractionItemViewModels;

        public IEnumerable<AttractionItemViewModel> AttractionItemViewModels => _attractionItemViewModels;

        private AttractionItemViewModel _incomingAttractionItemViewModel;
        public AttractionItemViewModel IncomingAttractionItemViewModel
        {
            get
            {
                return _incomingAttractionItemViewModel;
            }
            set
            {
                _incomingAttractionItemViewModel = value;
                OnPropertyChanged(nameof(IncomingAttractionItemViewModel));
            }
        }

        private AttractionItemViewModel _removedAttractionItemViewModel;
        public AttractionItemViewModel RemovedAttractionItemViewModel
        {
            get
            {
                return _removedAttractionItemViewModel;
            }
            set
            {
                _removedAttractionItemViewModel = value;
                OnPropertyChanged(nameof(RemovedAttractionItemViewModel));
            }
        }

        private AttractionItemViewModel _insertedAttractionItemViewModel;
        public AttractionItemViewModel InsertedAttractionItemViewModel
        {
            get
            {
                return _insertedAttractionItemViewModel;
            }
            set
            {
                _insertedAttractionItemViewModel = value;
                OnPropertyChanged(nameof(InsertedAttractionItemViewModel));
            }
        }

        private AttractionItemViewModel _targetAttractionItemViewModel;
        public AttractionItemViewModel TargetAttractionItemViewModel
        {
            get
            {
                return _targetAttractionItemViewModel;
            }
            set
            {
                _targetAttractionItemViewModel = value;
                OnPropertyChanged(nameof(TargetAttractionItemViewModel));
            }
        }

        public ICommand AttractionItemReceivedCommand { get; }
        public ICommand AttractionItemRemovedCommand { get; }
        public ICommand AttractionItemInsertedCommand { get; }

        public AttractionListingViewModel()
        {
            _attractionItemViewModels = new ObservableCollection<AttractionItemViewModel>();

            AttractionItemReceivedCommand = new AttractionItemReceivedCommand(this);
            AttractionItemRemovedCommand = new AttractionItemRemovedCommand(this);
            AttractionItemInsertedCommand = new AttractionItemInsertedCommand(this);
        }

        public void AddAttractionItem(AttractionItemViewModel item)
        {
            if (!_attractionItemViewModels.Contains(item))
            {
                _attractionItemViewModels.Add(item);
            }
        }

        public void InsertAttractionItem(AttractionItemViewModel insertedAttractionItem, AttractionItemViewModel targetAttractionItem)
        {
            if (insertedAttractionItem == targetAttractionItem)
            {
                return;
            }

            int oldIndex = _attractionItemViewModels.IndexOf(insertedAttractionItem);
            int nextIndex = _attractionItemViewModels.IndexOf(targetAttractionItem);

            if (oldIndex != -1 && nextIndex != -1)
            {
                _attractionItemViewModels.Move(oldIndex, nextIndex);
            }
        }

        public void RemoveAttractionItem(AttractionItemViewModel item)
        {
            _attractionItemViewModels.Remove(item);
        }
    }
}
