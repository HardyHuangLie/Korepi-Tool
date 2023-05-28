using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Korepi_Tool.Methods
{
    internal class Path
    {
        #region 手動選擇
        static string ChangePath(string path)
        {
            if (path.Contains("/"))
            {
                path=path.Replace("/", "\\");
            }
            return path;
        }
        public static string OpenPath()
        {
            string filename = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = Methods.Resours.ChooseFile;
            dialog.Filter = "GenshinImpact.exe|GenshinImpact.exe";
            dialog.ShowDialog();
            if (dialog.FileName != string.Empty)
            {
                filename = dialog.FileName;
                return ChangePath(filename);
            }
            return ChangePath(filename);
        }
        #endregion
        #region 自動搜索
        public static string GetGamePath()
        {
            string startpath = "";
            string launcherpath = GetLauncherPath();
            string cfgPath = System.IO.Path.Combine(launcherpath, "config.ini");
            if (File.Exists(launcherpath) || File.Exists(cfgPath))
            {
                //获取游戏本体路径
                using (StreamReader reader = new StreamReader(cfgPath))
                {
                    string[] abc = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    foreach (var item in abc)
                    {
                        //从官方获取更多配置
                        if (item.IndexOf("game_install_path") != -1)
                        {
                            startpath += item.Substring(item.IndexOf("=") + 1);
                        }
                    }
                }
            }
            byte[] bytearr = Encoding.UTF8.GetBytes(startpath);
            string path = Encoding.UTF8.GetString(bytearr);
            if (System.IO.File.Exists(path+ "/GenshinImpact.exe"))
            {
                return ChangePath(path + "/GenshinImpact.exe");
            }
            else
            {
                return "";
            }
        }
        private static string GetLauncherPath()
        {
            RegistryKey key = Registry.LocalMachine;
            string launcherpath = "";
            try
            {
                launcherpath = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Genshin Impact").GetValue("InstallPath").ToString();
            }
            catch (Exception)
            {
                launcherpath = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\原神").GetValue("InstallPath").ToString();
            }
            byte[] bytepath = Encoding.UTF8.GetBytes(launcherpath);     //编码转换
            string path = Encoding.UTF8.GetString(bytepath);
            return path;
        }
        #endregion
    }
}
