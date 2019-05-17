using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public interface ITrend
    {
        List<Trend> GetTrends();
        string GetTrendsJSON();
    }
}
