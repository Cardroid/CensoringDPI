using GoodByeDPIDotNet.Interface;
using GoodByeDPIDotNet.Manual;

namespace GoodByeDPIDotNet.Preset
{
    public class ArgumentPreset
    {
        private ArgumentPreset()
        {
        }

        public static IGoodByeDPIOptions GetPreset(IGoodByeDPIOptions options, PresetNum presetNum) => GetPreset(options.Path, options.IsAdmin, presetNum);

        public static IGoodByeDPIOptions GetPreset(string goodByeDPIPath, bool isAdmin, PresetNum presetNum)
        {
            var result = new GoodByeDPIOptions(goodByeDPIPath, isAdmin);

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

                    return result;
                }
            }
            result.Clear();
            return result;
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
