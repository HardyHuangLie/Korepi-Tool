using IWshRuntimeLibrary;
using Korepi_Tool.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Korepi_Tool.Properties;
using Memory;
using System.Security.Principal;

namespace Korepi_Tool.Methods
{
    internal class Start
    {
        static string ini_path = "cfg.ini";
        public static void Creat_Injector_ini()
        {
            if (!CheckGamePath())
            {
                return;
            }
            if(!System.IO.File.Exists(ini_path))
            {
                FileStream fs = new FileStream(ini_path, FileMode.Create);
                fs.Close();
            }
            WriteText();
        }
        static void WriteText()
        {
            using (StreamWriter writer = new StreamWriter(ini_path))
            {
                // 將文字寫入檔案
                writer.WriteLine("[Inject]");
                writer.WriteLine("GenshinPath = "+SaveData.save.Path);
                writer.WriteLine();
                writer.WriteLine("[System]");
                writer.WriteLine("InitializationDelayMS = " + SaveData.save.DelayTime);
                writer.Close();
            }
        }
        static bool CheckGamePath()
        {
            if (SaveData.save.Path == string.Empty)
            {
                MessageBox.Show(Methods.Resours.ChoosePath);
                MainWindow.Frame.Navigate(new Setting());
                return false;
            }
            if (!System.IO.File.Exists(SaveData.save.Path))
            {
                MessageBox.Show(Methods.Resours.PathError);
                MainWindow.Frame.Navigate(new Setting());
                return false;
            }
            return true;
        }
        public static void StartInjector()
        {
            Process notePad = new Process();
            // FileName 是要執行的檔案
            notePad.StartInfo.FileName = "injector.exe";
            notePad.Start();
        }
        public static void isAdmin()
        {
            bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

            // 如果未以系統管理員身分執行，則重新以系統管理員身分啟動應用程式
            if (!isAdmin)
            {
                // 重新啟動應用程式，並指定以系統管理員身分執行
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = System.Reflection.Assembly.GetEntryAssembly().Location;
                startInfo.Verb = "runas"; // 指定以系統管理員身分執行
                try
                {
                    Process.Start(startInfo);
                }
                catch
                {
                    // 無法以系統管理員身分啟動
                    // 可以在這裡進行錯誤處理
                    return;
                }

                // 退出當前應用程式
                System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
