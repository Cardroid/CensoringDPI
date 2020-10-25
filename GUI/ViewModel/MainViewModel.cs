using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GBDPIGUI.Core.Model;
using GBDPIGUI.View;

using ModernWpf.Controls;

namespace GBDPIGUI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            NavigationViewItems = new List<NavigationViewItem>
            {
                new NavigationViewItem { Content = "일반", Tag = new GeneralView(), Icon = new SymbolIcon(Symbol.Play) },
                new NavigationViewItem { Content = "메뉴얼", Tag = new ManualView(), Icon = new SymbolIcon(Symbol.Message) },
                new NavigationViewItem { Content = "인수 설정", Tag = new ArgumentView(), Icon = new SymbolIcon(Symbol.Edit) },
                new NavigationViewItem { Content = "설정", Tag = new OptionView(), Icon = new SymbolIcon(Symbol.Setting) },
            };
        }

        public List<NavigationViewItem> NavigationViewItems { get; }
    }
}
