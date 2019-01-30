using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinaroEngine.Library
{
    public class OrderBook
    {
        public SortedDictionary<double, Orders> Book1;
        public SortedDictionary<double, Orders> Book2;
        private object syncLock = new object();

        public OrderBook()
        {
            Book1 = new SortedDictionary<double, Orders>();
            Book2 = new SortedDictionary<double, Orders>();
        }

        public void Add(double _spread, double _amount, Side _side, DateTime? _orderPlacedOn = null, string _owner = null, string _orderId = null)
        {
            Orders orders = null;
            DateTime orderPlaced = (_orderPlacedOn == null) ? DateTime.Now : _orderPlacedOn.Value;
            Order myOrder = new Order { OrderPlacedOn = orderPlaced, Spread = _spread, WagerAmount = _amount, Side = _side, Status = Status.Open, OrderId = _orderId };            
            var whichBook = (_side == Side.Plus) ? Book1 : Book2;

            if (whichBook.ContainsKey(_spread))
            {
                orders = whichBook[_spread];

                lock (syncLock)
                {
                    orders.Add(myOrder);
                }
            }
            else
            {
                orders = new Orders();

                lock (syncLock)
                {
                    orders.Add(myOrder);
                    whichBook.Add(_spread, orders);
                }
                
            }

            Orders homeBookOrders = whichBook[_spread];
            CheckForMatchingOrders(_side, myOrder, ref homeBookOrders);
        }

        public void CheckForMatchingOrders(Side _side, Order _myOrder, ref Orders _homeBookOrders)
        {
            SortedDictionary<double, Orders> checkBook = new SortedDictionary<double, Orders>();
            List<double> checkSpreads = new List<double>();

            //If the order is on the (+) side, look for bets on the (-) side where the spreads are greater than or equal to _myOrder.Spread
            if (_side == Side.Plus)
            {
                checkBook = Book2;
                checkSpreads = checkBook.Where(x => Math.Abs(x.Key) >= _myOrder.Spread).Select(s => s.Key).ToList();
            }

            //If the order is on the (-) side, look for bets on the (+) side where the spreads are less than or equal to _myOrder.Spread
            if (_side == Side.Minus)
            {
                checkBook = Book1;
                checkSpreads = checkBook.Where(x => x.Key <= Math.Abs(_myOrder.Spread)).Select(s => s.Key).ToList();
            }

            foreach(double spread in checkSpreads)
            {
                Orders _opsideOrders = checkBook[spread];
                foreach(var opside in _opsideOrders.AllOrders)
                {
                    double difference = _myOrder.WagerAmount - opside.WagerAmount;

                    if (difference == 0)
                    {
                        _homeBookOrders.DecrementTotalAmount(opside.WagerAmount);
                        _myOrder.WagerAmount = 0;
                        _myOrder.Status = Status.Filled;
                        _opsideOrders.DecrementTotalAmount(opside.WagerAmount);                        
                        opside.WagerAmount = 0;
                        opside.Status = Status.Filled;
                        break;
                    }
                    else if(difference < 0)
                    {
                        _homeBookOrders.DecrementTotalAmount(_myOrder.WagerAmount);
                        _myOrder.WagerAmount = 0;
                        _myOrder.Status = Status.Filled;
                        _opsideOrders.DecrementTotalAmount(_myOrder.WagerAmount);
                        opside.WagerAmount = Math.Abs(difference);
                        opside.Status = Status.Partial;
                        break;
                    }
                    else if (difference < _myOrder.WagerAmount)
                    {
                        _homeBookOrders.DecrementTotalAmount(opside.WagerAmount);                        
                        _myOrder.WagerAmount = difference;
                        _myOrder.Status = Status.Partial;
                        _opsideOrders.DecrementTotalAmount(opside.WagerAmount);
                        opside.WagerAmount = 0;
                        opside.Status = Status.Filled;
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach(var orders in Book1.Values)
            {
                foreach(Order o in orders.AllOrders)
                {
                    string fmt = "{0}: {1} {2} @ {3} [{4}]";
                    sb.AppendLine(string.Format(fmt, "Book 1", o.Spread, o.WagerAmount, o.OrderPlacedOn.ToString(), o.Status));
                }
            }

            foreach (var orders in Book2.Values)
            {
                foreach (Order o in orders.AllOrders)
                {
                    string fmt = "{0}: {1} {2} @ {3} [{4}]";
                    sb.AppendLine(string.Format(fmt, "Book 2", o.Spread, o.WagerAmount, o.OrderPlacedOn.ToString(), o.Status));

                }
            }

            return sb.ToString();
        }

        public void Clear()
        {
            Book1 = new SortedDictionary<double, Orders>();
            Book2 = new SortedDictionary<double, Orders>();
        }
    }
}
