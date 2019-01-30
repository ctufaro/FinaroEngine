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
    }
}
