using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GBDPIGUI.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.GUI
{
    [TestClass]
    public class TaskSchedulerManagerTest
    {
        [TestMethod]
        public void TaskSchedulerAdd()
        {
            Assert.IsTrue(TaskSchedulerManager.Add());
        }

        [TestMethod]
        public void TaskSchedulerRemove()
        {
            Assert.IsTrue(TaskSchedulerManager.Remove());
        }
    }
}
