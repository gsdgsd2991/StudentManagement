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
        private readonly DbContext _dbcontext;
       // private readonly DbFactory _dbFactory;

        public DatabaseRepo()
        {
            //var _dbFactory = new DbFactory();
            if(_dbcontext == null)
            {
                _dbcontext = DbFactory.GetContext();
                //_dbcontext = DbFactory.GetContext();
            }
        }

        public T Insert(T o)
        {
            _dbcontext.Set<T>().Add(o);
            _dbcontext.SaveChanges();
            return o;
           
        }

        public T Get(int id)
        {
            return _dbcontext.Set<T>().First(a => a.ID == id);
           
        }

        public IQueryable<T> GetAll()
        {
            return _dbcontext.Set<T>();
           
        }

        public IQueryable<T> where(System.Linq.Expressions.Expression<Func<T, bool>> prediction, bool showDeleted)
        {
            return _dbcontext.Set<T>().Where(prediction).Where(a => a.isDeleted == false);
            
        }

        public void Save()
        {
            _dbcontext.SaveChanges();
        }

        public void Delete(int id)
        {
             _dbcontext.Set<T>().FirstOrDefault(a => a.ID == id).isDeleted = true;
        }

        public void Restore(int id)
        {
            _dbcontext.Set<T>().FirstOrDefault(a => a.ID == id).isDeleted = false;
        }


        public void Delete(T o)
        {
            o.isDeleted = true;
        }

        public void Restore(T o)
        {
            o.isDeleted = false;
        }
    }
}
