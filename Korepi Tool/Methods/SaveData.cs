using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Markup;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Windows.Shapes;
using Korepi_Tool.Properties;
using static System.Windows.Forms.LinkLabel;

namespace Korepi_Tool.Methods
{
    internal class SaveData
    {
        public static Save save = new Save();
        static string SaveFile = "save.ini";
        public static void Read_ini()
        {
            string ini_path = "cfg.ini";
            if (!File.Exists(SaveFile))
            {
                if (File.Exists(ini_path))
                {
                    try
                    {
                        string line = null;
                        using (StreamReader reader = new StreamReader(ini_path))
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line.StartsWith("GenshinPath"))
                                {
                                    string[] parts = line.Split('=');
                                    if (File.Exists(parts[1].Trim()))
                                    {
                                        SaveData.save.Path = parts[1].Trim();
                                    }
                                }
                                if (line.StartsWith("InitializationDelayMS"))
                                {
                                    string[] parts = line.Split('=');
                                    SaveData.save.DelayTime = int.Parse(parts[1].Trim());
                                }
                            }
                        }
                    }
                    catch
                    {
                        return;
                    }
                }
            }
            return;
        }
        public static void WriteFile()
        {
            try
            {
                if (!File.Exists(SaveFile))
                {
                    FileStream fs = new FileStream(SaveFile, FileMode.Create);
                    fs.Close();
                }
                File.WriteAllText(SaveFile, JsonConvert.SerializeObject(save));          // 寫入文字
            }
            catch(Exception)
            {
            }
        }
        public static void ReadFile()
        {
            try
            {
                if (!File.Exists(SaveFile))
                {
                    FileStream fs = new FileStream(SaveFile, FileMode.Create);
                    fs.Close();
                    WriteFile();
                }
                save = JsonConvert.DeserializeObject<Save>(File.ReadAllText(SaveFile));
            }
            catch (Exception) 
            {
            }
        }
    }
}
