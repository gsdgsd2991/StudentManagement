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
      // static DbContext GetContext();
    }

    public class DbFactory:IDbFactory
    {
        private static readonly DbContext dbContext = new Db();

        public DbFactory()
        {
            /*
                dbContext.Database.CreateIfNotExists();
                dbContext.Database.Initialize(true);
                if (((Db)dbContext).Teachers.Count(a => a.Name == "Admin") <= 0)
                {
                    var teacher = new Core.Model.Teacher();
                    teacher.Name = "Admin";

                    teacher.Password = "12345";
                    ((Db)dbContext).Teachers.Add(teacher);
                    dbContext.SaveChanges();
                }
                var modelBuilder = new DbModelBuilder();
                modelBuilder.Entity<Core.Model.Lecture>()
                    .HasMany(e => e.Students)
                    .WithMany(e => e.Lectures)
                    .Map(m =>
                    {
                        m.ToTable("StudentLecture");
                        m.MapLeftKey("ID");
                        m.MapRightKey("ID");
                    });

            */
        }

        public static DbContext GetContext()
        {
            
            return dbContext;
        }
    }
}
