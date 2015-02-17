using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Service;
using Core.Model;
using Microsoft.Practices.Unity;

namespace Teacher.Teacher
{
    public class TeacherController
    {

        private readonly ITeacherService service = IOC.Ioc.Container.Resolve<ITeacherService>();
        private readonly ICrudService<Core.Model.Lecture> Lectures = IOC.Ioc.Container.Resolve<ICrudService<Core.Model.Lecture>>();
        public TeacherController(ITeacherService service)
        {
            this.service = service;
        }

        public TeacherController()
        { }

        public bool ChangePassword(int id,string originalPassword,string newPassword)
        {
            if(service.Get(id).Password == originalPassword)
            {
                service.ChangePassword(id, newPassword);

                return true;
            }
            else
            {
                return false;
            }

        }


        public Core.Model.Teacher SignIn(string userName,string password)
        {
            var cryPass = Core.Model.Security.GetHash(password);
            var teacherLogined = service.Get(userName,cryPass);
            return teacherLogined;
            
        }
        //添加课程
        public void AddLecture(string LectureName)
        {
            if (LoginStatus.Teacher != null)
            {
                lock (LoginStatus.Teacher)
                {

                    Lectures.Create(new Core.Model.Lecture
                    {
                        Name = LectureName,
                        teacher = LoginStatus.Teacher
                    });
                    Lectures.Save();
                }
            }
        }

        public void DeleteLecture(string lectureName)
        {
            if(LoginStatus.Teacher != null)
            {
                lock(LoginStatus.Teacher)
                {
                    var lectureToDelete = LoginStatus.Teacher.Lectures.Where(a => a.Name == lectureName).FirstOrDefault();
                    if(lectureToDelete != null)
                    {
                        lectureToDelete.isDeleted = true;
                        Lectures.Save();
                    }
                }
            }
        }

        public void ChangeLectureName(int lectureId,string original,string newName)
        {
            if(LoginStatus.Teacher != null)
            {
                lock(LoginStatus.Teacher)
                {
                   // Lectures.where(a => a.ID == lectureId,false).FirstOrDefault().Name = newName;
                    LoginStatus.Teacher.Lectures.Where(a => a.ID == lectureId).FirstOrDefault().Name = newName;
                    Lectures.Save();
                }
            }
        }
        
        public List<Core.Model.Lecture> GetLectures(Core.Model.Teacher t)
        {
            return Lectures.where(a => a.teacher.Name == t.Name,false).ToList();
        }

        
    }
}
