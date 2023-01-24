using System.Linq.Expressions;

namespace Stok_Kontrol_API.Service.Abstract
{
    public interface IGenericService<T>
    {
        bool Add(T item);
        bool Add(List<T> items);
        bool Update(T item);
        bool Remove(T item);
        bool Remove(int id);
        bool RemoveAll(Expression<Func<T, bool>> exp);
        T GetByID(int id);
        IQueryable<T> GetByID(int id, params Expression<Func<T, object>>[] includes);

        T GetByDefault(Expression<Func<T, bool>> exp);
        List<T> GetActive();
        IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes);
        List<T> GetDefault(Expression<Func<T, bool>> exp);
        List<T> GetAll();
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        bool Activate(int id); // Aktifleştirmek için kullanılacak metot.
        bool Any(Expression<Func<T, bool>> exp); // LINQ ifadesi ile var mı diye sorgulama yapacağımız metot.

    }
}
