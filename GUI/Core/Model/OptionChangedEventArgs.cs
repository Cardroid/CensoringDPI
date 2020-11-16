using System;
using System.Collections.Generic;
using System.Text;

using GoodByeDPIDotNet.Interface;

namespace GoodByeDPIDotNet.Core
{
    public class OptionChangedEventArgs : EventArgs
    {
        public OptionChangedEventArgs(string name, OptionChangedState state, IGoodByeDPIOption option)
        {
            this.Name = name;
            this.State = state;
            this.Option = option;
        }

        public string Name { get; }
        public OptionChangedState State { get; }
        public IGoodByeDPIOption Option { get; }
    }

    public enum OptionChangedState
    {
        Added, Deleted, Changed
    }
}
