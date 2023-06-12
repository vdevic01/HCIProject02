using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCIProject02.Utils;

namespace HCIProject02.Core.Repository
{
    public interface ICrudRepository<T> where T : IBaseEntity
    {
        IEnumerable<T> GetAll();
        T? Get(Guid id);
        T Create(T entity);
        T? Update(T entity);
        T? Delete(Guid id);
    }
}
