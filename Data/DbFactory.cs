using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDbFactory
    {
        DbContext GetContext();
    }

    public class DbFactory:IDbFactory
    {
        private readonly DbContext dbContext;

        public DbFactory()
        {
           // Database.SetInitializer<Db>(new DropCreateDatabaseIfModelChanges<Db>());
           
            dbContext = new Db();
            
            dbContext.Database.CreateIfNotExists();
            dbContext.Database.Initialize(true);
            if(((Db)dbContext).Teachers.Count(a=>a.Name == "Admin")<0)
            {var teacher = new Core.Model.Teacher();
            teacher.Name = "Admin";
            
            teacher.Password = "12345";
            ((Db)dbContext).Teachers.Add(teacher);
            dbContext.SaveChanges();
            }
           /* if(!dbContext.Database.Exists())
            {
                ((IObjectContextAdapter)dbContext).ObjectContext.CreateDatabase();
            }*/
            
        }

        public DbContext GetContext()
        {
            return dbContext;
        }
    }
}
