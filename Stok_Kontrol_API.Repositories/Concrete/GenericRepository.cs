using Microsoft.EntityFrameworkCore;
using Stok_Kontrol_API.Entities.Entities;
using Stok_Kontrol_API.Repositories.Abstract;
using Stok_Kontrol_API.Repositories.Context;
using System.Linq.Expressions;
using System.Transactions;

namespace Stok_Kontrol_API.Repositories.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StockControlContext context;

        public GenericRepository(StockControlContext context)
        {
            this.context = context;
        }


        public bool Add(T item)
        {
            try
            {
                item.AddedDate = DateTime.Now;
                context.Set<T>().Add(item);
                return Save() > 0; // 1 satır etkileniyorsa true döndürsün
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Add(List<T> items)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (T item in items)
                    {
                        item.AddedDate = DateTime.Now;
                        context.Set<T>().Add(item);
                    }
                    ts.Complete();  // Tüm işlemler başarılı olduğunda, yani tüm ekleme işlemleri başarılı olursa Complete() olacak.
                    return Save() > 0;  // Bir veya daha fazla satır etkileniyorsa..
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Any(Expression<Func<T, bool>> exp) => context.Set<T>().Any(exp);

        public List<T> GetActive() => context.Set<T>().Where(x => x.isActive == true).ToList();
        public IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes)
        {
            var query = context.Set<T>().Where(x => x.isActive == true);
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        public List<T> GetAll() => context.Set<T>().ToList();
        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = context.Set<T>().AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        public T GetByDefault(Expression<Func<T, bool>> exp) => context.Set<T>().FirstOrDefault(exp);

        public T GetByID(int id) => context.Set<T>().Find(id);

        public List<T> GetDefault(Expression<Func<T, bool>> expression) => context.Set<T>().Where(expression).ToList();

        public bool Remove(T item)
        {
            item.isActive = false;
            return Remove(item);
        }

        public bool Remove(int id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    T item = GetByID(id);
                    item.isActive = false;
                    ts.Complete();

                    return Remove(item);
                }

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool RemoveAll(Expression<Func<T, bool>> expression)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var collection = GetDefault(expression);  // Verilen ifadeye göre ilgili nesneleri collection'a atıyoruz.
                    int counter = 0;
                    foreach (var item in collection)
                    {
                        item.isActive = false;
                        bool operationResult = RemoveAll(expression); // DB'den silmiyoruz. Durumunu silindi olarak ayarlıyoruz ve bunu da update metodu ile gerçekleştiriyoruz.

                        if (operationResult)
                        {
                            counter++;  // Sıradaki item'in silinme işlemi (yani silindi işaretleme) başarılı ise sayacı 1 arttırıyoruz.
                        }

                        if (collection.Count == counter) ts.Complete(); // Koleksiyondaki eleman sayısı ile silinme işlemi gerçekleşen eleman sayısı eşit ise bu işlemler başarılıdır.
                        else return false; // Aksi halde hiçbirinde değişiklik yapmadan false döndürür.
                    }

                    return true;

                }

            }
            catch (Exception)
            {

                return false;
            }
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public bool Update(T item)
        {
            try
            {
                item.ModifiedDate = DateTime.Now;
                context.Set<T>().Update(item);
                return Save() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Activate(int id)
        {
            T item = GetByID(id);
            item.isActive = true;
            return Update(item);
        }

        public void DetachEntity(T item)
        {
            context.Entry<T>(item).State = EntityState.Detached; // Bir entry'i takip etmeyi bırakmak için kullanılır.
        }

    }
}
