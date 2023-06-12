using HCIProject02.Core.Persistance;
using HCIProject02.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Repository
{
    public class CrudRepository<T> : ICrudRepository<T> where T : BaseObservableEntity
    {
        protected DatabaseContext _context;
        protected DbSet<T> _entities;

        public CrudRepository(DatabaseContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }


        public virtual T Create(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public virtual T? Update(T entity)
        {
            var entityToUpdate = Get(entity.Id);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                _context.SaveChanges();
                return entityToUpdate;
            }

            return null;
        }

        public virtual T? Delete(Guid id)
        {
            var entityToDelete = Get(id);
            if (entityToDelete != null)
            {
                entityToDelete.IsActive = false;
                Update(entityToDelete);
                return entityToDelete;
            }
            return null;
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.Where(ent => ent.IsActive).ToList();
        }

        public T? Get(Guid id)
        {
            return _entities.Where(ent => ent.IsActive).FirstOrDefault(e => e.Id == id);
        }
    }
}
