using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Korepi_Tool.Methods
{
    internal class ObjectText
    {
        public static string GetGameDelatTime()
        {
            return SaveData.save.DelayTime + "ms";
        }
        public static void Get_Window()
        {
            for (; ; )
            {
                string processName = "GenshinImpact";

                // 查找符合條件的進程
                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Length > 0)
                {
                    return;
                }
                Thread.Sleep(100);
            }
        }
        public static void Get_Window_Close()
        {
            for (; ; )
            {
                string processName = "GenshinImpact";
                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Length <= 0)
                {
                    return;
                }
                Thread.Sleep(100);
            }
        }
    }
}
