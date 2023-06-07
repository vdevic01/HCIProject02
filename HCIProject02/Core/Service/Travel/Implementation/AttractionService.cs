using HCIProject02.Core.Model;
using HCIProject02.Core.Repository;
using HCIProject02.Core.Repository.Travel;


namespace HCIProject02.Core.Service.Travel.Implementation
{
    public class AttractionService : IAttractionService
    {
        private readonly IAttractionRepository attractionRepository;

        public AttractionService(IAttractionRepository attractionRepository)
        {
            this.attractionRepository = attractionRepository;
        }

        public Attraction Create(Attraction attraction)
        {
            return this.attractionRepository.Create(attraction);
        }

        public Attraction? GetAttractionByName(string name)
        {
           return this.attractionRepository.FindAttractionByName(name);
        }

        public Attraction? Update(Attraction attraction)
        {
            return this.attractionRepository.Update(attraction);
        }
    }
}
