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
                new NavigationViewItem { Content = "일반", Tag = GeneralView, Icon = new SymbolIcon(Symbol.Play) },
                new NavigationViewItem { Content = "메뉴얼", Tag = ManualView, Icon = new SymbolIcon(Symbol.Message) },
            };
        }

        public List<NavigationViewItem> NavigationViewItems { get; }
        private GeneralView GeneralView { get; } = new GeneralView();
        private ManualView ManualView { get; } = new ManualView();
        private OptionView OptionView { get; } = new OptionView();
    }
}
