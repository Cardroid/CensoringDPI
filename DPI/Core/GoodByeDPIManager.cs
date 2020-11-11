using System.Diagnostics;

namespace GoodByeDPIDotNet.Core
{
    public static class Checker
    {
        internal static bool GoodByeDPIRunCheck() => Process.GetProcessesByName("goodbyedpi").Length > 0;
    }
}
