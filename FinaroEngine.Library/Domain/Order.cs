using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public class Order
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public int UserId { get; set; }
        public int TrendId { get; set; }
        public int TradeTypeId { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public int UnsetQuantity { get; set; }
        public string PublicKey { get; set; }
        public int Status { get; set; }
    }
}
