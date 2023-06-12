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

        public Arrangement Create(Arrangement arrangement)
        {
            return arrangementRepository.Create(arrangement);
        }

        public Arrangement Delete(Arrangement arrangement)
        {
            return arrangementRepository.Delete(arrangement.Id);
        }

        public List<Arrangement> GetAll()
        {
            return arrangementRepository.GetAll().ToList();
        }


        public Arrangement? Update(Arrangement arrangement)
        {
            return arrangementRepository.Update(arrangement);
        }
        public Arrangement? GetArrangementByName(string name)
        {
            return this.arrangementRepository.FindArrangementByName(name);
        }
    }
}
