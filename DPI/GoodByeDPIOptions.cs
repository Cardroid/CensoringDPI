using System;
using System.Collections.Generic;
using System.Text;

using GoodByeDPI.NET.Interface;

namespace GoodByeDPI.NET
{
    public class GoodByeDPIOptions : IGoodByeDPIOptions
    {
        public GoodByeDPIOptions(string path, bool isArgumentLock)
        {
            this.ArgumentList = new Dictionary<string, string>();
            this.IsArgumentLock = isArgumentLock;
            this.Path = path;
        }

        public GoodByeDPIOptions(string path, bool isArgumentLock, params string[] Arguments) : this(path, isArgumentLock)
        {
            for (int i = 0; i < Arguments.Length; i++)
                AddArgument(Arguments[i]);
        }

        public GoodByeDPIOptions(GoodByeDPIOptions option)
        {
            this.ArgumentList = option.ArgumentList;
            this.IsArgumentLock = option.IsArgumentLock;
            this.Path = option.Path;
        }

        private Dictionary<string, string> ArgumentList { get; }

        public bool IsArgumentLock { get; }
        public string Path { get; set; }

        public void AddArgument(string Argument)
        {
            if (IsArgumentLock) return;

            Argument = ArgumentParser(Argument);
            if (!ArgumentList.ContainsKey(Argument))
                ArgumentList.Add(Argument, string.Empty);
        }

        public void AddArgument(string Argument, string value)
        {
            if (IsArgumentLock) return;

            if (string.IsNullOrWhiteSpace(value))
                value = string.Empty;

            Argument = ArgumentParser(Argument);
            if (!ArgumentList.ContainsKey(Argument))
                ArgumentList.Add(Argument, value);
        }

        public bool RemoveArgument(string Argument)
        {
            if (IsArgumentLock) return false;

            Argument = ArgumentParser(Argument);
            if (ArgumentList.ContainsKey(Argument))
            {
                ArgumentList.Remove(Argument);
                return true;
            }
            else
                return false;
        }

        public bool ContainsArgument(string Argument)
        {
            Argument = ArgumentParser(Argument);
            return ArgumentList.ContainsKey(ArgumentParser(Argument));
        }

        public int Count => ArgumentList.Count;

        public void Clear()
        {
            if (IsArgumentLock) return;

            ArgumentList.Clear();
        }

        public string GetArgument()
        {
            string result = string.Empty;

            foreach (var item in ArgumentList)
            {
                if (!string.IsNullOrEmpty(result))
                    result += " ";
                result += $"{item.Key}";
                if (!string.IsNullOrWhiteSpace(item.Value))
                    result += $" \"{item.Value}\"";
            }

            return result;
        }

        private static string ArgumentParser(string Argument)
        {
            if (string.IsNullOrWhiteSpace(Argument))
                return string.Empty;

            Argument = Argument.ToLower();
            string oriArgument = Argument;

            // "-" 제거
            int i;
            for (i = 0; i < Argument.Length; i++)
            {
                if (Argument.StartsWith("-"))
                    Argument = Argument.Substring(1, Argument.Length - 1);
                else
                {
                    i--;
                    break;
                }
            }

            if (oriArgument.Length - Argument.Length - i == 0)
                return oriArgument;
            else
            {
                // "-" 추가
                if (Argument.Length > 1)
                    return $"--{Argument}";
                else
                    return $"-{Argument}";
            }
        }
    }
}
