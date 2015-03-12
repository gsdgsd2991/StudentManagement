using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Service;
using Core.Model;
using Microsoft.Practices.Unity;
using Core.DataModel;
using SocketConnection;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Shell;
using System.Data.OleDb;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using PostSharp.Constraints;


namespace Teacher.Teacher
{
 
    public class TeacherController
    {

        private readonly ITeacherService service = IOC.Ioc.Container.Resolve<ITeacherService>();
        private readonly ICrudService<Core.Model.Question> _Questions = IOC.Ioc.Container.Resolve<ICrudService<Core.Model.Question>>();
        private readonly ICrudService<Core.Model.Lecture> Lectures = IOC.Ioc.Container.Resolve<ICrudService<Core.Model.Lecture>>();
        private readonly NotifyIcon icon = new NotifyIcon
        {
            Text = "NotifyIconStd"
        };
        //投票对应的5个选项
        private readonly int[] _voteItems = new int[5];
        public TeacherController()
        {
            //this.service = service;
            this.service.fileReceiveEvent += service_fileReceiveEvent;
            this.service.raiseHandEvent += service_raiseHandEvent;
            this.service.answerQuestionEvent += service_answerQuestionEvent;
            this.service.voteEvent += service_voteEvent;
            this.service.checkInEvent += service_checkInEvent;
        }
        //上课
        public void StartClass()
        {
            service.StartReceive();
        }
        //保存更改
        public void SaveChange()
        {
            service.Save();
        }
//下课
        public void CloseClass()
        {
            service.EndReceive();
        }
        //学生签到事件
        void service_checkInEvent(object sender, CheckIn e)
        {
            var student = LoginStatus.Lecture.Students.FirstOrDefault(a => a.SecureNum == e.senderSecureNo);
            if (LoginStatus.Lecture != null && student != null)
            {
                student.lectureAttendent.FirstOrDefault(a => a.LectureID == LoginStatus.Lecture.ID).LectureLast.AddSeconds(5);
            }
        }
        //学生投票事件
        void service_voteEvent(object sender, VoteType e)
        {
            if (LoginStatus.Voting && LoginStatus.Lecture.Students.Count(a => a.SecureNum == e.senderSecureNo) > 0)
            {
                try
                {
                    _voteItems[e.selection] += 1;
                }
                catch { }
            }
        }
        //学生回答问题事件
        void service_answerQuestionEvent(object sender, answerQuestionType e)
        {
            var studentAnswer = LoginStatus.Lecture.Students.FirstOrDefault(a => a.SecureNum == e.senderSecureNo);
            if (studentAnswer != null && LoginStatus.Question != null)//学生存在，问题存在
            {
                var giveScoreWindow = new Question.GiveScore(studentAnswer, LoginStatus.Question);
                giveScoreWindow.ShowDialog();
            }
        }
        //学生举手事件
        void service_raiseHandEvent(object sender, RaiseHandType e)
        {


        }
        //文件接受事件
        void service_fileReceiveEvent(object sender, FileType e)
        {

        }

        // public TeacherController()
        //{ }
        //修改密码
        public bool ChangePassword(int id, string originalPassword, string newPassword)
        {
            if (service.Get(id).Password == originalPassword)
            {
                service.ChangePassword(id, newPassword);

                return true;
            }
            else
            {
                return false;
            }

        }

        //登陆
   // [Aspects.TeacherLogined]
        public Core.Model.Teacher SignIn(string userName, string password)
        {
            var cryPass = Core.Model.Security.GetHash(password);
            var teacherLogined = service.Get(userName, cryPass);
            if (teacherLogined != null && teacherLogined.Name != null)
            {
                CheckDirectory.CheckDirectoryExistAndCreate(@"\teacher\" + teacherLogined.Name);
            }
            return teacherLogined;

        }
        //添加课程
        public void AddLecture(string LectureName)
        {
            Lectures.Create(new Core.Model.Lecture
            {
                Name = LectureName,
                teacher = LoginStatus.Teacher
            });
            Lectures.Save();

            LoginStatus.Teacher.Lectures = Lectures.where(a => a.teacher.ID == LoginStatus.Teacher.ID, false).ToList();
        }
        //删除课程
        public void DeleteLecture(string lectureName)
        {
            if (LoginStatus.Teacher != null)
            {
                lock (LoginStatus.Teacher)
                {
                    var lectureToDelete = LoginStatus.Teacher.Lectures.Where(a => a.Name == lectureName).FirstOrDefault();
                    if (lectureToDelete != null)
                    {
                        lectureToDelete.isDeleted = true;
                        Lectures.Save();
                    }
                }
            }
        }
//更改课程名称
        public void ChangeLectureName(int lectureId, string original, string newName)
        {
            if (LoginStatus.Teacher != null)
            {
                lock (LoginStatus.Teacher)
                {
                    // Lectures.where(a => a.ID == lectureId,false).FirstOrDefault().Name = newName;
                    LoginStatus.Teacher.Lectures.Where(a => a.ID == lectureId).FirstOrDefault().Name = newName;
                    Lectures.Save();
                }
            }
        }
        //获得课程
        public List<Core.Model.Lecture> GetLectures(Core.Model.Teacher t)
        {
            return Lectures.where(a => a.teacher.Name == t.Name, false).ToList();
        }

        //随机挑选学生
        public Core.Model.Student RandomStudent()
        {
            var students = LoginStatus.Lecture.Students;
            var random = new Random();
            var studentSelected = new List<Core.Model.Student>();


            var prob = random.Next(100);

            foreach (var student in students)
            {
                if (student.CorrectProb < prob)
                {
                    studentSelected.Add(student);

                }
            }
            foreach (var student in studentSelected)
            {
                if (student.CorrectProb > prob)
                {
                    studentSelected.Remove(student);
                }
            }

            return studentSelected.FirstOrDefault() == null ? studentSelected.FirstOrDefault() : students.FirstOrDefault(a => a.CorrectProb < 50);
        }
        //查找excel中的所有表名
        public async Task<List<string>> GetAllSheetName(string fileName)
        {
            var excelApp = new Excel.Application();
            

                excelApp.Visible = false;
                var sheetNames = new List<string>();
                if (System.IO.File.Exists(fileName))
                {
                    try
                    {
                        var excelBook = excelApp.Workbooks.Open(fileName);

                        for (int i = 1; i <= excelBook.Worksheets.Count; i++)
                        {
                            sheetNames.Add(excelBook.Worksheets.get_Item(i).Name);
                        }
                    }
                    finally
                    {
                        excelApp.Workbooks.Close();
                        excelApp.Quit();
                    }
                }
                excelApp.Workbooks.Close();
            excelApp.Quit();
            
               return sheetNames;
        }
        //导入问题Excel
        public bool InputQuestionExcel(string fileName,string sheetName)
        {

            var excelApp = new Excel.Application();
            excelApp.Visible = false;
            var excelBook = excelApp.Workbooks.Open(fileName);
            var excelSheet = excelBook.Sheets[sheetName] as Excel.Worksheet;
            try
            {
                for (int i = 0; i < excelSheet.Rows.Count; i++)
                {
                    var excelValues = excelSheet.get_Range("A" + i.ToString(), "C" + i.ToString()).Cells.Value;
                    //excel格式：A：题目类型B：题目描述C：题目答案D:课程id
                    _Questions.Create(new Core.Model.Question
                    {
                        TeacherId = LoginStatus.Teacher.TeacherNo,
                        AddTime = DateTime.Now,
                        AnswerDescribe = excelValues.GetValue(1, 2).ToString(),
                        CreatedBy = LoginStatus.Teacher,
                        CorrectPercentage = 0,
                        LectureBelonged = LoginStatus.Lecture,
                        LectureId = LoginStatus.Lecture.ID,
                        QuestionDescribe = excelValues.GetValue(1, 1).ToString()
                    });
                }
            }
            catch
            {
                return false;
            }
            return true;
         
        }
        [Aspects.LectureSelected]
        [Aspects.TeacherLogined]
        public async Task<List<ViewModel.QuestionViewModel>> GetQuestions(bool thisTeacher = true,bool thisClass = true)
        {
            var questions = new List<ViewModel.QuestionViewModel>();
            if(thisTeacher == true)
            {
                if(thisClass == true)
                {
                    var original = _Questions.where(a => a.TeacherId == LoginStatus.Teacher.TeacherNo && a.LectureId == LoginStatus.Lecture.ID,false);
                    Parallel.ForEach(original, a =>
                    {
                        questions.Add(new ViewModel.QuestionViewModel(a));
                    });
                }
                else
                {
                    var original = _Questions.where(a => a.TeacherId == LoginStatus.Teacher.TeacherNo, false);
                    Parallel.ForEach(original, a =>
                    {
                        questions.Add(new ViewModel.QuestionViewModel(a));
                    });
                }
            }
            else
            {
                 var original = _Questions.GetAll().Take(200);
                 Parallel.ForEach(original, a =>
                 {
                     questions.Add(new ViewModel.QuestionViewModel(a));
                 });
            }
            return questions;
        }
    }
}
