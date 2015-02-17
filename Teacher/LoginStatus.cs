using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;

namespace Teacher
{
    //当前登录状态
    public static class LoginStatus
    {
        private static bool _teacherHasLogin;
        private static Core.Model.Teacher _teacher;
        private static Core.Model.Lecture _lecture;
        private static List<Core.Model.LectureOnce> _lectureOnce;

        public static bool TeacherHasLogin
        {
            get { return _teacherHasLogin; }
            set { _teacherHasLogin = value; }
        }

        public static Core.Model.Teacher Teacher
        {
            get { return _teacher; }
            set { _teacher = value; }
        }

        public static Core.Model.Lecture Lecture
        {
            get { return _lecture; }
            set 
            {
                _lecture = value;
                foreach(var student in _lecture.Students)
                {
                    var lectureOnceStudent = new LectureOnce
                    {
                        lecture = value,
                        LectureID = value.ID,
                        student = student,
                        teacher = _teacher,
                        TeacherId = _teacher.ID,
                        StudentId = student.ID,
                        studentOnlineLast = TimeSpan.Parse("0")
                    };
                    _lectureOnce.Add(lectureOnceStudent);
                }
            }
        }
    }
}
