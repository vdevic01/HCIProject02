using HCIProject02.Core.Model;
using HCIProject02.Core.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Repository.Travel.Implementation
{
    public class AttractionRepository : CrudRepository<Attraction>, IAttractionRepository
    {
        public AttractionRepository(DatabaseContext context) : base(context)
        {
        }

        public Attraction? FindAttractionByName(string name)
        {
            return _entities.FirstOrDefault(attraction => attraction.Name == name);
        }
    }
}
