using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface ICrudService< T> where T:Entity
    {
        int Create(T o);
        void Delete(int id);
        void Save();
        IQueryable<T> where(Expression<Func<T, bool>> predict, bool showDeleted);
        IQueryable<T> GetAll();
        T Get(int id);
        void Restore(int id);

    }
}
