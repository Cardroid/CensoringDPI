using System;
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

using GBDPIGUI.ViewModel;

using ModernWpf.Controls;

namespace GBDPIGUI.View
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

            this.MainNavigationView.IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
            this.MainNavigationView.IsBackEnabled = false;

            var viewModel = (MainViewModel)this.DataContext;

            this.MainNavigationView.SelectionChanged += MainNavigationView_SelectionChanged;
            this.MainNavigationView.SelectedItem = viewModel.NavigationViewItems.FirstOrDefault();
        }

        private void MainNavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
                this.MainFrame.Content = new OptionView();
            else
                this.MainFrame.Content = args.SelectedItemContainer.Tag;
        }
    }
}
