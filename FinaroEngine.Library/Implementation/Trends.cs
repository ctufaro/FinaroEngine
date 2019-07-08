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
                decimal price = Convert.ToDecimal(dr["Price"]);
                bool hasGain = price > Convert.ToDecimal(dr["LastPrice"]) ? true : false;

                trends.Add(new Trend
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    LoadDate = Convert.ToDateTime(dr["LoadDate"]),
                    Name = dr["Name"].ToString(),
                    TweetVolume = Convert.ToInt32(dr["TweetVolume"]),
                    URL = dr["URL"].ToString(),
                    Faved = false,
                    Notify = false,
                    Price = price,
                    ChangeIn = Convert.ToDecimal(dr["ChangeIn"]),
                    PriceText = price.ToString("C"),
                    Prices = Utility.ConvertToDecArray(dr["PriceHistory"].ToString(), false),
                    Gains = hasGain
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
