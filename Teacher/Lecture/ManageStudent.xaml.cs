﻿using System;
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
            lectureSelected.Students = lectureSelected.Students;//_controller.GetStudents(lectureSelected);
            foreach(var i in lectureSelected.Students)
            {
                AddTile(i.Sno, i.Name);
            }
            
            
        }

        

        private void DeleteFromLecture_Click(object sender, RoutedEventArgs e)
        {
           Tile tile = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Tile;
           _controller.DeleteStudent(tile.Title.Trim());      
        }
       
        private void GenerateNewMark_Click(object sender, RoutedEventArgs e)
        {
            Tile tile = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Tile;
            var student = _controller.GetStudent(tile.Title.Trim());
            GenerateSecureNumber(student);
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
                AddTile(newStudent.Sno, newStudent.Name);
                GenerateSecureNumber(newStudent);
            }
            else
            {
                if(original.Lectures.Where(a=>a.ID == _lectureSelected.ID).FirstOrDefault() ==null)
                {
                    _controller.AddLecture(original.Sno, _lectureSelected.ID);
                    AddTile(original.Sno, original.Name);
                }
            }

            
        }

        public void GenerateSecureNumber(Core.Model.Student student)
        {

        }

        private void AddTile(string sno,string name)
        {
            var tile = new Tile();
            tile.Title = sno;
            tile.Content = name;
            LectureStudentPanel.Children.Add(tile);
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
            AddTile(stu.Sno, stu.Name);
        }
    }
}
