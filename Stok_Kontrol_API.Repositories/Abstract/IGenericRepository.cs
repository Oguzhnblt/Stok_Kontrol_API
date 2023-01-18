using Stok_Kontrol_API.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StokKontrolProject.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        bool Add(T item);
        bool Add(List<T> items);
        bool Update(T item);
        bool Remove(T item);
        bool Remove(int id);
        bool RemoveAll(Expression<Func<T, bool>> exp);
        T GetByID(int id);
        T GetByDefault(Expression<Func<T, bool>> exp);
        List<T> GetActive();
        IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes);
        List<T> GetDefault(Expression<Func<T, bool>> exp);
        List<T> GetAll();
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        bool Activate(int id); // Aktifleştirmek için kullanılacak metot.
        bool Any(Expression<Func<T, bool>> exp); // LINQ ifadesi ile var mı diye sorgulama yapacağımız metot.
        int Save();// DB' de manipülasyon işleminden sonra 1 veya daha fazla satır etkilediğinde bize kaç satırın etkilendiğini döndürecek metodumuz.

        void DetachEntity(T item);
    }
}