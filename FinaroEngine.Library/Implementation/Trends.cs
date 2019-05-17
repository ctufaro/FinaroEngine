using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FinaroEngine.Library
{
    public class Trends : OptInit<Options>, ITrend
    {
        private Options opts;

        public Trends(Options opts) : base(opts)
        {
            this.opts = opts;
        }

        public List<Trend> GetTrends()
        {
            var dt = DBUtility.GetDataTable(opts.ConnectionString, "spSelectTrends", null);
            List<Trend> trends = new List<Trend>();
            foreach (DataRow dr in dt.Rows)
            {
                decimal[] randomPrices = new decimal[5] { Utility.RandomNumber(1,100), Utility.RandomNumber(1, 100), Utility.RandomNumber(1, 100), Utility.RandomNumber(1, 100), Utility.RandomNumber(1, 100) };
                decimal lastPrice = randomPrices[4];
                bool hasGain = randomPrices[4] > randomPrices[3] ? true : false;
                string myColor = hasGain ? "#0079FF" : "#DE3442";
                string myCSS = hasGain ? "btn btn-primary btn-sm" : "btn btn-danger btn-sm";
                string[] myGradient = hasGain ? new string[] { "#0079FF", "#4da0ff" } : new string[] { "#DE3442", "#e66570" };

                trends.Add(new Trend
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    LoadDate = Convert.ToDateTime(dr["LoadDate"]),
                    Name = dr["Name"].ToString(),
                    TweetVolume = Convert.ToInt32(dr["TweetVolume"]),
                    URL = dr["URL"].ToString(),
                    Color = myColor,
                    CSS = myCSS,
                    Faved = true,
                    Notify = true,
                    Gradient = myGradient,
                    Price = lastPrice,
                    PriceText = lastPrice.ToString("C"),
                    Prices = randomPrices
                });
            }
            return trends;
        }

        public string GetTrendsJSON()
        {
            return JsonConvert.SerializeObject(GetTrends());
        }
    }
}
