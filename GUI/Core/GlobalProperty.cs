using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using GBDPIGUI.Core.Model;
using GBDPIGUI.Utility;

using GoodByeDPIDotNet;
using GoodByeDPIDotNet.Interface;

using HandyControl.Data;
using HandyControl.Themes;

using Newtonsoft.Json;

namespace GBDPIGUI.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GlobalProperty : INotifyPropertyChanged
    {
        private GlobalProperty()
        {
            GoodByeDPI = GoodByeDPI.GetInstence();
            GoodByeDPIOptionsHelper = new GoodByeDPIOptionsHelper(new GoodByeDPIOptions());
            GoodByeDPIOptionsHelper.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
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
        internal GoodByeDPIOptionsHelper GoodByeDPIOptionsHelper { get; }
        public GoodByeDPIOptions GoodByeDPIOptions => GoodByeDPIOptionsHelper.GoodByeDPIOptions;

        private SkinType _Skin = SkinType.Default;
        /// <summary>
        /// 스킨 타입
        /// </summary>
        [JsonProperty]
        public SkinType Skin
        {
            get => _Skin;
            set
            {
                _Skin = value;
                ((App)Application.Current).UpdateSkin(_Skin);
                OnPropertyChanged("Skin");
            }
        }

        private string _CustomArgument;
        /// <summary>
        /// 커스텀 인수 목록
        /// </summary>
        [JsonProperty]
        public string CustomArgument
        {
            get => _CustomArgument;
            set
            {
                _CustomArgument = value;
                OnPropertyChanged("CustomArgument");
            }
        }
    }
}
