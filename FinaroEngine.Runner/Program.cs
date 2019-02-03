using FinaroEngine.Library;
using System;

namespace FinaroEngine.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderProcess.ProcessNewOrder(1, 1, TradeType.Sell, 123, 100);
            Console.WriteLine("Completed");
            Console.ReadLine();
        }
    }
}
