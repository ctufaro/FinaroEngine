﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public class OrderProcess
    {
        private IOrder order;
        private IMarketData market;
        private ITradeHistory history;
        private IMyOrder myOrder;
        private Options opts;
        private int userId;
        private int entityId;

        public OrderProcess(Options opts, int userId, int entityId)
        {
            order = new Orders(opts, userId, entityId);
            market = new DBMarket(opts, userId, entityId);
            history = new DBTradeHistory(opts, userId, entityId);
            myOrder = new DBMyOrder(opts, userId);
            this.opts = opts;
            this.userId = userId;
            this.entityId = entityId;
        }

        public string AddNewOrder(TradeType tradeType, decimal price, int quantity, int unsetQuantity, string publicKey)
        {
            IContractCall contractCall = new ContractCall(opts);
            var orders = order.AddNewOrder(new Order { TradeTypeId = (int)tradeType, Price = price, Quantity = quantity, UnsetQuantity = unsetQuantity, PublicKey = publicKey }, contractCall);
            return JsonConvert.SerializeObject(orders);
        }

        public string GetNewOrders()
        {
            var orders = order.GetOrders(this.userId, this.entityId);
            return JsonConvert.SerializeObject(orders);
        }

        public string GetMarketData()
        {
            var marketData = market.GetMarketData(this.userId, this.entityId);
            return JsonConvert.SerializeObject(marketData);
        }

        public string GetTradeHistory()
        {
            var tradeHistoryData = history.GetTradeHistory(this.userId, this.entityId);
            return JsonConvert.SerializeObject(tradeHistoryData);
        }

        public string GetMyOrders()
        {
            var myOrders = myOrder.GetMyOrder(this.userId);
            return JsonConvert.SerializeObject(myOrders);
        }

        public string GetMyBalance()
        {
            var myBalance = myOrder.GetMyBalance(this.userId);
            return JsonConvert.SerializeObject(myBalance);
        }
        
        public string GetMyUnits()
        {
            var myUnits = myOrder.GetMyUnits(this.userId, this.entityId);
            return JsonConvert.SerializeObject(myUnits);
        }

    }
}
