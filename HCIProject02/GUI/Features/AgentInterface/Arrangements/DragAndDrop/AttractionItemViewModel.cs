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
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public AttractionItemViewModel(string description)
        {
            Description = description;
        }
    }
}
