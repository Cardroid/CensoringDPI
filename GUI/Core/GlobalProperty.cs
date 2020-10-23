using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBDPIGUI.Core
{
    public class GlobalProperty : INotifyPropertyChanged
    {
        private GlobalProperty()
        {
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
    }
}
