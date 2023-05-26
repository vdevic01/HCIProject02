using HCIProject02.Core.Model;
using HCIProject02.Core.Repository.Travel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Service.Travel.Implementation
{
    public class ArrangementService : IArrangementService
    {
        private readonly IArrangementRepository arrangementRepository;

        public ArrangementService(IArrangementRepository arrangementRepository)
        {
            this.arrangementRepository = arrangementRepository;
        }

        public List<Arrangement> GetAll()
        {
            return arrangementRepository.GetAll().ToList();
        }
    }
}
