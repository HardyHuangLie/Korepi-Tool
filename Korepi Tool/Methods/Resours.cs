using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Korepi_Tool.Methods
{
    internal class Resours
    {
        public static string IsClose = Application.Current.Resources["IsClose"].ToString();
        public static string IsOpen = Application.Current.Resources["IsOpen"].ToString();
        public static string IsRun = Application.Current.Resources["IsRun"].ToString();
        public static string Close = Application.Current.Resources["Close"].ToString();
        public static string IsNotOpen = Application.Current.Resources["IsNotOpen"].ToString();
        public static string StartGame = Application.Current.Resources["StartGame"].ToString();
        public static string ChooseFile = Application.Current.Resources["ChooseFile"].ToString();
        public static string ChoosePath = Application.Current.Resources["ChoosePath"].ToString();
        public static string FailCloseProtect = Application.Current.Resources["FailCloseProtect"].ToString();
        public static string FailCreatShortCut = Application.Current.Resources["FailCreatShortCut"].ToString();
        public static string PathError = Application.Current.Resources["PathError"].ToString();
        public static string OpenGanshinFail = Application.Current.Resources["OpenGanshinFail"].ToString();
        public static string WriteMemoryFail = Application.Current.Resources["WriteMemoryFail"].ToString();
        public static string Msg = Application.Current.Resources["Msg"].ToString();
        public static string Sign = Application.Current.Resources["Sign"].ToString();
        public static string DownloadFail = Application.Current.Resources["DownloadFail"].ToString();
    }
}
