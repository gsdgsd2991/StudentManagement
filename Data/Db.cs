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
