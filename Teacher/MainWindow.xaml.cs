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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Text.RegularExpressions;
using System.Windows.Interop;
using System.Windows.Threading;
using Microsoft.Practices.Unity;
using IOC;
using System.Data.Entity;
using MahApps.Metro.Controls.Dialogs;
using System.IO;


namespace Teacher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow:MetroWindow // Window
    {
        private readonly Teacher.TeacherController _teacher;

        public MainWindow()
        {
           
            InitializeComponent();
           
            
            Ioc.Container.RegisterType(typeof(Core.Repo.IRepo<>),typeof(Data.DatabaseRepo<>));
            Ioc.Container.RegisterType<Core.Service.ITeacherService, Service.TeacherService>(); 
            
            Ioc.Container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);
           
            _teacher = new Teacher.TeacherController();
            
            initHomeWorkTab();
      
        }
        //绑定数据显示控件
        public async void BindDataGrid(bool onlyCurrentTeacher = true/*是否只显示当前教师的题目*/,bool onlyCurrentLecture = true)
        {
            QuestionsDataGrid.ItemsSource = await _teacher.GetQuestions(onlyCurrentTeacher, onlyCurrentLecture);
          
        }
        //登陆按钮
        public async void InitLogin()
        {
            var setting = new MahApps.Metro.Controls.Dialogs.LoginDialogSettings();
            setting.NegativeButtonText = "Cancel";
            setting.NegativeButtonVisibility = Visibility.Visible;
            
            var loginData = await DialogManager.ShowLoginAsync(this, "Login", "login with username and password", setting);
            if (loginData != null)
            {
                LoginStatus.Teacher = _teacher.SignIn(loginData.Username,
                     loginData.Password);
                
                if (LoginStatus.Teacher != null)
                {
                    LoginStatus.TeacherHasLogin = true;
                    LoginStatus.Teacher.Lectures = _teacher.GetLectures(LoginStatus.Teacher);
                }
            }
        }
        //文件管理代码
        public void initHomeWorkTab()
        {
          //  var icons = new Dictionary<string, System.Windows.Controls.Image>();
            int i = 0;
            homeworkTab.Drop += (sender, e) =>
            {
                var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var fileName in fileNames)
                {
                    var fileIcon = new System.Windows.Controls.Image();


                    var showFileName = new Label();
                    showFileName.Content = Regex.Split(fileName, @"\\").Last();
                    var stackPanel = new StackPanel();
                    //  fileNameLabel.Content = fileName;
                    var bitmap = System.Drawing.Icon.ExtractAssociatedIcon(fileName).ToBitmap();
                    var hBitmap = bitmap.GetHbitmap();
                    ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
                    fileIcon.Source = wpfBitmap;
                    fileIcon.Width = 50;
                    fileIcon.Height = 50;
                    fileIcon.MouseDown += (sender2, e2) =>
                    {
                        i += 1;

                        DispatcherTimer timer = new DispatcherTimer();

                        timer.Interval = new TimeSpan(0, 0, 0, 0, 300);

                        timer.Tick += (s, e1) => { timer.IsEnabled = false; i = 0; };

                        timer.IsEnabled = true;

                        if (i % 2 == 0)
                        {

                            timer.IsEnabled = false;

                            i = 0;
                            System.Diagnostics.Process.Start(fileName/*@"D:\Program Files\CCleaner\CCleaner64.exe"*/);
                           

                        }

                    };
                    //icons.Add(fileName, fileIcon);
                    stackPanel.Children.Add(fileIcon);
                    stackPanel.Children.Add(showFileName);
                    fileViewerStackPanel.Children.Add(stackPanel);
                }

            };
            homeworkTab.DragOver += (sender, e) =>
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effects = DragDropEffects.All;
                else e.Effects = DragDropEffects.None;
                e.Handled = false;
            };
        }
        //登陆
        private void LoginIn_Click(object sender, RoutedEventArgs e)
        {
            InitLogin();
        }
        //管理课程
        private void ManageLecture_Click(object sender, RoutedEventArgs e)
        {
            //如果没有登陆
            if(LoginStatus.TeacherHasLogin == false || LoginStatus.Teacher == null)
            {
                InitLogin();
            }
            else 
            {
              var manager = new Lecture.LectureManager();
              manager.ShowDialog();
            }
        }
        //开启tcp socket 服务
        private void StartCheckIn_Click(object sender, RoutedEventArgs e)
        {
            if (LoginStatus.TeacherHasLogin == false || LoginStatus.Teacher == null )
            {
                InitLogin();
            }
            else
            {
                var lectureSelect = new Lecture.LectureSelect(true);
                lectureSelect.ShowDialog();
                if (LoginStatus.Lecture != null)
                {
                    _teacher.StartClass();
                }
               // var server = Ioc.Container.Resolve<SocketConnection.asyncServer>("server");
                //server.Receive();
            }
            
        }
        //关闭tcp socket 服务
        private void EndCheckIn_Click(object sender, RoutedEventArgs e)
        {
            _teacher.CloseClass();
            //var server = Ioc.Container.Resolve<SocketConnection.asyncServer>("server");
            //server.sockets.Close();
        }
        //手工签到按钮
        private void CheckInWithoutNet_Click(object sender, RoutedEventArgs e)
        {
            if (LoginStatus.TeacherHasLogin == false || LoginStatus.Teacher == null)
            {
                InitLogin();
            }
            else
            {
                var lectureSelect = new Lecture.LectureSelect(false);
                lectureSelect.ShowDialog();
            }

        }
        //临时提问
        private void QuestionInTime_Click(object sender, RoutedEventArgs e)
        {
            if(LoginStatus.Lecture != null)
            {
                var studentSelect = new Lecture.StudentSelect(LoginStatus.Lecture, null);
                studentSelect.ShowDialog();
            }
        }
        //问题回答情况
        private void QuestionAnswerSituation_Click(object sender, RoutedEventArgs e)
        {

        }
        //导入题目
        private async void InputQuestion_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.CheckFileExists = true;
            fileDialog.CheckPathExists = true;
            //fileDialog.Filter = "*.xlsx";
            string fileName;
            fileDialog.ShowDialog();
            
            fileName = fileDialog.FileName;
            if (fileName != null && fileName != "")
            {
                var sheetNames = await _teacher.GetAllSheetName(fileName);
            }
            BindDataGrid(true, true);
        }
        //发送课件文档
        private void SendDocumentButton_Click(object sender, RoutedEventArgs e)
        {

        }
        //打开作业存放的文件夹
        private void WorkFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LoginStatus.Teacher != null && LoginStatus.TeacherHasLogin == true)
                {
                    System.Diagnostics.Process.Start("Explorer.exe", @"\teacher\" + LoginStatus.Teacher.Name);
                }
            }
            catch
            {  }
        }
        //开始收取作业
        private void StartGetFileButton_Click(object sender, RoutedEventArgs e)
        {

        }
        //擦除显示的文档
        private void ClearDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            fileViewerStackPanel.Children.Clear();
        }
    }
}
