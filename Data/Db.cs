using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Core;
using Core.Model;
using System.Data.Entity.Infrastructure;
using System.Configuration;

namespace Data
{
    public class Db:DbContext
    {
        public Db()
            : base(@"Data Source=(LocalDb)\v11.0;Initial Catalog=DefaultDatabase;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\DefaultDatabase.mdf")
        {
            
            this.Database.CreateIfNotExists();
            this.Database.Initialize(true);
            if (this.Teachers.Count(a => a.Name == "Admin") <= 0)
            {
                var teacher = new Core.Model.Teacher();
                teacher.Name = "Admin";

                teacher.Password = "12345";
                this.Teachers.Add(teacher);
                this.SaveChanges();
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
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Lecture> Lectures { get; set; }

        public DbSet<LectureAttendent> LectureAttendents { get; set; }

        public DbSet<LectureOnce> LectureOnceOfStudents { get; set; }

       // public DbSet<IMessage> Messages { get; set; }

        public DbSet<AnswerAccident> AnswerAccidents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
        }

    }
}
