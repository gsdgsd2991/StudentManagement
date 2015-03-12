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

namespace Teacher.Question
{
    /// <summary>
    /// Interaction logic for GiveScore.xaml
    /// </summary>
    public partial class GiveScore 
    {
        private Core.Model.Student student;
        private Core.Model.Question question;
        private Teacher.TeacherController controller = new Teacher.TeacherController();

        public GiveScore(Core.Model.Student student,Core.Model.Question question)
        {
            InitializeComponent();
            this.student = student;
            this.question = question;
            StudentName.Content = student.Name;
        }

        private void AButton_Click(object sender, RoutedEventArgs e)
        {
            student.ansNum += 4;
            student.ansCorrect += 4;
            controller.SaveChange();
            this.Close();
        }

        private void BButton_Click(object sender, RoutedEventArgs e)
        {
            student.ansNum += 3;
            student.ansCorrect += 3;
            controller.SaveChange();
            this.Close();
        }

        private void CButton_Click(object sender, RoutedEventArgs e)
        {
            student.ansNum += 2;
            student.ansCorrect += 2;
            controller.SaveChange();
            this.Close();
        }

        private void DButton_Click(object sender, RoutedEventArgs e)
        {
            student.ansNum += 1;
            student.ansCorrect += 1;
            controller.SaveChange();
            this.Close();
        }

        private void EButton_Click(object sender, RoutedEventArgs e)
        {
            student.ansNum += 0;
            student.ansCorrect += 0;
            controller.SaveChange();
            this.Close();
        }

        
        
    }
}
