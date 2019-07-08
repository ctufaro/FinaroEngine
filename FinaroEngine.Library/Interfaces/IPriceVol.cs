using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public interface IPriceVol
    {
        PriceVol GetPriceVol(string name);
        string GetPriceVolJSON(string name);
    }
}
