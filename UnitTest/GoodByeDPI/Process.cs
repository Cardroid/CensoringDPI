using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using GoodByeDPIDotNet;
using GoodByeDPIDotNet.Preset;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using UnitTest.Utility;

namespace UnitTest.GoodByeDPI
{
    [TestClass]
    public class ProcessTest
    {
        [TestMethod]
        public void ProcessRun()
        {
            GoodByeDPIDotNet.GoodByeDPI.Path = @"DPIEXE\goodbyedpi.exe";
            GoodByeDPIDotNet.GoodByeDPI.IsAdmin = Checker.IsAdministrator();
            var total = true;

            total &= !GoodByeDPIDotNet.GoodByeDPI.IsRun;

            total &= GoodByeDPIDotNet.GoodByeDPI.Start(ArgumentPreset.GetPreset(PresetNum.Better_Speed_HTTP_HTTPS));

            Trace.WriteLine(GoodByeDPIDotNet.GoodByeDPI.LastError);

            total &= GoodByeDPIDotNet.GoodByeDPI.IsRun;

            total &= GoodByeDPIDotNet.GoodByeDPI.Stop();

            Trace.WriteLine(GoodByeDPIDotNet.GoodByeDPI.LastError);

            total &= GoodByeDPIDotNet.GoodByeDPI.IsRun;

            Assert.IsTrue(total);
        }
    }
}
