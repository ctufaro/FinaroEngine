using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    class RedisStorage : IStorage<string, SortedDictionary<double, Orders>>
    {
        public SortedDictionary<double, Orders> Get(string key)
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<double, Orders> Insert(SortedDictionary<double, Orders> item)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<double, Orders> Update(SortedDictionary<double, Orders> item)
        {
            throw new NotImplementedException();
        }
    }
}
