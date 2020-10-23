using System;
using System.Collections.Generic;
using System.Text;

namespace GoodByeDPI.NET.Interface
{
    public interface IGoodByeDPIOptions
    {
        string Path { get; }
        string GetArgument();
    }
}
