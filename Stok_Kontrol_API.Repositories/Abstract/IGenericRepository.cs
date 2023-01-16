    using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Stok_Kontrol_API.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        bool Add(T item);
        bool Add(List<T> item);
        bool Update(T item);
        bool Remove(int id);
        bool RemoveAll(Expression<Func<T, bool>> exp);
        List<T> GetActive();
        List<T> GetDefault(Expression<Func<T, bool>> expression);
        List<T> GetAll();
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] ex);
        bool Activate(int id); // Aktifleştirmek için kullancağımız metot.
        bool Any(Expression<Func<T, bool>> expression); // LINQ ifadesi ile var mı yok mu diye sorgulama yapacağımız metot.
        int Save(); // DB'de manipülasyon işleminden sonra 1 veya daha fazla satır etkilendiğinde bize kaç satırın etkilendiğini döndürecek metot.
        IQueryable<T> GetActive(params Expression<Func<T, object>>[] ex);
        void DetachEntity(T item);
    }
}
