using HCIProject02.Commands;
using HCIProject02.Core.Model;
using HCIProject02.Core.Service.Travel;
using HCIProject02.Core.Service.Users;
using HCIProject02.GUI.ViewModel;
using HCIProject02.Navigation;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCIProject02.GUI.Features.ClientInterface
{
    public class ArrangementCardDTO
    {
        public Arrangement Arrangement { get; set; }
        public ICommand BookCommand { get; set; }

        public ArrangementCardDTO(Arrangement arrangement, ICommand bookCommand)
        {
            Arrangement = arrangement;
            BookCommand = bookCommand;
        }
    }
    public class DestinationsViewModel : ViewModelBase
    {
        private readonly IArrangementService arrangementService;

        private List<ArrangementCardDTO> _arrangements;
        public List<ArrangementCardDTO> Arrangements
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
            _arrangements = arrangementService.GetAll().Select(arrangement =>
            {
                ICommand command = new RelayCommand(obj =>
                {
                    Navigator.FireEvent(ViewType.ArrangementView, arrangement);
                });
                ArrangementCardDTO card = new ArrangementCardDTO(arrangement, command);
                return card;
            }).ToList();
        }
    }
}
