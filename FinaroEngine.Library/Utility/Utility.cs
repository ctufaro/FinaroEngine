using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public class Utility
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }

        public static DateTime RandomDay()
        {
            lock (syncLock)
            {
                //DateTime start = new DateTime(1995, 1, 1);
                //DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-5).Day);
                //int range = (DateTime.Today - start).Days;
                //return start.AddDays(random.Next(range));

                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-5).Day);
                DateTime endDate = DateTime.Now;

                TimeSpan timeSpan = endDate - startDate;
                var randomTest = new Random();
                TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
                DateTime newDate = startDate + newSpan;
                return newDate;
            }
        }


        public static decimal[] ConvertToDecArray(string priceHistory, bool exportAll)
        {
            int totalEntries = 5;
            priceHistory = priceHistory.Trim();
            priceHistory = (priceHistory.EndsWith(',')) ? priceHistory.Remove(priceHistory.Length - 1) : priceHistory;
            decimal[] retval = new decimal[5] { 0, 0, 0, 0, 0 };
            if (priceHistory == null)
            {
                return retval;
            }
            try
            {
                var array = Array.ConvertAll(priceHistory.Split(','), Decimal.Parse);
                
                // <= 5 OR  exportAll is true
                if(array.Length <= totalEntries || exportAll)
                {
                    return array;
                }
                
                // > 5
                else if (array.Length > totalEntries)
                {
                    int start = array.Length - totalEntries;
                    decimal[] newValues = new ArraySegment<decimal>(array, start, totalEntries).ToArray();
                    return newValues;
                }
                
                return array;
            }
            catch(Exception e)
            {
                string error = e.ToString();
            }
            return retval;
        }

        public static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
    }
}
