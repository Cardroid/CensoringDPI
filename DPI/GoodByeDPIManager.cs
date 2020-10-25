using System.Diagnostics;

namespace GoodByeDPIDotNet
{
    public class GoodByeDPIManager
    {
        public static bool GoodByeDPIRunCheck() => Process.GetProcessesByName("goodbyedpi").Length > 0;
    }
}
