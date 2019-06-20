using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinaroEngine.Library
{
    public interface ITrend
    {
        List<Trend> GetTrends(int filter);
        string GetTrendsJSON(int filter);
        Task InsertUserTrend(int userId, string trendName);
    }
}
