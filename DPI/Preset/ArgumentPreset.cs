using System;
using System.Collections.Generic;
using System.Text;

namespace GoodByeDPI.NET.Preset
{
    public class ArgumentPreset
    {
        private ArgumentPreset()
        {
        }

        public static GoodByeDPIOptions GetPreset(string goodByeDPIPath, PresetNum presetNum)
        {
            switch (presetNum)
            {
                case PresetNum.Compatible_Better_Speed_HTTPS:
                    return new GoodByeDPIOptions(goodByeDPIPath, true, "2");
                case PresetNum.Better_Speed_HTTP_HTTPS:
                    return new GoodByeDPIOptions(goodByeDPIPath, true, "3");
                case PresetNum.Best_Speed:
                    return new GoodByeDPIOptions(goodByeDPIPath, true, "4");
                default:
                    return new GoodByeDPIOptions(goodByeDPIPath, true, "1");
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
