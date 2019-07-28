using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using VaderSharp;

namespace FinaroEngine.Library
{
    public class TrendLibrary
    {
        public static void LoadTrends(string sqlConnectionString, string twitterConsumerKey, string twitterConsumerSecret, string twitterAccessToken, string twitterAccessTokenSecret, Action<string> LogError)
        {
            var request = new RestRequest("1.1/trends/place.json", Method.GET);
            request.AddQueryParameter("id", "23424977");
            request.AddQueryParameter("result_type", "popular");

            var client = new RestClient("https://api.twitter.com")
            {
                Authenticator = OAuth1Authenticator.ForProtectedResource(
                    twitterConsumerKey,
                    twitterConsumerSecret,
                    twitterAccessToken,
                    twitterAccessTokenSecret)
            };

            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            dynamic stuff = JsonConvert.DeserializeObject(content);
            DateTime startTime = Convert.ToDateTime(DBUtility.ExecuteScalar(sqlConnectionString, "SELECT GETDATE()"));
            bool updatesMade = false;

            foreach (JObject item in stuff)
            {
                foreach (var trend in item["trends"])
                {
                    string trendName = "";
                    double? score = 0;

                    try
                    {
                        int volume = 0;
                        Int32.TryParse(trend["tweet_volume"].ToString(), out volume);
                        List<SqlParameter> sparams = new List<SqlParameter>();

                        trendName = trend["name"].ToString();
                        var tweets = GetTweetsAsync(trendName, 10, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret);

                        if (tweets == null || volume == 0)
                            continue;

                        score = GetVaderSentAvgAsync(tweets.Result).Result;

                        sparams.Add(new SqlParameter("@NAME", trendName));
                        sparams.Add(new SqlParameter("@URL", trend["url"].ToString()));
                        sparams.Add(new SqlParameter("@TWEETVOLUME", volume));
                        sparams.Add(new SqlParameter("@AVGSENTIMENT", score));
                        sparams.Add(new SqlParameter("@USERENTRY", false));
                        DBUtility.ExecuteQuery(sqlConnectionString, "spInsertTrend", sparams);
                        updatesMade = true;
                    }
                    catch (Exception e)
                    {
                        LogError($"Trend:{trendName} Score:{score} {e.ToString()}");
                    }
                }
            }

            // UPDATES WERE MADE LETS ZERO OUT ORPHANED TRENDS
            if (updatesMade)            {
                
                var dt = DBUtility.GetDataTable(sqlConnectionString, "spSelectOrphanedTrends", new List<SqlParameter> { new SqlParameter("@LOADTIME", startTime) });                
                foreach(DataRow dr in dt.Rows)
                {
                    List<SqlParameter> sqlp = new List<SqlParameter>();
                    sqlp.Add(new SqlParameter("@NAME", dr["Name"].ToString()));
                    sqlp.Add(new SqlParameter("@URL", dr["URL"].ToString()));
                    sqlp.Add(new SqlParameter("@TWEETVOLUME", Convert.ToInt32(0)));
                    sqlp.Add(new SqlParameter("@AVGSENTIMENT", Convert.ToInt32(0)));
                    sqlp.Add(new SqlParameter("@USERENTRY", true));
                    DBUtility.ExecuteQuery(sqlConnectionString, "spInsertTrend", sqlp);
                }
            }
        }

        public static void LoadUserTrends(string sqlConnectionString, string twitterConsumerKey, string twitterConsumerSecret, string twitterAccessToken, string twitterAccessTokenSecret, Action<string> LogError)
        {
            //sql statement here to get user trend names
            var dt = DBUtility.GetDataTable(sqlConnectionString, "spSelectUserTrends", null);
            foreach(DataRow dr in dt.Rows)
            {
                var trendName = dr["TRENDNAME"].ToString();
                var tweets = GetTweetsAsync(trendName, 10, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret);
                double? score = GetVaderSentAvgAsync(tweets.Result).Result;
                int fakeVolume = Utility.RandomNumber(10000, 500000);

                List<SqlParameter> sparams = new List<SqlParameter>();
                string enCodedName = (trendName.StartsWith('#') ? "%23"+trendName.Substring(1) : trendName);
                sparams.Add(new SqlParameter("@NAME", trendName));
                sparams.Add(new SqlParameter("@URL", $"http://twitter.com/search?q={HttpUtility.HtmlEncode(enCodedName)}"));
                sparams.Add(new SqlParameter("@TWEETVOLUME", fakeVolume));//random number
                sparams.Add(new SqlParameter("@AVGSENTIMENT", score));
                sparams.Add(new SqlParameter("@USERENTRY", true));
                FinaroEngine.Library.DBUtility.ExecuteQuery(sqlConnectionString, "spInsertTrend", sparams);
            }
        }

        public async static Task<string> LoadTweetVolumeAsync(string keyword, string twitterConsumerKey, string twitterConsumerSecret, string twitterAccessToken, string twitterAccessTokenSecret)
        {
            var request = new RestRequest("5/insights/keywords/search", Method.GET);
            request.AddQueryParameter("keywords", keyword);
            request.AddQueryParameter("start_time", "2019-06-20");
            request.AddQueryParameter("granularity", "DAY");
    
            var client = new RestClient("https://ads-api-sandbox.twitter.com")
            {
                Authenticator = OAuth1Authenticator.ForProtectedResource(
                    twitterConsumerKey,
                    twitterConsumerSecret,
                    twitterAccessToken,
                    twitterAccessTokenSecret)
            };

            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

            var content = response.Content; // raw content as string
            return content;
        }

        public static void ClearTrends(string sqlConnectionString)
        {
            List<SqlParameter> dummy = new List<SqlParameter> { new SqlParameter("@DUMMY", -1) };
            FinaroEngine.Library.DBUtility.ExecuteQuery(sqlConnectionString, "spClearTrends", dummy);
        }

        public static void ClearOrders(string sqlConnectionString)
        {
            List<SqlParameter> dummy = new List<SqlParameter> { new SqlParameter("@DUMMY", -1) };
            FinaroEngine.Library.DBUtility.ExecuteQuery(sqlConnectionString, "spClearOrders", dummy);
        }

        public static async Task<List<string>> GetTweetsAsync(string tweet, int count, string twitterConsumerKey, string twitterConsumerSecret, string twitterAccessToken, string twitterAccessTokenSecret)
        {
            List<string> tweets = new List<string>();
            var request = new RestRequest("1.1/search/tweets.json", Method.GET);
            request.AddQueryParameter("q", tweet);
            request.AddQueryParameter("result_type", "mixed");//recent popular mixed
            request.AddQueryParameter("count", count.ToString());
            request.AddQueryParameter("lang", "en");

            var client = new RestClient("https://api.twitter.com")
            {
                Authenticator = OAuth1Authenticator.ForProtectedResource(
                    twitterConsumerKey,
                    twitterConsumerSecret,
                    twitterAccessToken,
                    twitterAccessTokenSecret)
            };

            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

            var content = response.Content; // raw content as string

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;
            
            dynamic stuff = JsonConvert.DeserializeObject(content);

            foreach (JObject item in stuff["statuses"])
            {
                tweets.Add(Sanitize(item["text"].ToString(), tweet));
            }

            return tweets;
        }        

        public static async Task<double?> GetVaderSentAvgAsync(List<string> tweets)
        {            
            double temp;
            double? retval = 0;
            int count = tweets.Count;
            SentimentIntensityAnalyzer analyzer = new SentimentIntensityAnalyzer();

            foreach (string text in tweets)
            {
                var results = analyzer.PolarityScores(text);

                if (results.Compound == 0 && count > 0)
                    count--;
                else
                    retval += results.Compound;
            }

            // SAFE GUARD AGAINST ANY ILLEGAL RETURN VALUES
            if (tweets.Count > 0)
            {
                try
                {
                    retval = retval / count;
                }
                catch
                {
                    return 0;
                }

                if (retval == Double.NaN || !Double.TryParse(retval.ToString(), out temp))
                    retval = 0;                
            }

            return retval;
        }

        public static async Task<double?> GetVaderAsync(string text)
        {
            SentimentIntensityAnalyzer analyzer = new SentimentIntensityAnalyzer();
            var results = analyzer.PolarityScores(text);
            return results.Compound;;
        }

        private static string Sanitize(string raw, string tweet)
        {
            string cleanedText = Regex.Replace(raw, @"http[^\s]+", "");
            cleanedText = cleanedText.Replace(tweet, "");            
            cleanedText = cleanedText.Replace(Environment.NewLine, "");
            cleanedText = Regex.Replace(cleanedText, @"\r\n?|\n", "");
            cleanedText = Regex.Replace(cleanedText, @"#\w+", "");
            cleanedText = Regex.Replace(cleanedText, @"@\w+", "");
            cleanedText = cleanedText.Replace("RT :", "");
            cleanedText = Regex.Replace(cleanedText, @"/[^A-Za-z0-9\s!?\u0000-\u0080\u0082]/g,''", "");
            return cleanedText.Trim();
        }
    }
}
