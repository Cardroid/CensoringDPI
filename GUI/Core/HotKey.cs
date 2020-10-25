using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Newtonsoft.Json;

namespace GBDPIGUI.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public class HotKey : ISaveLoadable
    {
        public HotKey(Key key, ModifierKeys modifiers)
        {
            Key = key;
            Modifiers = modifiers;
        }

        [JsonProperty]
        public Key Key { get; private set; }

        [JsonProperty]
        public ModifierKeys Modifiers { get; private set; }

        public string Save() => JsonConvert.SerializeObject(this);

        public bool Load(string json)
        {
            try
            {
                var loadedObj = (HotKey)JsonConvert.DeserializeObject(json);

                this.Key = loadedObj.Key;
                this.Modifiers = loadedObj.Modifiers;
            }
            catch
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            if (Modifiers.HasFlag(ModifierKeys.Control))
                str.Append("Ctrl + ");
            if (Modifiers.HasFlag(ModifierKeys.Shift))
                str.Append("Shift + ");
            if (Modifiers.HasFlag(ModifierKeys.Alt))
                str.Append("Alt + ");
            if (Modifiers.HasFlag(ModifierKeys.Windows))
                str.Append("Win + ");

            str.Append(Key);

            return str.ToString();
        }
    }
}
