using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public class SpreadData
    {
        public double Spread { get; set; }
        public double Amount { get; set; }
        public Side Side { get; set; }
        public DateTime? OrderPlacedOn { get; set; }
        public string Owner { get; set; }
        public string OrderId { get; set; }

        public SpreadData(double Spread, double Amount, Side Side, DateTime? OrderPlacedOn, string Owner = null, string OrderId = null)
        {
            this.Spread = Spread;
            this.Amount = Amount;
            this.Side = Side;
            this.OrderPlacedOn = OrderPlacedOn;
            this.Owner = Owner;
            this.OrderId = OrderId;
        }
    }
}
