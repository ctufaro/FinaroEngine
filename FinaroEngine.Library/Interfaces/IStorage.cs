using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    interface IStorage<K,T>
    {
        T Insert(T item);
        T Get(K key);
        T Update(T item);
        void Remove(K key);
    }
}
