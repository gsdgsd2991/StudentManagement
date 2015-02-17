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
        private readonly ICrudService<Core.Model.Lecture> lectures = IOC.Ioc.Container.Resolve<ICrudService<Core.Model.Lecture>>();
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
            return service.where(a => a.Lectures.Contains(lecture),false).ToList();
        }
    }
}
