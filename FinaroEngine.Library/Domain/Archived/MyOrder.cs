using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public class MyOrder
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int EntityId { get; set; }
        public int TradeTypeId { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public int UnsetQuantity { get; set; }
        public int Status { get; set; }
        public string TxHash { get; set; }
    }
}
