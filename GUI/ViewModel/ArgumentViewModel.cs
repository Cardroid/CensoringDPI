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

using GoodByeDPIDotNet.Manual;
using GoodByeDPIDotNet.Preset;

namespace GBDPIGUI.ViewModel
{
    public class ArgumentViewModel : BaseViewModel
    {
        public ArgumentViewModel()
        {
            ArgumentViewItems = new StackPanel();
            foreach (var arg in ArgumentManual.GetArgumentManual())
                ArgumentViewItems.Children.Add(new ArgumentListViewItem(arg.Key, arg.Value.Item2, arg.Value.Item1));

            PresetList = new List<ComboBoxItem>
            {
                new ComboBoxItem { Content = "Default", Tag = PresetNum.Default },
                new ComboBoxItem { Content = "Compatible Better Speed HTTPS", Tag = PresetNum.Compatible_Better_Speed_HTTPS },
                new ComboBoxItem { Content = "Better Speed HTTP HTTPS", Tag = PresetNum.Better_Speed_HTTP_HTTPS },
                new ComboBoxItem { Content = "Best Speed", Tag = PresetNum.Best_Speed },
                new ComboBoxItem { Content = "Custom Preset", Tag = "" },
            };

            GlobalProperty.GetInstence().GoodByeDPI.PropertyChanged += GoodByeDPI_PropertyChanged;
            SelectPresetIndex = 2;
        }

        ~ArgumentViewModel()
        {
            GlobalProperty.GetInstence().GoodByeDPI.PropertyChanged -= GoodByeDPI_PropertyChanged;
        }

        private void GoodByeDPI_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsRun")
                IsPresetComboBoxEnabled = !GlobalProperty.GetInstence().GoodByeDPI.IsRun;
        }

        public StackPanel ArgumentViewItems { get; }

        private bool _IsPresetComboBoxEnabled = !GlobalProperty.GetInstence().GoodByeDPI.IsRun;
        public bool IsPresetComboBoxEnabled
        {
            get => _IsPresetComboBoxEnabled;
            set
            {
                _IsPresetComboBoxEnabled = value;
                OnPropertyChanged("IsPresetComboBoxEnabled");
            }
        }

        private string _PresetDescript;
        public string PresetDescript
        {
            get => _PresetDescript;
            set
            {
                _PresetDescript = value;
                OnPropertyChanged("PresetDescript");
            }
        }

        public IList<ComboBoxItem> PresetList { get; set; }

        private int _SelectPresetIndex;
        public int SelectPresetIndex
        {
            get => _SelectPresetIndex;
            set
            {
                _SelectPresetIndex = value;
                Debug.WriteLine($"SelectPresetIndex: {_SelectPresetIndex}");
                SelectPresetIndexRefresh();
                OnPropertyChanged("SelectPresetIndex");
            }
        }

        private void SelectPresetIndexRefresh()
        {
            var optionhelper = GlobalProperty.GetInstence().GoodByeDPIOptionsHelper;
            if (PresetList[SelectPresetIndex].Tag is PresetNum presetNum)
            {
                var presetManual = ArgumentManual.GetPresetManual();

                optionhelper.GoodByeDPIOptions.Clear();
                switch (presetNum)
                {
                    case PresetNum.Default:
                        optionhelper.ArgumentParser(presetManual["-1"].Item1);
                        PresetDescript = $"인수 집합: {presetManual["-1"].Item1}\n{presetManual["-1"].Item2}";
                        break;
                    case PresetNum.Compatible_Better_Speed_HTTPS:
                        optionhelper.ArgumentParser(presetManual["-2"].Item1);
                        PresetDescript = $"인수 집합: {presetManual["-2"].Item1}\n{presetManual["-2"].Item2}";
                        break;
                    case PresetNum.Better_Speed_HTTP_HTTPS:
                        optionhelper.ArgumentParser(presetManual["-3"].Item1);
                        PresetDescript = $"인수 집합: {presetManual["-3"].Item1}\n{presetManual["-3"].Item2}";
                        break;
                    case PresetNum.Best_Speed:
                        optionhelper.ArgumentParser(presetManual["-4"].Item1);
                        PresetDescript = $"인수 집합: {presetManual["-4"].Item1}\n{presetManual["-4"].Item2}";
                        break;
                }
                if (!optionhelper.GoodByeDPIOptions.IsPreset)
                    optionhelper.GoodByeDPIOptions.IsPreset = true;
            }
            else
            {
                PresetDescript = "커스텀 프리셋";

                if (optionhelper.GoodByeDPIOptions.IsPreset)
                    optionhelper.GoodByeDPIOptions.IsPreset = false;
            }
        }
    }
}
