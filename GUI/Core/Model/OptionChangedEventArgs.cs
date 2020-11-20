using System;
using System.Collections.Generic;
using System.Text;

using GoodByeDPIDotNet.Interface;

namespace GoodByeDPIDotNet.Core
{
    public class OptionChangedEventArgs : EventArgs
    {
        public OptionChangedEventArgs(string key, OptionChangedState state, IGoodByeDPIOption option)
        {
            this.Key = key;
            this.State = state;
            this.Option = option;
        }

        public string Key { get; }
        public OptionChangedState State { get; }
        public IGoodByeDPIOption Option { get; }
    }

    public enum OptionChangedState
    {
        Added, Deleted, Changed
    }
}
