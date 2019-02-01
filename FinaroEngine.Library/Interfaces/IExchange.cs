using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    interface IExchange<T>
    {
        void Insert(T item);
    }
}
