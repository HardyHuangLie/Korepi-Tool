using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Korepi_Tool.Methods
{
    internal class Protect
    {
        static string GenshinPath = null;
        static string[] orginal = { "mhyprot3.Sys", "mhypbase.dll", "HoYoKProtect.Sys" };
        static string[] changed = { "mhyprot3.bak", "mhypbase.bak", "HoYoKProtect.bak" };
        public static void ChangeProtect(string GamePath,bool Close)
        {
            SaveData.save.MHYProtect = Close;
            SaveData.save.Path = GamePath;
            GenshinPath=GamePath;
            try
            {
                if(Close)
                {
                    for (int i = 0; i < orginal.Length; i++)
                    {
                        ChangeName(orginal[i], changed[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < orginal.Length; i++)
                    {
                        ChangeName(changed[i], orginal[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Methods.Resours.FailCloseProtect + ex.Message);
            }
        }
        static void ChangeName(string original, string changed)
        {
            string path = System.IO.Path.GetDirectoryName(GenshinPath);
            if (File.Exists(System.IO.Path.Combine(path, changed)))
            {
                if (File.Exists(System.IO.Path.Combine(path, original)))
                {
                    File.Delete(System.IO.Path.Combine(path, original));
                }
                return;
            }
            if (File.Exists(System.IO.Path.Combine(path, original)))
            {
                File.Move(System.IO.Path.Combine(path, original), System.IO.Path.Combine(path, changed));
                File.Delete(System.IO.Path.Combine(path, original));
            }
        }
    }
}
