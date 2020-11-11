using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GBDPIGUI.Utility;

using GoodByeDPIDotNet;
using GoodByeDPIDotNet.Interface;

using Newtonsoft.Json.Linq;

namespace GBDPIGUI.Core.Model
{
    public class GoodByeDPIOptionsHelper : ISaveLoadable, INotifyPropertyChanged
    {
        public GoodByeDPIOptionsHelper(GoodByeDPIOptions options)
        {
            this.GoodByeDPIOptions = options;
        }

        private GoodByeDPIOptions _GoodByeDPIOptions;
        public GoodByeDPIOptions GoodByeDPIOptions
        {
            get => _GoodByeDPIOptions;
            set
            {
                _GoodByeDPIOptions = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GoodByeDPIOptions"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Load(string json)
        {
            try
            {
                var JObj = JObject.Parse(json);
                GoodByeDPIOptions.Path = JObj["Path"].Value<string>();
                GoodByeDPIOptions.IsAdmin = Checker.IsAdministrator();

                ArgumentParser(JObj["Argument"].Value<string>());

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GoodByeDPIOptions"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Save()
        {
            JObject jObject = new JObject
            {
                { "Path", GoodByeDPIOptions.Path },
                { "Argument", GoodByeDPIOptions.GetArgument() }
            };
            return jObject.ToString();
        }

        internal void ArgumentParser(string args)
        {
            string[] _args = args.Split(' ');

            for (int i = 0; i < _args.Length; i++)
            {
                if (i + 1 < _args.Length && _args[i + 1].StartsWith("\""))
                    GoodByeDPIOptions.AddArgument(_args[i], _args[++i]);
                else
                    GoodByeDPIOptions.AddArgument(_args[i]);
            }
        }
    }
}
