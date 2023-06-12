using HCIProject02.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Service.Travel
{
    public interface IArrangementService
    {
        public List<Arrangement> GetAll();
        public Arrangement Create(Arrangement arrangement);
        public Arrangement? Update(Arrangement arrangement);
        public Arrangement Delete(Arrangement arrangement);
        public Arrangement? GetArrangementByName(string name);
    }
}
