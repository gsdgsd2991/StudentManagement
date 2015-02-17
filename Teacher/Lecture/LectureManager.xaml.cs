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
using MahApps.Metro.Controls.Dialogs;

namespace Teacher.Lecture
{
    /// <summary>
    /// Interaction logic for LectureManager.xaml
    /// </summary>
    public partial class LectureManager 
    {
        private readonly Teacher.TeacherController teacher = new Teacher.TeacherController();

        public LectureManager()
        {
            InitializeComponent();
            AddTile();
        }

        public void AddTile()
        {
            if (LoginStatus.Teacher.Lectures != null)
            {
                foreach (var i in LoginStatus.Teacher.Lectures)
                {
                    if (i.isDeleted == false)
                    {
                        var newLecture = new Tile();
                        newLecture.Title = i.Name;
                        newLecture.Content = i.ID;
                        var rightClickMenu = FindResource("RightClickContextMenu");
                        newLecture.ContextMenu = rightClickMenu as ContextMenu;
                        LecturesPanel.Children.Add(newLecture);
                    }
                }
            }
        }
        //添加课程按钮
        private async void Add_Click(object sender, RoutedEventArgs e)
        {
           var input = await DialogManager.ShowInputAsync(this, "课程名称", "");
           if (input != null && input.Trim() != "")
           {
               
               teacher.AddLecture(input);
               LecturesPanel.Children.RemoveRange(0, LecturesPanel.Children.Count);
               //var x = Resources.Values;
               AddTile();
           }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            teacher.DeleteLecture((sender as Tile).Title);
            LecturesPanel.Children.Remove(sender as Tile);           
        }

        private async void ChangeNameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var tile = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Tile;
            var input = await DialogManager.ShowInputAsync(this, "课程名称", "");
            teacher.ChangeLectureName((int)tile.Content,Title, input);
            tile.Title = input;
        }

        private void ManageStudentMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var tile = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Tile;
            var studentManage = new ManageStudent(LoginStatus.Teacher.Lectures.Where(a=>a.ID == (int)tile.Content).FirstOrDefault());
            studentManage.ShowDialog();
        }

       
    }
}
