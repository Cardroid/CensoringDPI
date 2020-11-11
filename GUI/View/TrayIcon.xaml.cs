﻿using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HandyControl.Controls;

namespace GBDPIGUI.View
{
    public partial class TrayIcon : UserControl
    {
        public TrayIcon()
        {
            InitializeComponent();
            NotifyIconContextContent.Token = nameof(TrayIcon);

            NotifyIconContextContent.MouseDoubleClick += NotifyIconContextContent_MouseDoubleClick;
        }

        private void NotifyIconContextContent_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = ((MainWindow)App.Current.MainWindow);
            mainWindow.Visibility = Visibility.Visible;
            mainWindow.WindowState = WindowState.Normal;
            mainWindow.Activate();
        }

        public static TrayIcon GetTrayIcon() => ((MainWindow)App.Current.MainWindow)._Tray;
        
        public bool IsIconEnabled
        {
            get => NotifyIconContextContent.Visibility == Visibility.Visible;
            set
            {
                if (value)
                    NotifyIconContextContent.Visibility = Visibility.Visible;
                else
                    NotifyIconContextContent.Visibility = Visibility.Collapsed;
            }
        }
    }
}
