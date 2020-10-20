using System;
using System.Collections.Generic;
using System.Text;

namespace GoodByeDPI.NET
{
    public class GoodByeDPIOption
    {
        public GoodByeDPIOption()
        {
            this.ArgumentList = new Dictionary<string, string>();
        }

        public GoodByeDPIOption(params string[] Arguments) : this()
        {
            for (int i = 0; i < Arguments.Length; i++)
                AddArgument(Arguments[i]);
        }

        public GoodByeDPIOption(GoodByeDPIOption option)
        {
            this.ArgumentList = option.ArgumentList;
        }

        private Dictionary<string, string> ArgumentList { get; }

        public bool Lock { get; set; }

        public void AddArgument(string Argument)
        {
            if (Lock) return;

            Argument = ArgumentParser(Argument);
            if (!ArgumentList.ContainsKey(Argument))
                ArgumentList.Add(Argument, string.Empty);
        }
        
        public void AddArgument(string Argument, string value)
        {
            if (Lock) return;

            if (string.IsNullOrWhiteSpace(value))
                value = string.Empty;

            Argument = ArgumentParser(Argument);
            if (!ArgumentList.ContainsKey(Argument))
                ArgumentList.Add(Argument, value);
        }
        
        public bool RemoveArgument(string Argument)
        {
            if (Lock) return false;

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
            if (Lock) return;

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
