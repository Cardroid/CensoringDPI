using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using GBDPIGUI.Core.Model;
using GBDPIGUI.View;

namespace GBDPIGUI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            SelectItem = new RelayCommand(_SelectItem);
        }

        public RelayCommand SelectItem { get; }

        private UserControl _SelectedControl = new GeneralView();
        public UserControl SelectedControl
        {
            get => _SelectedControl;
            set
            {
                _SelectedControl = value;
                OnPropertyChanged("SelectedControl");
            }
        }

        private void _SelectItem(object o)
        {
            if (o is string header)
            {
                switch (header)
                {
                    case "Home":
                        if (!(SelectedControl is GeneralView))
                            SelectedControl = new GeneralView();
                        break;
                    case "Document":
                        if (!(SelectedControl is ManualView))
                            SelectedControl = new ManualView();
                        break;
                    case "Argument":
                        if (!(SelectedControl is ArgumentView))
                            SelectedControl = new ArgumentView();
                        break;
                    case "Option":
                        if (!(SelectedControl is OptionView))
                            SelectedControl = new OptionView();
                        break;
                }
            }
        }
    }
}
