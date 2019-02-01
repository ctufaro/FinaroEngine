using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//https://github.com/imperugo/StackExchange.Redis.Extensions
namespace FinaroEngine.Library
{
    public class Exchange
    {
        string host;
        int port;
        string key;

        public Exchange(string key, string host = "localhost", int port = 6379)
        {
            //lets get the orderbook from Redis
            this.host = host;
            this.port = port;
            this.key = key;
        }

        public async Task InsertAsync(SpreadData item)
        {            

            var redisConfiguration = new RedisConfiguration();
            redisConfiguration.Hosts = new RedisHost[]
            {
                new RedisHost(){Host = host, Port = port}
            };
            var serializer = new NewtonsoftSerializer();
            using (var cacheClient = new StackExchangeRedisCacheClient(serializer, redisConfiguration))
            {          
                OrderBook ob = await cacheClient.GetAsync<OrderBook>(this.key);
                if(ob == null)
                {
                    ob = new OrderBook();
                }
                ob.Add(item);
                bool added = await cacheClient.AddAsync(key, ob);
                var cachedUser = await cacheClient.GetAsync<OrderBook>(key);
            }
        }

        public static void Clear(string key, string host = "localhost", int port = 6379)
        {
            var redisConfiguration = new RedisConfiguration();
            
            redisConfiguration.Hosts = new RedisHost[]
            {
                new RedisHost(){Host = host, Port = port}
            };

            var serializer = new NewtonsoftSerializer();

            using (var cacheClient = new StackExchangeRedisCacheClient(serializer, redisConfiguration))
            {
                cacheClient.Remove(key);
            }
                            
        }

        public void Print()
        {
            var redisConfiguration = new RedisConfiguration();

            redisConfiguration.Hosts = new RedisHost[]
            {
                new RedisHost(){Host = host, Port = port}
            };

            var serializer = new NewtonsoftSerializer();

            using (var cacheClient = new StackExchangeRedisCacheClient(serializer, redisConfiguration))
            {
                OrderBook ob = cacheClient.Get<OrderBook>(this.key);
                if (ob == null)
                {
                    Console.WriteLine("Empty");
                }
                else
                {
                    var cachedUser = cacheClient.Get<OrderBook>(this.key);
                    Console.WriteLine(cachedUser);
                }
            }
        }
    }

}
