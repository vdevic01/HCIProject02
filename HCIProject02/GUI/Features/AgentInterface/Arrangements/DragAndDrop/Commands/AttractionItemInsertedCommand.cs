using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.GUI.Features.AgentInterface.Arrangements.DragAndDrop.Commands
{
    public class AttractionItemInsertedCommand : CommandBase
    {
        private readonly AttractionListingViewModel _viewModel;

        public AttractionItemInsertedCommand(AttractionListingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.InsertAttractionItem(_viewModel.InsertedAttractionItemViewModel, _viewModel.TargetAttractionItemViewModel);
        }
    }
}
