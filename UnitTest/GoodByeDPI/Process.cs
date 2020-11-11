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
            var GBDPI = GoodByeDPIDotNet.GoodByeDPI.GetInstence();

            var total = true;

            total &= !GBDPI.IsRun;

            var runResult = GBDPI.Start(ArgumentPreset.GetPreset(@"DPIEXE\goodbyedpi.exe", Checker.IsAdministrator(), PresetNum.Better_Speed_HTTP_HTTPS));

            Trace.WriteLine(runResult.Result);
            total &= runResult.Success;

            total &= GBDPI.IsRun;

            var endResult = GBDPI.Stop();

            Trace.WriteLine(endResult.Result);
            total &= endResult.Success;

            GBDPI.Kill();

            Assert.IsTrue(total);
        }
    }
}
