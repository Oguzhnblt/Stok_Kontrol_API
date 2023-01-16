using Stok_Kontrol_API.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Stok_Kontrol_API.Service.Abstract
{
    public interface IGenericService<T>
    {
        bool Add(T item);
        bool Add(List<T> item);
        bool Update(T item);
        bool Remove(int id);
        bool RemoveAll(Expression<Func<T, bool>> exp);
        List<T> GetActive();
        List<T> GetAll();
        List<T> GetDefault(Expression<Func<T, bool>> exp);
        T GetByDefault(Expression<Func<T, bool>> exp);
        T GetByID(int id);
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] ex);
        bool Activate(int id); // Aktifleştirmek için kullancağımız metot.
        bool Any(Expression<Func<T, bool>> exp); // LINQ ifadesi ile var mı yok mu diye sorgulama yapacağımız metot.
        IQueryable<T> GetActive(params Expression<Func<T, object>>[] ex);
    }
}
