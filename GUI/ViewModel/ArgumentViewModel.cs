using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using GBDPIGUI.Core;
using GBDPIGUI.Core.Model;
using GBDPIGUI.View;

using GoodByeDPIDotNet;
using GoodByeDPIDotNet.Manual;
using GoodByeDPIDotNet.Preset;

namespace GBDPIGUI.ViewModel
{
    public class ArgumentViewModel : BaseViewModel
    {
        public ArgumentViewModel()
        {
            Manual = new List<Tuple<string, bool, string>>();
            foreach (var item in ArgumentManual.GetArgumentManual())
                Manual.Add(new Tuple<string, bool, string>(item.Key, item.Value.Item1, item.Value.Item2));
        }

        //public OptionsHelper Options => GoodByeDPIOptionsHelper.GetInstence().Options;
        public List<Tuple<string, bool, string>> Manual { get; }
    }
}
