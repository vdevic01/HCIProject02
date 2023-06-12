using HCIProject02.Core.Model;
using HCIProject02.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.GUI.Features.AgentInterface.Arrangements.DragAndDrop
{
    public class AttractionItemViewModel : ViewModelBase
    {
        private Attraction _attractionItem;
        public Attraction AttractionItem
        {
            get
            {
                return _attractionItem;
            }
            set
            {
                _attractionItem = value;
                OnPropertyChanged(nameof(AttractionItem));
            }
        }

        public AttractionItemViewModel(Attraction attraction)
        {
            AttractionItem = attraction;
        }
    }
}
