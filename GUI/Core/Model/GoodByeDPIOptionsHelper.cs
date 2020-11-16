using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using GoodByeDPIDotNet;
using GoodByeDPIDotNet.Core;
using GoodByeDPIDotNet.Interface;
using GoodByeDPIDotNet.Preset;

using Newtonsoft.Json.Linq;

namespace GBDPIGUI.Core.Model
{
    public class GoodByeDPIOptionsHelper : ISaveLoadable
    {
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
            foreach (var item in Options.GetOptions())
            {
                if (!OptionsHelper.IsDefaultPreset(item.Key))
                    optionArray.Add(new JArray(item.Key, item.Value.GetArgument()));
            }

            JObject jObject = new JObject(new JProperty("Options", optionArray));
            return jObject.ToString();
        }

        internal IGoodByeDPIOption ArgumentParser(string args)
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

        public class OptionsHelper
        {
            public static readonly string[] DefaultPresetNames = new string[] { "Default", "Compatible Better Speed HTTPS", "Better Speed HTTP HTTPS", "Better Speed" };
            public OptionsHelper()
            {
                this.Options = new Dictionary<string, IGoodByeDPIOption>
                {
                    { DefaultPresetNames[0], ArgumentPreset.GetPreset(PresetNum.Default) },
                    { DefaultPresetNames[1], ArgumentPreset.GetPreset(PresetNum.Compatible_Better_Speed_HTTPS) },
                    { DefaultPresetNames[2], ArgumentPreset.GetPreset(PresetNum.Better_Speed_HTTP_HTTPS) },
                    { DefaultPresetNames[3], ArgumentPreset.GetPreset(PresetNum.Best_Speed) }
                };
            }

            private Dictionary<string, IGoodByeDPIOption> Options { get; }


            public event EventHandler<OptionChangedEventArgs> OptionChanged;

            public static bool IsDefaultPreset(string name) => DefaultPresetNames.Contains(name);

            public bool ContainsKey(string name) => Options.ContainsKey(name);

            public void Add(string name, IGoodByeDPIOption option)
            {
                if (!IsDefaultPreset(name))
                {
                    if (Options.ContainsKey(name))
                    {
                        Options[name] = option;
                        OptionChanged?.Invoke(this, new OptionChangedEventArgs(name, OptionChangedState.Changed, option));
                    }
                    else
                    {
                        Options.Add(name, option);
                        OptionChanged?.Invoke(this, new OptionChangedEventArgs(name, OptionChangedState.Added, option));
                    }
                }
            }

            public bool Remove(string name)
            {
                if (ContainsKey(name) && !IsDefaultPreset(name))
                {
                    var option = Options[name];
                    Options.Remove(name);
                    OptionChanged?.Invoke(this, new OptionChangedEventArgs(name, OptionChangedState.Deleted, option));
                    return true;
                }
                return false;
            }

            public bool TryGetOption(string name, out IGoodByeDPIOption option) => Options.TryGetValue(name, out option);
            public IReadOnlyDictionary<string, IGoodByeDPIOption> GetOptions() => Options;

            public void Clear()
            {
                foreach (var key in Options.Keys)
                    Remove(key);
            }
        }
    }
}
