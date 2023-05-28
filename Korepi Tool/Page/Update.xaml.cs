using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
using System.Xml.Linq;

namespace Korepi_Tool.Page
{
    /// <summary>
    /// Update.xaml 的互動邏輯
    /// </summary>
    public partial class Update : System.Windows.Controls.Page
    {
        private WebClient webClient;
        public Update(string Url,Version version)
        {
            InitializeComponent();
            Page_Start(Url, version);
        }
        public Update(string[] Url, string[] filemame)
        {
            InitializeComponent();
            Storyboard myAnimation = (Storyboard)this.Resources["Open"];
            myAnimation.Completed += async (s, E) => {
                for (int i=0;i<Url.Length;i++)
                {
                    await DownLoad(Url[i], filemame[i], false);
                }
                Storyboard Animation = (Storyboard)this.Resources["Open_Copy1"];
                Animation.Completed += async (a,b) => {
                    MainWindow.Frame.Navigate(new Home());
                }; 
                Animation.Begin();
            };
            myAnimation.Begin();
        }
        void Page_Start(string Url, Version version)
        {
            string saveFilePath = "Kebabi Tool_" + version.ToString() + ".exe";
            Storyboard myAnimation = (Storyboard)this.Resources["Open"];
            myAnimation.Completed += (s, E) => {
                DownLoad(Url, saveFilePath,true);
            };
            myAnimation.Begin();
        }
        async Task  DownLoad(string fileUrl, string saveFilePath,bool a)
        {
            if(File.Exists(saveFilePath)) { File.Delete(saveFilePath); }
            webClient = new WebClient();
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            try
            {
                await webClient.DownloadFileTaskAsync(new Uri(fileUrl), saveFilePath);
                if (a)
                {
                    Open_Kebabi(saveFilePath);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("檔案下載失敗");
                MainWindow.NavigateTo(new Home());
            }
            finally
            {
                webClient.DownloadProgressChanged -= WebClient_DownloadProgressChanged;
                webClient.Dispose();
                webClient = null;
            }
        }
        void Open_Kebabi(string name)
        {
            string secondAppPath =Path.Combine(AppDomain.CurrentDomain.BaseDirectory,name);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = secondAppPath;
            string path= Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location);
            startInfo.Arguments = $"\"{path}\"";
            Process.Start(startInfo);
            Application.Current.MainWindow.Close();
        }
        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // 在 UI 線程更新進度條的值
            Dispatcher.Invoke(() =>
            {
                ProgressBar.Value = e.ProgressPercentage;
            });
        }
    }
}
