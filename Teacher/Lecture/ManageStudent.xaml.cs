using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Microsoft.Practices.Unity;
using MahApps.Metro.Controls.Dialogs;

namespace Teacher.Lecture
{
    /// <summary>
    /// Interaction logic for ManageStudent.xaml
    /// </summary>
    public partial class ManageStudent 
    {
       // private readonly Core.Service.IStudentService _studentService = IOC.Ioc.Container.Resolve<Core.Service.IStudentService>();
        private readonly Core.Model.Lecture _lectureSelected;
        private readonly Teacher.StudentController _controller = new Teacher.StudentController();
        public ManageStudent(Core.Model.Lecture lectureSelected)
        {
            _lectureSelected = lectureSelected;
            InitializeComponent();
           // lectureSelected.Students = lectureSelected.Students;//_controller.GetStudents(lectureSelected);
           
                AddTile();          
        }
       

        private void DeleteFromLecture_Click(object sender, RoutedEventArgs e)
        {
           Tile tile = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Tile;
           _controller.DeleteStudent(_lectureSelected,tile.Title);
           AddTile();
        }
       
        private async void GenerateNewMark_Click(object sender, RoutedEventArgs e)
        {
            Tile tile = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Tile;
            var student = _controller.GetStudent(tile.Title.Trim());
            string ip = await DialogManager.ShowInputAsync(this,"学生IP","IP地址如：127.0.0.1");
            string port = await DialogManager.ShowInputAsync(this, "学生端口号", "");
            _controller.ChangeSecureNumber(student.ID,ip,int.Parse(port));//GenerateSecureNumber(student);
        }
        //弹出添加学生窗口
        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            this.AddStudentFlyout.IsOpen = !this.AddStudentFlyout.IsOpen;
        }

        
        //从文件批量导入学生
        private void AddStudentWithFile_Click(object sender, RoutedEventArgs e)
        {

        }
        //确定添加学生，先查数据库有没有学生，信息是否一致，没有就添加，学号和姓名不一致什么都不做
        private void AddStudentConfirm_Click(object sender, RoutedEventArgs e)
        {
            var original = _controller.GetStudent(StudentNumberTextBox.Text.Trim());//_studentService.where(a=>a.Sno == StudentNumberTextBox.Text.Trim(),true).FirstOrDefault();
            if(original == null)
            {
                var newStudent = new Core.Model.Student
                {
                     Sno = StudentNumberTextBox.Text.Trim(),
                      Name = StudentNameTextBox.Text.Trim(),
                   
                };
                newStudent.Lectures.Add(_lectureSelected);
                _controller.AddStudent(newStudent);
                AddTile();
                //_controller.ChangeSecureNumber(newStudent.ID);//GenerateSecureNumber(newStudent);
            }
            else
            {
                if(original.Lectures.Where(a=>a.ID == _lectureSelected.ID).FirstOrDefault() ==null)
                {
                    _controller.AddLecture(original.Sno, _lectureSelected.ID);
                    AddTile();
                }
            }

            
        }

        /*public void GenerateSecureNumber(Core.Model.Student student)
        {

        }*/

        private void AddTile()
        {
            LectureStudentPanel.Children.RemoveRange(0, LectureStudentPanel.Children.Count);
            if (_lectureSelected.Students == null)
                _lectureSelected.Students = new List<Core.Model.Student>();
            foreach (var i in _lectureSelected.Students)
            {
                var tile = new Tile();
                tile.Title = i.Sno;
                tile.Content = i.Name;
                tile.ContextMenu = FindResource("LectureStudentContextMenu") as ContextMenu;
                LectureStudentPanel.Children.Add(tile);
            }
        }

        private void ChangeStudentDetailOfLecture_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubmitInfo_Click(object sender, RoutedEventArgs e)
        {
            var stu = new Core.Model.Student
            {
                Name = StudentNameTextBox.Text,
                Sno = StudentNumberTextBox.Text,
               
            };
            
            _controller.AddStudent(stu);

            _controller.AddLecture(stu.Sno, _lectureSelected.ID);
            AddTile();
        }
    }
}
