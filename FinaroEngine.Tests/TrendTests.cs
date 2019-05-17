using FinaroEngine.Library;
using System;
using System.Data;
using System.Threading.Tasks;
using Xunit;

namespace FinaroEngine.Tests
{
    public class TrendTests
    {
        private Options opts = new Options
        {
            ConnectionString = @"Data Source=VM-DEV-SQL\sql2014;Initial Catalog=FinaroDB;persist security info=True; Integrated Security=SSPI;"
        };

        [Fact]
        public void SelectListOfTrendsFromDatabase()
        {
            Trends trends = new Trends(opts);
            var list = trends.GetTrends();
            Assert.NotNull(list);
        }

        [Fact]
        public void SelectJSONResultOfTrendsFromDatabase()
        {
            Trends trends = new Trends(opts);
            var json = trends.GetTrendsJSON();
            Assert.NotNull(json);
        }
    }
}
