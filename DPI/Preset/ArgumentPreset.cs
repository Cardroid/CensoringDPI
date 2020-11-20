using GoodByeDPIDotNet.Interface;
using GoodByeDPIDotNet.Manual;

namespace GoodByeDPIDotNet.Preset
{
    public class ArgumentPreset
    {
        public static GoodByeDPIOption GetPreset(PresetNum presetNum)
        {
            var result = new GoodByeDPIOption();

            var presetManual = ArgumentManual.GetPresetManual();

            foreach (var preset in presetManual)
            {
                if(preset.Key == ((int)presetNum).ToString())
                {
                    string[] args = preset.Value.Item1.Split(' ');

                    for (int i = 0; i < args.Length; i++)
                    {
                        if(i + 1 < args.Length && args[i + 1].StartsWith("\""))
                            result.AddArgument(args[i], args[++i]);
                        else
                            result.AddArgument(args[i]);
                    }

                    result.IsPreset = true;
                    return result;
                }
            }
            return new GoodByeDPIOption();
        }
    }

    public enum PresetNum
    {
        Default = -1,
        Compatible_Better_Speed_HTTPS = -2,
        Better_Speed_HTTP_HTTPS = -3,
        Best_Speed = -4,
    }
}
