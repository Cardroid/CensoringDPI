using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Utility
{
    internal static class Checker
    {
        internal static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            if (identity != null)
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return false;
        }
    }
}
