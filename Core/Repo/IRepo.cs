using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repo
{
    public interface IRepo<T>
    {
        T Insert(T o);
        T Get(int id);
        IQueryable<T> GetAll();
        IQueryable<T> where(Expression<Func<T, bool>> prediction, bool showDeleted);
        void Save();
        void Delete(T o);
        void Restore(T o);
    }
}
