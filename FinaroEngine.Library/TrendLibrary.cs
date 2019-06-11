using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinaroEngine.Library
{
    public class TrendLibrary
    {
        public static void LoadTrends(string sqlConnectionString, string twitterConsumerKey, string twitterConsumerSecret, string twitterAccessToken, string twitterAccessTokenSecret)
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
                    //Console.WriteLine($"Hello, Name:{trend["name"]} Url:{trend["url"]} Volume:{trend["tweet_volume"]}");
                    //"name": "#ChainedToTheRhythm", "url": "http://twitter.com/search?q=%23ChainedToTheRhythm", "promoted_content": null, "query": "%23ChainedToTheRhythm", "tweet_volume": 48857
                    //Console.WriteLine($"Name:{trend["name"]} Volume:{trend["tweet_volume"]}");

                    int volume = 0;
                    Int32.TryParse(trend["tweet_volume"].ToString(), out volume);
                    List<SqlParameter> sparams = new List<SqlParameter>();
                    sparams.Add(new SqlParameter("@NAME", trend["name"].ToString()));
                    sparams.Add(new SqlParameter("@URL", trend["url"].ToString()));
                    sparams.Add(new SqlParameter("@TWEETVOLUME", volume));
                    FinaroEngine.Library.DBUtility.ExecuteQuery(sqlConnectionString, "spInsertTrend", sparams);
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
                tweets.Add(item["text"].ToString());
            }

            return tweets;
        }

        public static async Task<double?> GetSentimentAvgAsync(string endpoint, string key, List<string> texts)
        {
            var credentials = new ApiKeyServiceClientCredentials(key);
            var client = new TextAnalyticsClient(credentials)
            {
                Endpoint = endpoint
            };

            int count = 1;
            List<MultiLanguageInput> inputs = new List<MultiLanguageInput>();
            foreach (string text in texts)
            {
                inputs.Add(new MultiLanguageInput("en", (count++).ToString(), text));
            }
            var inputDocuments = new MultiLanguageBatchInput(inputs);
            var result = await client.SentimentAsync(false, inputDocuments);
            var avgScore = result.Documents.Average(x => x.Score);
            return avgScore;
        }

    }
}
