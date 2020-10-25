using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GBDPIGUI.Core;
using GBDPIGUI.Core.Model;

namespace GBDPIGUI.ViewModel
{
    public class GeneralViewModel : BaseViewModel
    {
        public GeneralViewModel()
        {
        }

        #region IsExecuteGoodByeDPI
        public bool IsExecuteGoodByeDPI
        {
            get => GlobalProperty.GetInstence().GoodByeDPI.IsRun;
            set
            {
                var gP = GlobalProperty.GetInstence();
                if (value)
                    Status = gP.GoodByeDPI.Start(gP.GoodByeDPIOptions).Result;
                else
                    Status = gP.GoodByeDPI.Stop().Result;
                OnPropertyChanged("IsExecuteGoodByeDPI");
            }
        }
        #endregion

        #region Status
        private string _Status = string.Empty;
        public string Status
        {
            get => _Status;
            set
            {
                _Status = value;
                OnPropertyChanged("Status");
            }
        }
        #endregion
    }
}
