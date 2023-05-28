using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Korepi_Tool
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoadLanguage();
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                string deletefile = args[1];
                if(!deletefile.EndsWith(".exe"))
                {
                    deletefile = deletefile + ".exe";
                }
                if (File.Exists(deletefile))
                {
                    await Task.Delay(1000);
                    File.Delete(deletefile);
                }
            }
        }
        private void LoadLanguage()
        {
            CultureInfo currentCultureInfo = CultureInfo.CurrentCulture;
            ResourceDictionary langRd = null;
            try
            {
                langRd = Application.LoadComponent(
                new Uri(@"Languages\" + currentCultureInfo.Name+ ".xaml ", UriKind.Relative)) as ResourceDictionary;
            }
            catch
            {
            }

            if (langRd != null)
            {
                if (this.Resources.MergedDictionaries.Count > 0)
                {
                    this.Resources.MergedDictionaries.Clear();
                }
                this.Resources.MergedDictionaries.Add(langRd);
            }
        }
    }
}