using System;
using System.Collections.Generic;
using System.Text;

namespace GoodByeDPI.NET.Wrapper
{
    public class ArgumentPreset
    {
        private ArgumentPreset()
        {
        }

        public static GoodByeDPIOption GetPreset(PresetNum presetNum)
        {
            switch (presetNum)
            {
                default:
                case PresetNum.Default:
                    return new GoodByeDPIOption("1") { Lock = true };
                case PresetNum.Compatible_Better_Speed_HTTPS:
                    return new GoodByeDPIOption("2") { Lock = true };
                case PresetNum.Better_Speed_HTTP_HTTPS:
                    return new GoodByeDPIOption("3") { Lock = true };
                case PresetNum.Best_Speed:
                    return new GoodByeDPIOption("4") { Lock = true };
            }
        }
    }

    public enum PresetNum
    {
        Default,
        Compatible_Better_Speed_HTTPS,
        Better_Speed_HTTP_HTTPS,
        Best_Speed,
    }
}
