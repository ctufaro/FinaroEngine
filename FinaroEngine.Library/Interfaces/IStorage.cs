﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public interface IStorage<T>
    {
        void Init();
        T Insert(T item);
    }
}
