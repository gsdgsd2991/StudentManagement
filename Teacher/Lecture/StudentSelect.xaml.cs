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

namespace Teacher.Lecture
{
    /// <summary>
    /// Interaction logic for StudentSelect.xaml
    /// </summary>
    public partial class StudentSelect :MetroWindow
    {
        private Core.Model.Question _question;
        public StudentSelect(Core.Model.Lecture lecture,Core.Model.Question _question)
        {
            InitializeComponent();
            foreach(var student in lecture.Students)
            {
                var tile = new Tile();
                tile.Content = student.Sno;
                tile.Title = student.Name;
                tile.Click += Tile_Clicked;
                StudentSelectPanel.Children.Add(tile);
            }
            this._question = _question;
        }

        private void Tile_Clicked(object sender,RoutedEventArgs e)
        {
            var tile = sender as Tile;
            var student = LoginStatus.Lecture.Students.Where(a => a.Sno == tile.Content && a.Name == tile.Title).FirstOrDefault();
            var giveScore = new Question.GiveScore(student,_question);
            this.Close();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var student = new Core.Model.Student();
            var result = await MahApps.Metro.Controls.Dialogs.DialogManager.ShowMessageAsync(this, "恭喜学号为" + student.Sno + "的" + student.Name + "同学中奖", "确定结果？");
            
        }
        
    }
}
