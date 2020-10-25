using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GBDPIGUI.Core;
using GBDPIGUI.Core.Model;
using GBDPIGUI.View.Helper;

using GoodByeDPIDotNet.Manual;

using ModernWpf.Controls;

namespace GBDPIGUI.ViewModel
{
    public class ArgumentViewModel : BaseViewModel
    {
        public ArgumentViewModel()
        {
            IsPresetMode = true;
        }

        private bool _IsPresetMode;
        public bool IsPresetMode
        {
            get => _IsPresetMode;
            set
            {
                if (_IsPresetMode != value)
                {
                    _IsPresetMode = value;

                    if (ViewItems == null)
                        ViewItems = new ObservableCollection<ListViewItem>();

                    if (_IsPresetMode)
                    {
                        ViewItems.Clear();
                        foreach (var arg in ArgumentManual.GetPresetManual())
                            ViewItems.Add(new ArgumentListViewItem(arg.Key, arg.Value, false));
                    }
                    else
                    {
                        ViewItems.Clear();
                        foreach (var arg in ArgumentManual.GetArgumentManual())
                            ViewItems.Add(new ArgumentListViewItem(arg.Key, arg.Value, arg.Key.EndsWith("[value]") || arg.Key.EndsWith("[txtfile]")));
                    }
                    OnPropertyChanged("IsPresetMode");
                }
            }
        }

        private ObservableCollection<ListViewItem> _ViewItems;
        public ObservableCollection<ListViewItem> ViewItems
        {
            get => _ViewItems;
            private set
            {
                _ViewItems = value;
                OnPropertyChanged("ViewItems");
            }
        }
    }
}
