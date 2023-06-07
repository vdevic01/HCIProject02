using HCIProject02.Core.Model;
using HCIProject02.Core.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Repository.Travel.Implementation
{
    public class ArrangementRepository : CrudRepository<Arrangement>, IArrangementRepository
    {
        public ArrangementRepository(DatabaseContext context) : base(context)
        {
        }

        public Arrangement? FindArrangementByName(string name)
        {
            return _entities.FirstOrDefault(arrangement => arrangement.Name == name);
        }
    }
}
