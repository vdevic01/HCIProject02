using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.GUI.Features.AgentInterface.Arrangements.DragAndDrop.Commands
{
    public class AttractionItemRemovedCommand : CommandBase
    {
        private readonly AttractionListingViewModel _attractionListingViewModel;

        public AttractionItemRemovedCommand(AttractionListingViewModel attractionListingViewModel)
        {
            _attractionListingViewModel = attractionListingViewModel;
        }

        public override void Execute(object parameter)
        {
            _attractionListingViewModel.RemoveAttractionItem(_attractionListingViewModel.RemovedAttractionItemViewModel);
        }
    }
}
