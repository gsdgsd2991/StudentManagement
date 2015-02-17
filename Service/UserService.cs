using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Service;
using Core.Repo;

namespace Service
{
    public class CrudService<T>:ICrudService<T> where T:Core.Model.Entity
    {
        protected IRepo<T> repo;

        public CrudService(IRepo<T> repo)
        {
            this.repo = repo;
        }

        public int Create(T o)
        {
            var inserted =  repo.Insert(o);
            repo.Save();
            return inserted.ID;
        }

        public void Delete(int id)
        {
            repo.Delete(repo.Get(id));
            repo.Save();
        }

        public void Save()
        {
            repo.Save();
        }

        public IQueryable<T> where(System.Linq.Expressions.Expression<Func<T, bool>> predict, bool showDeleted)
        {
            return repo.where(predict, showDeleted);
        }

        public IQueryable<T> GetAll()
        {
            return repo.GetAll();
        }

        public T Get(int id)
        {
            return repo.Get(id);
        }

        public void Restore(int id)
        {
            repo.Restore(repo.Get(id));
            repo.Save();

        }
    }
}
