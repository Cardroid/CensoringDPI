using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using GBDPIGUI.Utility;

using GoodByeDPIDotNet;

using Newtonsoft.Json;

namespace GBDPIGUI.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GlobalProperty : INotifyPropertyChanged
    {
        private GlobalProperty()
        {
            GoodByeDPI = GoodByeDPI.GetInstence();
            GoodByeDPIOptions = new GoodByeDPIOptions { IsAdmin = Checker.IsAdministrator() };
            Application.Current.Exit += (s, e) => GoodByeDPI.Stop();
        }

        private static GlobalProperty Instence;

        public static GlobalProperty GetInstence()
        {
            if (Instence == null)
                Instence = new GlobalProperty();
            return Instence;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public GoodByeDPI GoodByeDPI { get; }
        public GoodByeDPIOptions GoodByeDPIOptions { get; }

        private bool _IsPresetMode;
        /// <summary>
        /// GoodByeDPI 의 인수 프리셋 모드
        /// </summary>
        [JsonProperty]
        public bool IsPresetMode
        {
            get => _IsPresetMode;
            set
            {
                _IsPresetMode = value;
                OnPropertyChanged("IsPresetMode");
            }
        }
    }
}
