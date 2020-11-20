using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

using GBDPIGUI.Core;
using GBDPIGUI.Utility;
using GBDPIGUI.View;

namespace GBDPIGUI
{
    public partial class MainWindow : HandyControl.Controls.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (Check.IsAdministrator()) this.Title = $"[Admin] {this.Title}";

            this.Loaded += (s, e) =>
            {
                Load();
                Application.Current.Exit += (s, e) => Save();

                //this.Visibility = Visibility.Collapsed;
                TrayIcon.GetTrayIcon().IsIconEnabled = true;
            };

#if DEBUG
            #region TestCode
            GoodByeDPIDotNet.GoodByeDPI.Path = @"DPIEXE\goodbyedpi.exe";
            GoodByeDPIDotNet.GoodByeDPI.IsAdmin = Check.IsAdministrator();

            GlobalProperty.GetInstence().Skin = HandyControl.Data.SkinType.Default;
            #endregion
#endif
        }

        private const string directory = @"Save";

        private void Save()
        {
            SaveLoadManager.Save(Path.Combine(directory, "Option.json"), GlobalProperty.GetInstence());
            SaveLoadManager.Save(Path.Combine(directory, "Argument.json"), GoodByeDPIOptionsHelper.GetInstence());
        }

        private void Load()
        {
            SaveLoadManager.Load(Path.Combine(directory, "Option.json"), GlobalProperty.GetInstence());
            SaveLoadManager.Load(Path.Combine(directory, "Argument.json"), GoodByeDPIOptionsHelper.GetInstence());
        }
    }
}
