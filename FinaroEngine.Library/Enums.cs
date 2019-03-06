using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public enum Side
    {
        Plus = 1,
        Minus = 2
    }

    public enum Status
    {
        Open = 1,
        Partial = 2,
        Filled = 3,
        Cancelled = 4
    }

    public enum TradeType
    {
        Buy = 1,
        Sell = 2,
        ShortSell = 3,
        BuyToCover = 4
    }

    public enum EntityType
    {
        Team = 1,
        Player = 2
    }

    public enum EntityLeague
    {
        MLB = 1,
        NFL = 2,
        NHL = 3,
        NBA = 4
    }

    public class RopstenAccount
    {
        public const string Chris = "0xD64c013d4676F832D9BC69b4D65412dF6a393a76";
        public const string Mark = "0x8E86638C68BB5342F281D96f772f1447A40425D5";
    }
}
