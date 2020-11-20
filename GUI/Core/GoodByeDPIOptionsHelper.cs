using GBDPIGUI.Core.Model;

using GoodByeDPIDotNet;
using GoodByeDPIDotNet.Interface;

using Newtonsoft.Json.Linq;

namespace GBDPIGUI.Core
{
    public class GoodByeDPIOptionsHelper : ISaveLoadable
    {
        private GoodByeDPIOptionsHelper()
        {
        }

        private static GoodByeDPIOptionsHelper Instence;
        public static GoodByeDPIOptionsHelper GetInstence()
        {
            if (Instence == null)
                Instence = new GoodByeDPIOptionsHelper();
            return Instence;
        }

        public OptionsHelper Options { get; } = new OptionsHelper();

        public bool Load(string json)
        {
            try
            {
                var JObj = JObject.Parse(json);
                JArray optionArray = JObj["Options"].ToObject<JArray>();

                for (int i = 0; i < optionArray.Count; i++)
                    Options.Add(optionArray[i][0].ToString(), ArgumentParser(optionArray[i][1].ToString()));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Save()
        {
            JArray optionArray = new JArray();
            foreach (var item in Options)
            {
                if (!OptionsHelper.IsDefaultPreset(item.Key))
                    optionArray.Add(new JArray(item.Key, item.Value.GetArgument()));
            }

            JObject jObject = new JObject(new JProperty("Options", optionArray));
            return jObject.ToString();
        }

        public static GoodByeDPIOption ArgumentParser(string args)
        {
            GoodByeDPIOption options = new GoodByeDPIOption();
            string[] _args = args.Split(' ');

            for (int i = 0; i < _args.Length; i++)
            {
                if (i + 1 < _args.Length && _args[i + 1].StartsWith("\""))
                    options.AddArgument(_args[i], _args[++i]);
                else
                    options.AddArgument(_args[i]);
            }
            return options;
        }
    }
}
