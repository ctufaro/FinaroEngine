using FinaroEngine.Library;
using System;

namespace FinaroEngine.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            //string constring = @"Data Source=CHRIS\SQLEXPRESS;Initial Catalog=FinaroDB;persist security info=True; Integrated Security=SSPI;";
            string constring = @"Data Source=VM-DEV-SQL\sql2014;Initial Catalog=Sandbox;persist security info=True; Integrated Security=SSPI;";

            //string json = OrderProcess.AddNewOrder(constring, 1, 1, TradeType.Buy, 50, 5000);
            //string json = OrderProcess.GetNewOrders(constring, 1, 1);
            //Console.WriteLine(json);
            Console.WriteLine("Completed");
            Console.ReadLine();
        }
    }
}
