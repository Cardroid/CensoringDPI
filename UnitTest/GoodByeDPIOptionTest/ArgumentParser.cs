using System;
using System.Diagnostics;

using GoodByeDPIDotNet;
using GoodByeDPIDotNet.Preset;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using UnitTest.Utility;

namespace UnitTest.GoodByeDPIOptionTest
{
    [TestClass]
    public class ArgumentParser
    {
        [TestMethod]
        public void ArgParser()
        {
            GoodByeDPIOptions option = new GoodByeDPIOptions("", Checker.IsAdministrator());

            option.AddArgument("-w");
            Assert.IsTrue(option.ContainsArgument("-w"), "Existence Test \"-w\"");
            Assert.IsTrue(option.ContainsArgument("w"), "Existence Test \"w\"");
            Assert.IsTrue(option.ContainsArgument("W"), "Existence Test \"W\"");

            option.AddArgument("--Arg");
            Assert.IsTrue(option.ContainsArgument("--Arg"), "Existence Test \"--Arg\"");
            Assert.IsTrue(option.ContainsArgument("arg"), "Existence Test \"arg\"");
            Assert.IsTrue(option.ContainsArgument("Arg"), "Existence Test \"Arg\"");
            Assert.IsTrue(option.ContainsArgument("ARG"), "Existence Test \"ARG\"");

            option.AddArgument("--Arg-Type");
            Assert.IsTrue(option.ContainsArgument("--Arg-Type"), "Existence Test \"--Arg-Type\"");
            Assert.IsTrue(option.ContainsArgument("arg-Type"), "Existence Test \"arg-Type\"");
            Assert.IsTrue(option.ContainsArgument("arg-type"), "Existence Test \"arg-type\"");
            Assert.IsTrue(option.ContainsArgument("Arg-Type"), "Existence Test \"Arg-Type\"");
            Assert.IsTrue(option.ContainsArgument("ARG-TYPE"), "Existence Test \"ARG-TYPE\"");

            Assert.IsTrue(option.RemoveArgument("w"), "Remove Test \"w\"");
            Assert.IsTrue(option.RemoveArgument("arg"), "Remove Test \"arg\"");
            Assert.IsTrue(option.RemoveArgument("ARG-TYPE"), "Remove Test \"arg-type\"");
        }

        [TestMethod]
        public void ArgSpeedandClear()
        {
            GoodByeDPIOptions option = new GoodByeDPIOptions("", Checker.IsAdministrator());

            int max = 10000;
            for (int i = 0; i < max; i++)
                option.AddArgument(i.ToString());
            Assert.IsTrue(option.Count == max, $"Speed Test \"value: {max}\"");

            option.Clear();
            Assert.IsTrue(option.Count == 0, "Clear Test");
        }

        [TestMethod]
        public void ArgPresetGetter()
        {
            Assert.IsTrue(((GoodByeDPIOptions)ArgumentPreset.GetPreset("", Checker.IsAdministrator(), PresetNum.Default)).ContainsArgument("1"), "1");
            Assert.IsTrue(((GoodByeDPIOptions)ArgumentPreset.GetPreset("", Checker.IsAdministrator(), PresetNum.Compatible_Better_Speed_HTTPS)).ContainsArgument("2"), "2");
            Assert.IsTrue(((GoodByeDPIOptions)ArgumentPreset.GetPreset("", Checker.IsAdministrator(), PresetNum.Better_Speed_HTTP_HTTPS)).ContainsArgument("3"), "3");
            Assert.IsTrue(((GoodByeDPIOptions)ArgumentPreset.GetPreset("", Checker.IsAdministrator(), PresetNum.Best_Speed)).ContainsArgument("4"), "4");
        }

        [TestMethod]
        public void ArgGetter()
        {
            GoodByeDPIOptions option = new GoodByeDPIOptions("", Checker.IsAdministrator());

            option.AddArgument("-w");
            option.AddArgument("hello");
            option.AddArgument("--not-value", "");
            option.AddArgument("value-test", "value");

            Assert.IsTrue(option.GetArgument() == "-w --hello --not-value --value-test \"value\"", option.GetArgument());
        }
    }
}
