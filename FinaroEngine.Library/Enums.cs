using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public enum Side
    {
        Plus = 1,
        Minus = 2
    }

    public enum Status
    {
        Open,
        Partial,
        Filled,
        Cancelled
    }
}
