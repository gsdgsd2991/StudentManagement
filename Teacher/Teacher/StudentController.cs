using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Service;
using Microsoft.Practices.Unity;

namespace Teacher.Teacher
{
    public class StudentController
    {
        private readonly IStudentService service = IOC.Ioc.Container.Resolve<IStudentService>();
        private readonly ICrudService<Core.Model.Lecture> _lectures = IOC.Ioc.Container.Resolve<ICrudService<Core.Model.Lecture>>();
        public StudentController()
        {
            
        }

        public void ChangeSecureNumber(int id)
        {
            //change secure number in database
            service.changeSecureNumber(id, Guid.NewGuid().ToString());
            //send new secure number to student

        }

        public List<Core.Model.Student> GetStudents(Core.Model.Lecture lecture)
        {
            return service.where(a => a.Lectures.Exists(b=>b.ID == lecture.ID),false).ToList();
        }

        public Core.Model.Student GetStudent(string sno)
        {
            return service.where(a => a.Sno == sno,false).FirstOrDefault();
        }

        public void DeleteStudent(string sno)
        {
            service.where(a => a.Sno == sno, false).FirstOrDefault().isDeleted = true;
            service.Save();
        }

        public void AddStudent(Core.Model.Student stu)
        {
            var original = service.where(a=>a.Sno == stu.Sno,false).FirstOrDefault();
            if (original == null)
            {
                stu.Lectures = new List<Core.Model.Lecture>();
                service.Create(stu);
                service.Save();
            }
            
        }

        public void AddLecture(string sno,int lectureId)
        {
            var stu = service.where(a => a.Sno == sno, false).FirstOrDefault();
            stu.Lectures.Add(_lectures.where(a => a.ID == lectureId,false).FirstOrDefault());
            service.Save();
            //stu.Lectures.Add(lecture);
        }
    }
}
