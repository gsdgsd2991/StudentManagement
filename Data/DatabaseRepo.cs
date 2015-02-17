using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Core.Repo;
using Core.Model;


namespace Data
{
    public class DatabaseRepo<T>:IRepo<T> where T:Entity
    {
        private readonly DbContext dbcontext;

        public DatabaseRepo()
        {
            if(dbcontext == null)
            {
                dbcontext = (new DbFactory()).GetContext();
            }
        }

        public T Insert(T o)
        {
            dbcontext.Set<T>().Add(o);
            dbcontext.SaveChanges();
            return o;
            //throw new NotImplementedException();
        }

        public T Get(int id)
        {
            return dbcontext.Set<T>().First(a => a.ID == id);
            //throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            return dbcontext.Set<T>();
            //throw new NotImplementedException();
        }

        public IQueryable<T> where(System.Linq.Expressions.Expression<Func<T, bool>> prediction, bool showDeleted)
        {
            return dbcontext.Set<T>().Where(prediction).Where(a => a.isDeleted == false);
            //throw new NotImplementedException();
        }

        public void Save()
        {
            dbcontext.SaveChanges();
        }

        public void Delete()
        {
            ;
        }

        public void Restore()
        {
            throw new NotImplementedException();
        }


        public void Delete(T o)
        {
            throw new NotImplementedException();
        }

        public void Restore(T o)
        {
            throw new NotImplementedException();
        }
    }
}
