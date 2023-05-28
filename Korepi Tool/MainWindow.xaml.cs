using Korepi_Tool.Methods;
using Korepi_Tool.Page;
using Korepi_Tool.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using swf=System.Windows.Forms;
using WpfAnimatedGif;

namespace Korepi_Tool
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Methods.Start.isAdmin();
            Frame = frame;
            GetUpdate();
            SaveData.Read_ini();
            SaveData.ReadFile();
            GetWindow();
        }
        public static Frame Frame;
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Window_Close();
        }

        private void Mine_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            if (Is_Run)
            {
                Storyboard myAnimation = (Storyboard)this.Resources["Msg"];
                myAnimation.Begin();
            }
            else
            {
                frame.Navigate(new Setting());
            }
        }
        public static void NavigateTo(System.Windows.Controls.Page pg)
        {
            Frame.Navigate(pg);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void frame_Navigated(object sender, NavigationEventArgs e)
        {
            frame_event();
        }

        private void frame_Loaded(object sender, RoutedEventArgs e)
        {
            frame_event();
        }
        #region 方法
        void Window_Close()
        {
            Storyboard myAnimation = (Storyboard)this.Resources["Close"];
            myAnimation.Completed += (s, E) => {
                // 關閉視窗
                this.Close();
            };
            myAnimation.Begin();
        }
        void frame_event()
        {
            DelayTime.Text = ObjectText.GetGameDelatTime();
            if (SaveData.save.MHYProtect)
            {
                GameProtect.Foreground = Brushes.Red;
                GameProtect.Text =Methods.Resours.IsClose;
            }
            else
            {
                GameProtect.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF23CE00"));
                GameProtect.Text = Methods.Resours.IsOpen;
            }
        }
        public static bool Is_Run = false;
        async void GetWindow()
        {
            await Task.Run(() => {
                for (; ; )
                {
                    ObjectText.Get_Window();
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        Frame.Navigate(new Home());
                        GameStata.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF23CE00"));
                        GameStata.Text = Methods.Resours.IsRun;
                        btnText.Text = Methods.Resours.Close;
                        btnIconClose.Visibility = Visibility.Visible;
                        btnIcon.Visibility = Visibility.Hidden;
                        Is_Run = true;
                    }));
                    ObjectText.Get_Window_Close();
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        GameStata.Foreground=Brushes.Red;
                        GameStata.Text = Methods.Resours.IsNotOpen;
                        btnText.Text = Methods.Resours.StartGame;
                        btnIconClose.Visibility = Visibility.Hidden;
                        btnIcon.Visibility = Visibility.Visible;
                        Is_Run = false;
                    }));
                }
            });
        }
        async void GetUpdate()
        {
            await Task.Run(() => {
                Methods.Update.GetUpdate();
                Methods.Update.GetKorepiUpdate();
            });
        }
        void GenshinImpact_Close()
        {
            string processName = "GenshinImpact";
            // 查找符合條件的進程
            Process[] processes = Process.GetProcessesByName(processName);
            swf.Application.DoEvents();
            processes[0].Kill();
        }
        #endregion
        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               if (btnText.Text == Methods.Resours.StartGame)
                {
                    if (!SaveData.save.MHYProtect)
                    {
                        MessageBoxResult MBR= MessageBox.Show(Methods.Resours.Msg, Methods.Resours.Sign,MessageBoxButton.YesNo);
                        if(MBR== MessageBoxResult.Yes)
                        {
                            Methods.Start.Creat_Injector_ini();
                            Methods.Start.StartInjector();
                            return;
                        }
                        else
                        {
                            frame.Navigate(new Setting());
                            return;
                        }
                    }
                    Methods.Start.Creat_Injector_ini();
                    Methods.Start.StartInjector();
                }
                else
                {
                    GenshinImpact_Close();
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Link_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://github.com/HardyHuangLie/KebabiTool");
        }

        private void Link_Korpi_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://github.com/Korepi/Korepi");
        }
    }
}
