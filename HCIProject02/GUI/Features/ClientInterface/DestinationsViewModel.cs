using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.Core.Service.Users;
using HCIProject02.GUI.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.GUI.Features.ClientInterface
{
    public class DestinationsViewModel : ViewModelBase
    {
        private readonly IArrangementService arrangementService;

        private List<Arrangement> _arrangements;
        public List<Arrangement> Arrangements
        {
            get => _arrangements;
            set
            {
                _arrangements = value;
                OnPropertyChanged(nameof(Arrangements));
            }
        }
        public DestinationsViewModel(IArrangementService arrangementService)
        {
            this.arrangementService = arrangementService;
            Arrangements = arrangementService.GetAll();
        }
    }
}
