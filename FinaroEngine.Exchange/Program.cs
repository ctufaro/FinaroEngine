using System;
using System.Threading;
using System.Threading.Tasks;
using FinaroEngine.Library;

namespace FinaroEngine.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                string key = "sway";
                Exchange.Clear(key);
                Exchange exchange = new Exchange(key);
                for (int i = 0; i < 10000; i++)
                {
                    SpreadData sd = new SpreadData(12, 100, Side.Plus, DateTime.Now);
                    await exchange.InsertAsync(sd);
                }

                Console.WriteLine("Completed");
                
            });

            Console.ReadLine();
        }
    }

}
