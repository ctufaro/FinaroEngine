using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace FinaroEngine.Library
{
    public class TweetVols : OptInit<Options>, ITweetVol
    {
        private Options opts;

        public TweetVols(Options opts) : base(opts)
        {
            this.opts = opts;
        }

        public TweetVol GetTweetVol(string name)
        {
            List<SqlParameter> prms = new List<SqlParameter> { new SqlParameter("@NAME", name) };
            var dt = DBUtility.GetDataTable(opts.ConnectionString, "spSelectTrendData", prms);
            TweetVol tweetVols = new TweetVol();
            tweetVols.Name = name;
            tweetVols.LoadDate = new List<string>();
            tweetVols.TweetVolume = new List<int>();
            tweetVols.AvgSentiment = new List<double?>();
            foreach (DataRow dr in dt.Rows)
            {
                tweetVols.LoadDate.Add(Convert.ToDateTime(dr["LoadDate"]).ToString("M'/'dd HH:mm"));
                tweetVols.TweetVolume.Add(Convert.ToInt32(dr["TweetVolume"]));
                tweetVols.AvgSentiment.Add(Convert.ToDouble(dr["AvgSentiment"]));   
            }
            return tweetVols;
        }

        public string GetTweetVolJSON(string name)
        {
            return JsonConvert.SerializeObject(GetTweetVol(name));
        }
    }
}
