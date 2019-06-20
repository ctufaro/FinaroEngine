using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaroEngine.Library
{
    public class Trends : OptInit<Options>, ITrend
    {
        private Options opts;

        public Trends(Options opts) : base(opts)
        {
            this.opts = opts;
        }

        public List<Trend> GetTrends(int filter)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("@USERENTRY", filter));
            var dt = DBUtility.GetDataTable(opts.ConnectionString, "spSelectTrends", paras);
            List<Trend> trends = new List<Trend>();
            foreach (DataRow dr in dt.Rows)
            {
                decimal[] randomPrices = new decimal[5] { Utility.RandomNumber(1,100), Utility.RandomNumber(1, 100), Utility.RandomNumber(1, 100), Utility.RandomNumber(1, 100), Utility.RandomNumber(1, 100) };
                decimal lastPrice = randomPrices[4];
                bool hasGain = randomPrices[4] > randomPrices[3] ? true : false;
                string myColor = hasGain ? "success" : "danger";
                string myCSS = hasGain ? "btn btn-outline-success btn-sm" : "btn btn-outline-danger btn-sm";
                string[] myGradient = hasGain ? new string[] { "#29D3A5", "#fff" } : new string[] { "#FF4D29", "#fff" };

                trends.Add(new Trend
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    LoadDate = Convert.ToDateTime(dr["LoadDate"]),
                    Name = dr["Name"].ToString(),
                    TweetVolume = Convert.ToInt32(dr["TweetVolume"]),
                    URL = dr["URL"].ToString(),
                    Color = myColor,
                    CSS = myCSS,
                    Faved = false,
                    Notify = false,
                    Gradient = myGradient,
                    Price = lastPrice,
                    PriceText = lastPrice.ToString("C"),
                    Prices = randomPrices
                });
            }
            return trends;
        }

        public async Task InsertUserTrend(int userId, string trendName)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("@USERID", userId));
            paras.Add(new SqlParameter("@NAME", trendName));
            await DBUtility.ExecuteQueryAsync(opts.ConnectionString, "spInsertUserTrend", paras);      
        }

        public string GetTrendsJSON(int filter)
        {
            return JsonConvert.SerializeObject(GetTrends(filter));
        }
    }
}
