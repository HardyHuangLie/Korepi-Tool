using Korepi_Tool.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Korepi_Tool.Page
{
    /// <summary>
    /// Setting.xaml 的互動邏輯
    /// </summary>
    public partial class Setting : System.Windows.Controls.Page
    {
        public Setting()
        {
            InitializeComponent();
            //GetGameOpen();
            Page_Open();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Page_Close();
        }
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            SaveData.save.DelayTime = (int)DelayTime.Value;
            SaveData.save.MHYProtect = Close_Protect.IsChecked.Value;
            SaveData.WriteFile();
            if (GamePath.Text == string.Empty)
            {
                MessageBox.Show(Methods.Resours.ChoosePath);
                return;
            }
            Protect.ChangeProtect(GamePath.Text,Close_Protect.IsChecked.Value);
            if (CreatShortCit.IsChecked==true)
            {
                ShortCut.CreateDesktopShortcut();
            }
            else
            {
                ShortCut.DeleteDesktopShortcut();
            }
            Page_Close();
        }
        private void AutoSearch_Click(object sender, RoutedEventArgs e)
        {
            string path =Korepi_Tool.Methods.Path.GetGamePath();
            if (path != string.Empty)
            {
                GamePath.Text = Korepi_Tool.Methods.Path.GetGamePath();
            }
            else
            {
                GamePath.Text = Korepi_Tool.Methods.Path.OpenPath();
            }
            SaveData.save.Path = GamePath.Text;
        }
        private void ChoosePath_Click(object sender, RoutedEventArgs e)
        {
            GamePath.Text = Korepi_Tool.Methods.Path.OpenPath();
            SaveData.save.Path = GamePath.Text;
        }
        #region 動關閉事件
        void Page_Close()
        {
            Storyboard myAnimation = (Storyboard)this.Resources["Close"];
            myAnimation.Completed += (s, E) => {
                MainWindow.NavigateTo(new Home());
            };
            myAnimation.Begin();
        }
        void Page_Open()
        {
            GamePath.Text=SaveData.save.Path;
            DelayTime.Value = SaveData.save.DelayTime;
            Close_Protect.IsChecked = SaveData.save.MHYProtect;
            CreatShortCit.IsChecked = ShortCut.IsDesktopShortcut();
        }
        #endregion
    }
}
