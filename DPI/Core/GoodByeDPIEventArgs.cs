using System;
using System.Collections.Generic;
using System.Text;

namespace GoodByeDPIDotNet.Core
{
    public class GoodByeDPIEventArgs : EventArgs
    {
        public GoodByeDPIEventArgs()
        {

        }

        public bool IsRun { get; }
        public int ExitCode { get; }
        public Exception Exception { get; }
    }
}
