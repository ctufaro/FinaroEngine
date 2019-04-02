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

    public enum MarginMove
    {
        BalanceToMargin = 0,
        MarginToBalance = 1,
        MarginToMargin = 2
    }

    public class RopstenAccount
    {
        public const string Admin = "0xa4eA2c04E92556a165cE4c9C8f6cd5Ead2A1EB79";
        public const string Chris = "0x2f7E50C572b51c2352636ca0Be931Ce5B26b95e4";
        public const string Mark = "0xfD1F298A6B5dB4E9dAedd7098De056Bc62e693e9";
    }

    public class LocalhostAccount
    {
        public const string Admin = "0xa4ea2c04e92556a165ce4c9c8f6cd5ead2a1eb79";
        public const string Chris = "0x2f7e50c572b51c2352636ca0be931ce5b26b95e4";
        public const string Mark = "0xfd1f298a6b5db4e9daedd7098de056bc62e693e9";
    }
}
