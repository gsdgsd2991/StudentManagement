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


namespace Teacher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow:MetroWindow // Window
    {
        private readonly Teacher.TeacherController teacher;

        public MainWindow()
        {
           
            InitializeComponent();
           
            
            Ioc.Container.RegisterType(typeof(Core.Repo.IRepo<>),typeof(Data.DatabaseRepo<>));
            Ioc.Container.RegisterType<Core.Service.ITeacherService, Service.TeacherService>(); 
            //Ioc.Container.RegisterType(Core.Service.ITeacherService, Service.TeacherService);
            Ioc.Container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);
           
            teacher = new Teacher.TeacherController();
            //teacher.SignIn("abc", "def");
            initHomeWorkTab();
           // InitLogin(); 
            //TextShow.Text = "scrollBar width=" + scrollBar.Width.ToString() + " panel width=" + StudentsNamePanel.Width.ToString();
        }


        public async void InitLogin()
        {
            var setting = new MahApps.Metro.Controls.Dialogs.LoginDialogSettings();
            setting.NegativeButtonText = "Cancel";
            setting.NegativeButtonVisibility = Visibility.Visible;
            
            var loginData = await DialogManager.ShowLoginAsync(this, "Login", "login with username and password", setting);
            if (loginData != null)
            {
                LoginStatus.Teacher = teacher.SignIn(loginData.Username,
                     loginData.Password);
                if (LoginStatus.Teacher != null)
                {
                    LoginStatus.TeacherHasLogin = true;
                    LoginStatus.Teacher.Lectures = teacher.GetLectures(LoginStatus.Teacher);
                }
            }
        }
        //文件管理代码
        public void initHomeWorkTab()
        {
            var icons = new Dictionary<string, System.Windows.Controls.Image>();
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
                    icons.Add(fileName, fileIcon);
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
            if (LoginStatus.TeacherHasLogin == false || LoginStatus.Teacher == null)
            {
                InitLogin();
            }
            else
            {
                var lectureSelect = new Lecture.LectureSelect(true);
                lectureSelect.ShowDialog();
                teacher.StartClass();
               // var server = Ioc.Container.Resolve<SocketConnection.asyncServer>("server");
                //server.Receive();
            }
            
        }
        //关闭tcp socket 服务
        private void EndCheckIn_Click(object sender, RoutedEventArgs e)
        {
            teacher.CloseClass();
            //var server = Ioc.Container.Resolve<SocketConnection.asyncServer>("server");
            //server.sockets.Close();
        }
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
    }
}
