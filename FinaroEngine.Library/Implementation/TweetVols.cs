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

        public List<TweetVol> GetTweetVol(string name)
        {
            List<SqlParameter> prms = new List<SqlParameter> { new SqlParameter("@NAME", name) };
            var dt = DBUtility.GetDataTable(opts.ConnectionString, "spSelectTrendDataTweetVol", prms);
            List<TweetVol> tweetVols = new List<TweetVol>();
            foreach (DataRow dr in dt.Rows)
            {
                tweetVols.Add(new TweetVol
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    URL = dr["URL"].ToString(),
                    TweetVolume = Convert.ToInt32(dr["TweetVolume"]),
                    LoadDate = Convert.ToDateTime(dr["LoadDate"]),
                    UserEntry = Convert.ToBoolean(dr["UserEntry"]),
                    AvgSentiment = Convert.ToDouble(dr["AvgSentiment"])
                });
            }
            return tweetVols;
        }

        public string GetTweetVolJSON(string name)
        {
            return JsonConvert.SerializeObject(GetTweetVol(name));
        }
    }
}
