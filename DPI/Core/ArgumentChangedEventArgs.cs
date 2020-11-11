using System;

namespace GoodByeDPIDotNet.Core
{
    public class ArgumentChangedEventArgs : EventArgs
    {
        public ArgumentChangedEventArgs(bool isAdded, string argument, string value, bool isPreset)
        {
            this.IsAdded = isAdded;
            this.IsCleared = false;
            this.IsPreset = isPreset;
            this.Argument = argument;
            this.Value = value;
        }
        
        public ArgumentChangedEventArgs(bool isCleared, bool isPreset)
        {
            this.IsCleared = isCleared;
            this.IsAdded = false;
            this.IsPreset = isPreset;
            this.Argument = string.Empty;
            this.Value = string.Empty;
        }

        public bool IsAdded { get; }
        public bool IsCleared { get; }
        public bool IsPreset { get; }
        public string Argument { get; }
        public string Value { get; }
    }
}
