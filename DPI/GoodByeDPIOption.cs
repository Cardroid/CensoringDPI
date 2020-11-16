using System;
using System.Collections.Generic;
using System.Text;

using GoodByeDPIDotNet.Core;
using GoodByeDPIDotNet.Interface;
using GoodByeDPIDotNet.Manual;

namespace GoodByeDPIDotNet
{
    public class GoodByeDPIOption : IGoodByeDPIOption
    {
        public GoodByeDPIOption() : this(false)
        {
        }

        public GoodByeDPIOption(bool isPreset)
        {
            this.ArgumentList = new Dictionary<string, string>();
            this.IsPreset = isPreset;
        }

        public GoodByeDPIOption(GoodByeDPIOption option)
        {
            this.ArgumentList = option.ArgumentList;
            this.IsPreset = option.IsPreset;
        }

        private Dictionary<string, string> ArgumentList { get; }

        public event EventHandler<ArgumentChangedEventArgs> ArgumentChangedEvent;
        private void OnArgumentChanged(bool isAdded, string argument, string value) => ArgumentChangedEvent?.Invoke(this, new ArgumentChangedEventArgs(isAdded, argument, value, IsPreset));
        private void OnArgumentChanged(bool isCleared) => ArgumentChangedEvent?.Invoke(this, new ArgumentChangedEventArgs(isCleared, IsPreset));

        public bool IsForceArgumentCheck { get; set; } = true;

        private bool _IsPreset;
        public bool IsPreset
        {
            get => _IsPreset;
            set
            {
                if (_IsPreset != value)
                {
                    _IsPreset = value;
                    OnArgumentChanged(false);
                }
            }
        }

        public void AddArgument(string argument) => AddArgument(argument, string.Empty);

        public void AddArgument(string argument, string value)
        {
            if (IsPreset) return;

            argument = ArgumentParser(argument);
            if (string.IsNullOrEmpty(argument))
                return;

            ArgumentList[argument] = value;
            OnArgumentChanged(true, argument, value);
        }

        public bool RemoveArgument(string argument)
        {
            if (IsPreset) return false;

            argument = ArgumentParser(argument);
            if (ArgumentList.ContainsKey(argument))
            {
                string value = ArgumentList[argument];
                ArgumentList.Remove(argument);
                OnArgumentChanged(false, argument, value);
                return true;
            }
            else
                return false;
        }

        public bool ContainsArgument(string arg) => ArgumentList.ContainsKey(ArgumentParser(arg));
        public string GetArgumentValue(string arg)
        {
            if (ArgumentList.TryGetValue(ArgumentParser(arg), out string value))
                return value;
            else
                return string.Empty;
        }

        public int Count => ArgumentList.Count;

        public void Clear()
        {
            ArgumentList.Clear();
            _IsPreset = false;
            OnArgumentChanged(true);
        }

        public string GetArgument()
        {
            StringBuilder result = new StringBuilder();

            foreach (var item in ArgumentList)
            {
                if (result.Length != 0)
                    result.Append(" ");

                result.Append($"{item.Key}");

                if (!string.IsNullOrEmpty(item.Value))
                    result.Append($" \"{item.Value}\"");
            }

            return result.ToString();
        }

        private string ArgumentParser(string argument)
        {
            argument = argument.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(argument))
                return string.Empty;

            string result;

            // "-", "--" 제거
            for (int i = 0; i < argument.Length; i++)
            {
                if (argument.StartsWith("-"))
                    argument = argument.Substring(1, argument.Length - 1);
                else
                    break;
            }

            if (argument.Length > 1)
                result = $"--{argument}";
            else
                result = $"-{argument}";

            // 인수 사전을 통해 검사
            if (IsForceArgumentCheck)
            {
                foreach (var arg in ArgumentManual.GetArgumentManual().Keys)
                {
                    if (arg == result)
                        return arg;
                }
            }
            else
                return result;

            return string.Empty;
        }
    }
}
