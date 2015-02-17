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

namespace Teacher.Lecture
{
    /// <summary>
    /// Interaction logic for LectureSelect.xaml
    /// </summary>
    public partial class LectureSelect 
    {
        private bool _isNet;
        //private readonly Core.Service.ITeacherService teacherService = IOC.Ioc.Container.Resolve<Core.Service.ITeacherService>();
        public LectureSelect(bool isNet = true)//是否是联网签到
        {
            InitializeComponent();
            foreach(var lecture in LoginStatus.Teacher.Lectures)
            {
                var tile = new Tile();
                tile.Title = lecture.Name;
                tile.Content = lecture.ID;
                tile.Click += Tile_Clicked;
                LectureSelectPanel.Children.Add(tile);
            }
           
            
        }

        private void Tile_Clicked(object sender,RoutedEventArgs e)
        {
            var tile = sender as Tile;
           LoginStatus.Lecture = LoginStatus.Teacher.Lectures.Where(a => a.Name == tile.Title && a.ID == (int)tile.Content).FirstOrDefault();
            if(_isNet)
            {
                this.Close();
            }
            else
            {
                var manageStudent = new ManageStudent(LoginStatus.Lecture);
                manageStudent.ShowDialog();
            }
        }
        
    }
}
