using System.Diagnostics;

namespace GoodByeDPIDotNet.Core
{
    internal static class Check
    {
        internal static bool GoodByeDPIRunCheck() => Process.GetProcessesByName("goodbyedpi").Length > 0;
    }
}
