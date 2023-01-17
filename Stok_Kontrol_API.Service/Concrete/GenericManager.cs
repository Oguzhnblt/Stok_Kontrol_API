using Stok_Kontrol_API.Entities.Entities;
using Stok_Kontrol_API.Repositories.Abstract;
using Stok_Kontrol_API.Service.Abstract;
using System.Linq.Expressions;

namespace Stok_Kontrol_API.Service.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : BaseEntity
    {
        private readonly IGenericRepository<T> repository;

        public GenericManager(IGenericRepository<T> repository)
        {
            this.repository = repository;
        }
        public bool Activate(int id)
        {
            if (id == 0 || GetByID(id) == null)
            {
                return false;
            }
            else
            {
                return repository.Activate(id);
            }
        }

        public bool Add(T item)
        {
            return repository.Add(item);
        }

        public bool Add(List<T> items)
        {
            return repository.Add(items);
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            return repository.Any(exp);
        }



        public List<T> GetActive()
        {
            return repository.GetActive();
        }

        public IQueryable<T> GetActive(params Expression<Func<T, object>>[] exp)
        {
            return repository.GetActive(exp);
        }

        public List<T> GetAll()
        {
            return repository.GetAll();
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] exp)
        {
            return repository.GetAll(exp);
        }

        public T GetByDefault(Expression<Func<T, bool>> exp)
        {
            return GetByDefault(exp);
        }

        public T GetByID(int id)
        {
            return repository.GetByID(id);
        }

        public List<T> GetDefault(Expression<Func<T, bool>> exp)
        {
            return repository.GetDefault(exp);
        }
        public bool Remove(T item)
        {
            if (item == null)
            {
                return false;
            }
            else
            {
                return repository.Remove(item);
            }
        }
        public bool Remove(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            else
            {
                return repository.Remove(id);
            }
        }

        public bool RemoveAll(Expression<Func<T, bool>> exp)
        {
            return repository.RemoveAll(exp);
        }
        public bool Update(T item)
        {

            if (item == null)
            {
                return false;
            }
            else
            {
                return repository.Update(item);
            }
        }
  

    }
}