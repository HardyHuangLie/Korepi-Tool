using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace Korepi_Tool.Methods
{
    internal class Update
    {
        public static async void GetKorepiUpdate()
        {
            if (File.Exists("mhyprot.dll"))
            {
                File.Delete("mhyprot.dll");
            }
            string url = "https://github.com/HardyHuangLie/KebabiTool/releases/download/KorepiUpdate/KorepiUpdate.json";
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    Methods.KorpiJson data = JsonConvert.DeserializeObject<Methods.KorpiJson>(jsonResponse);
                    CompareKorepi(data.dll_md5,data.injector_md5,data.dll_url,data.injector_url);
                }
                catch (Exception e)
                {
                    return;
                }
            }
        }
        public static async void GetUpdate()
        {
            string url = "https://github.com/HardyHuangLie/KebabiTool/releases/download/Update/Update.json";
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    Methods.Json data = JsonConvert.DeserializeObject<Methods.Json>(jsonResponse);
                    CompareVersion(data.version, data.url);
                }
                catch (Exception e)
                {
                    return;
                }
            }
        }
        static void CompareKorepi(string dll_md5,string injector_md5, string dll_url, string injector_url)
        {
            List<string> strings = new List<string>();
            List<string> filename = new List<string>();
            string dllname = "HoYoKProtect.dll";
            string injectorname = "injector.exe";
            if (!File.Exists(dllname))
            {
                strings.Add(dll_url);
                filename.Add(dllname);
            }
            else
            {
                if (GetMD5HashFromFile(dllname) != dll_md5)
                {
                    strings.Add(dll_url);
                    filename.Add(dllname);
                }
            }
            if (!File.Exists(injectorname))
            {
                strings.Add(injector_url);
                filename.Add(injectorname);
            }
            else
            {
                if (GetMD5HashFromFile(injectorname) != injector_md5)
                {
                    strings.Add(injector_url);
                    filename.Add(injectorname);
                }
            }
            if (strings.Count>0)
            {
                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    MainWindow.NavigateTo(new Korepi_Tool.Page.Update(strings.ToArray(), filename.ToArray()));
                }));
            }
        }
        static void CompareVersion(Version version,string url)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Version now_version = assembly.GetName().Version;
            if (now_version< version)
            {
                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    MainWindow.NavigateTo(new Korepi_Tool.Page.Update(url, version));
                }));
            }
        }
        static string GetMD5HashFromFile(string fileName)
        {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
        }
    }
}
