using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public interface IMyOrder
    {
        IEnumerable<MyOrder> GetMyOrder(int userId);
    }
}
