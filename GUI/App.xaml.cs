using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using GBDPIGUI.Utility;

using HandyControl.Data;
using HandyControl.Themes;
using HandyControl.Tools;

namespace GBDPIGUI
{
    public partial class App : Application
    {
        public App()
        {
            if (!Checker.IsAdministrator() && !(Process.GetProcessesByName("CensoringDPI").Length > 2))
            {
                Process.Start(new ProcessStartInfo(Environment.GetCommandLineArgs()[0])
                {
                    Verb = "runas"
                });
                App.Current.Shutdown();
            }
        }

        internal void UpdateSkin(SkinType skin)
        {
            SharedResourceDictionary.SharedDictionaries.Clear();
            Resources.MergedDictionaries.Add(ResourceHelper.GetSkin(skin));
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml")
            });
            Current.MainWindow?.OnApplyTemplate();
        }
    }
}
