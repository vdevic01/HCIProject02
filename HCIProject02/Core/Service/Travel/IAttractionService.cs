using HCIProject02.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Service.Travel
{
    public interface IAttractionService
    {

        public Attraction Create(Attraction attraction);
        public Attraction? Update(Attraction attraction);
        public Attraction? GetAttractionByName(string name);
    }
}
