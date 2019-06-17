﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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

            foreach (JObject item in stuff)
            {
                foreach (var trend in item["trends"])
                {
                    try
                    {
                        int volume = 0;
                        Int32.TryParse(trend["tweet_volume"].ToString(), out volume);
                        List<SqlParameter> sparams = new List<SqlParameter>();

                        string trendName = trend["name"].ToString();
                        var tweets = GetTweetsAsync(trendName, 10, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret);
                        double? score = GetVaderSentAvgAsync(tweets.Result).Result;

                        sparams.Add(new SqlParameter("@NAME", trendName));
                        sparams.Add(new SqlParameter("@URL", trend["url"].ToString()));
                        sparams.Add(new SqlParameter("@TWEETVOLUME", volume));
                        sparams.Add(new SqlParameter("@AVGSENTIMENT", score));
                        FinaroEngine.Library.DBUtility.ExecuteQuery(sqlConnectionString, "spInsertTrend", sparams);
                    }
                    catch(Exception e)
                    {
                        LogError(e.ToString());
                    }
                }
            }
        }

        public static void ClearTrends(string sqlConnectionString)
        {
            List<SqlParameter> dummy = new List<SqlParameter> { new SqlParameter("@DUMMY", -1) };
            FinaroEngine.Library.DBUtility.ExecuteQuery(sqlConnectionString, "spClearTrends", dummy);
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
            dynamic stuff = JsonConvert.DeserializeObject(content);

            foreach (JObject item in stuff["statuses"])
            {
                tweets.Add(Sanitize(item["text"].ToString(), tweet));
            }

            return tweets;
        }        

        public static async Task<double?> GetVaderSentAvgAsync(List<string> tweets)
        {
            SentimentIntensityAnalyzer analyzer = new SentimentIntensityAnalyzer();
            double? retval = 0;
            int count = tweets.Count;

            foreach (string text in tweets)
            {
                var results = analyzer.PolarityScores(text);

                if (results.Compound == 0 && count > 0)
                    count--;
                else
                    retval += results.Compound;
            }

            if (tweets.Count > 0)
                retval = retval / count;

            return retval;
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