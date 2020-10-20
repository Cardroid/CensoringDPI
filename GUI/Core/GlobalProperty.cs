using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Core
{
    public class GlobalProperty
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
    }
}
