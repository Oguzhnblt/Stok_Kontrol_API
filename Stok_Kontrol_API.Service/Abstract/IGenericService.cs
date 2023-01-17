using System.Linq.Expressions;

namespace Stok_Kontrol_API.Service.Abstract
{
    public interface IGenericService<T>
    {
        bool Add(T item);
        bool Add(List<T> items);
        bool Update(T item);
        bool Remove(int id);
        bool Remove(T item);

        bool RemoveAll(Expression<Func<T, bool>> exp);
        List<T> GetDefault(Expression<Func<T, bool>> exp);
        List<T> GetActive();
        List<T> GetAll();
        T GetByDefault(Expression<Func<T, bool>> exp);
        T GetByID(int id);
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] exp);
        IQueryable<T> GetActive(params Expression<Func<T, object>>[] exp);
        bool Activate(int id); // Aktifleştirmek için kullancağımız metot.
        bool Any(Expression<Func<T, bool>> exp); // LINQ ifadesi ile var mı yok mu diye sorgulama yapacağımız metot.
        
    }
}
