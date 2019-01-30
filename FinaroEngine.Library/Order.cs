using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FinaroEngine.Library
{
    public class Order
    {
        public string OrderId { get; set; }
        public DateTime OrderPlacedOn { get; set;}
        public double Spread { get; set; }
        public double WagerAmount { get; set; }
        public Side Side { get; set; }
        public string Owner { get; set; }
        public Status Status { get; set; }
        public string StatusTxt
        {
            get
            {
                return Enum.GetName(typeof(Status), this.Status).ToUpper();
            }
        }
        public string OrderPlaceOnTxt
        {
            get
            {
                return OrderPlacedOn.ToString("MM/dd/yyyy HH:mm:ss");
            }
        }
    }
    
    public class Orders
    {
        public List<Order> AllOrders;
        public double TotalAmount;
        private object syncLock = new object();

        public Orders()
        {
            AllOrders = new List<Order>();
        }

        public void Add(Order order)
        {
            lock (syncLock)
            {
                TotalAmount += order.WagerAmount;
                AllOrders.Add(order);
            }
        }

        public void DecrementTotalAmount(double amount)
        {
            lock (syncLock)
            {
                TotalAmount = (TotalAmount - amount < 0) ? 0 : TotalAmount - amount;
            }
        }
    }
}
