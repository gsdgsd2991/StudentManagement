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
        private static Core.Model.Question _question;
        private static bool _voting;

        public static Core.Model.Question Question
        {
            get { return _question; }
            set { _question = value; }
        }

        public static bool Voting
        {
            get { return _voting; }
            set { _voting = value; }
        }

        

        public static bool TeacherHasLogin
        {
            get { return _teacherHasLogin; }
            set { _teacherHasLogin = value; }
        }

        public static Core.Model.Teacher Teacher
        {
            get { return _teacher; }
            set 
            {
                _teacher = value; 
                //教师登陆不成功
                if(value == null)
                {
                    TeacherHasLogin = false;
                    _lecture = null;
                    _lectureOnce = null;
                    _question = null;
                    _voting = false;
                }
            }
        }

        public static Core.Model.Lecture Lecture
        {
            get { return _lecture; }
            set 
            {
                _lecture = value;
               /* foreach(var student in _lecture.Students)
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
                }*/
            }
        }
    }
}
