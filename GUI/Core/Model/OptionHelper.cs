using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GoodByeDPIDotNet;
using GoodByeDPIDotNet.Core;
using GoodByeDPIDotNet.Interface;
using GoodByeDPIDotNet.Preset;

namespace GBDPIGUI.Core.Model
{
    public class OptionsHelper : IDictionary<string, GoodByeDPIOption>
    {

        public static readonly string[] DefaultPresetNames = new string[] { "Default", "Compatible Better Speed HTTPS", "Better Speed HTTP HTTPS", "Better Speed" };
        public OptionsHelper()
        {
            this.Options = new Dictionary<string, GoodByeDPIOption>
                {
                    { DefaultPresetNames[0], ArgumentPreset.GetPreset(PresetNum.Default) },
                    { DefaultPresetNames[1], ArgumentPreset.GetPreset(PresetNum.Compatible_Better_Speed_HTTPS) },
                    { DefaultPresetNames[2], ArgumentPreset.GetPreset(PresetNum.Better_Speed_HTTP_HTTPS) },
                    { DefaultPresetNames[3], ArgumentPreset.GetPreset(PresetNum.Best_Speed) }
                };
        }

        private Dictionary<string, GoodByeDPIOption> Options { get; }
        public event EventHandler<OptionChangedEventArgs> OptionChanged;

        public static bool IsDefaultPreset(string key) => DefaultPresetNames.Contains(key);

        public GoodByeDPIOption this[string key]
        {
            get => Options[key];
            set => Add(key, value);
        }

        public ICollection<string> Keys => Options.Keys;
        public ICollection<GoodByeDPIOption> Values => Options.Values;

        public int Count => Options.Count;
        public bool IsReadOnly => false;

        public void Add(string key, GoodByeDPIOption value)
        {
            if (!IsDefaultPreset(key))
            {
                if (Options.ContainsKey(key))
                {
                    Options[key] = value;
                    OptionChanged?.Invoke(this, new OptionChangedEventArgs(key, OptionChangedState.Changed, value));
                }
                else
                {
                    Options.Add(key, value);
                    OptionChanged?.Invoke(this, new OptionChangedEventArgs(key, OptionChangedState.Added, value));
                }
            }
        }

        public void Add(KeyValuePair<string, GoodByeDPIOption> item)
        {
            if (!IsDefaultPreset(item.Key))
            {
                if (Options.Contains(item))
                {
                    Options[item.Key] = item.Value;
                    OptionChanged?.Invoke(this, new OptionChangedEventArgs(item.Key, OptionChangedState.Changed, item.Value));
                }
                else
                {
                    Options.Add(item.Key, item.Value);
                    OptionChanged?.Invoke(this, new OptionChangedEventArgs(item.Key, OptionChangedState.Added, item.Value));
                }
            }
        }

        public void Clear()
        {
            foreach (var key in Options.Keys)
                if (!IsDefaultPreset(key))
                    Remove(key);
        }

        public bool Contains(KeyValuePair<string, GoodByeDPIOption> item) => Options.Contains(item);

        public bool ContainsKey(string key) => Options.ContainsKey(key);

        public void CopyTo(KeyValuePair<string, GoodByeDPIOption>[] array, int arrayIndex) => throw new NotImplementedException();

        public IEnumerator<KeyValuePair<string, GoodByeDPIOption>> GetEnumerator() => Options.GetEnumerator();

        public bool Remove(string key)
        {
            if (Options.ContainsKey(key) && !IsDefaultPreset(key))
            {
                var option = Options[key];
                Options.Remove(key);
                OptionChanged?.Invoke(this, new OptionChangedEventArgs(key, OptionChangedState.Deleted, option));
                return true;
            }
            return false;
        }

        public bool Remove(KeyValuePair<string, GoodByeDPIOption> item)
        {
            if (Options.Contains(item) && !IsDefaultPreset(item.Key))
            {
                Options.Remove(item.Key);
                OptionChanged?.Invoke(this, new OptionChangedEventArgs(item.Key, OptionChangedState.Deleted, item.Value));
                return true;
            }
            return false;
        }

        public bool TryGetValue(string key, out GoodByeDPIOption value) => Options.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => Options.GetEnumerator();
    }
}
