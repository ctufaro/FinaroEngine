// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Newtonsoft.Json;
using FinaroEngine.Library;
using Microsoft.Azure.WebJobs.Host;

namespace FinaroEngine.Functions
{
    public static class Function1
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetSignalRInfo([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req, [SignalRConnectionInfo(HubName = "exchange")] SignalRConnectionInfo connectionInfo)
        {            
            return connectionInfo;
        }
        
        [FunctionName("getOrders")]
        public static IActionResult GetOrders([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "orders/{userid}/{entityid}")]HttpRequest req, TraceWriter log, int userid, int entityid)
        {
            log.Info("Getting all orders");
            Options opts = GetOptions();
            OrderProcess op = new OrderProcess(opts, userid, entityid);
            var orders = op.GetNewOrders();
            return new OkObjectResult(orders);
        }

        [FunctionName("getTradeHistory")]
        public static IActionResult GetTradeHistory([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tradehistory/{userid}/{entityid}")]HttpRequest req, TraceWriter log, int userid, int entityid)
        {
            log.Info("Getting trade history");
            Options opts = GetOptions();
            OrderProcess op = new OrderProcess(opts, userid, entityid);
            var orders = op.GetTradeHistory();
            return new OkObjectResult(orders);
        }

        [FunctionName("getMyOrders")]
        public static IActionResult GetMyOrders([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "myorders/{userid}/{entityid}")]HttpRequest req, TraceWriter log, int userid, int entityid)
        {
            log.Info("Getting my orders");
            Options opts = GetOptions();
            OrderProcess op = new OrderProcess(opts, userid, entityid);
            var myOrders = op.GetMyOrders();
            return new OkObjectResult(myOrders);
        }

        [FunctionName("getMarketData")]
        public static IActionResult GetMarketData([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "market/{userid}/{entityid}")]HttpRequest req, TraceWriter log, int userid, int entityid)
        {
            log.Info("Getting market data");
            Options opts = GetOptions();
            OrderProcess op = new OrderProcess(opts, userid, entityid);
            var orders = op.GetMarketData();
            return new OkObjectResult(orders);
        }

        [FunctionName("getTeamPayers")]
        public static IActionResult GetTeamPlayers([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "teamplayers/{entitytypeid}/{entityleagueid}")]HttpRequest req, TraceWriter log, int entitytypeid, int entityleagueid)
        {
            log.Info("Getting Team Players");
            Options opts = GetOptions();
            DBTeamPlayer tp = new DBTeamPlayer(opts);
            var teamPlayers = tp.GetTeamPlayers(entitytypeid, entityleagueid);
            return new OkObjectResult(teamPlayers);
        }

        [FunctionName("createOrder")]
        public static Task CreateOrder([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "orders")]HttpRequest req, TraceWriter log, [SignalR(HubName = "exchange")]IAsyncCollector<SignalRMessage> signalRMessages)
        {
            log.Info("Creating a new order");
            var neworder = new JsonSerializer().Deserialize<Order>(new JsonTextReader(new StreamReader(req.Body)));
            Options opts = GetOptions();
            OrderProcess op = new OrderProcess(opts, neworder.UserId, neworder.EntityId);
            var orders = op.AddNewOrder((TradeType)neworder.TradeType, neworder.Price, neworder.Quantity, neworder.UnsetQuantity, neworder.PublicKey);
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    UserId = "",
                    GroupName = "",
                    Target = "newOrders",
                    Arguments = new[] { orders }
                });
        }

        [FunctionName("getUserBalance")]
        public static async Task<object> GetUserBalance([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "contract/balance/{userid}/{address}")]HttpRequest req, TraceWriter log, int userid, string address)
        {
            log.Info("Getting User Balance");
            log.Info("Getting User Token Balance");
            Options opts = GetOptions();
            ContractCall contract = new ContractCall(opts);
            var balance = await contract.GetUserBalanceAsync(address);
            log.Info("Getting User Game Units");
            OrderProcess op = new OrderProcess(opts, userid, 0);
            var units = op.GetMyBalance();
            return new OkObjectResult(new { WalletBalance = balance, UnitBalance = units });
        }

        [FunctionName("getUserUnits")]
        public static IActionResult GetUserUnits([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "balance/units/{userid}/{entityid}")]HttpRequest req, TraceWriter log, int userid, int entityid)
        {
            log.Info("Getting User Units");
            Options opts = GetOptions();
            OrderProcess op = new OrderProcess(opts, userid, entityid);
            var myUnits = op.GetMyUnits();
            return new OkObjectResult(myUnits);
        }

        [FunctionName("sendTokens")]
        public static async Task<object> SendTokens([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "contract/tokens")]HttpRequest req, TraceWriter log, [SignalR(HubName = "exchange")]IAsyncCollector<SignalRMessage> signalRMessages)
        {
            log.Info("Sending Tokens");
            var dreq = new JsonSerializer().Deserialize<Deposit>(new JsonTextReader(new StreamReader(req.Body)));
            var deposit = new Deposit { Address = dreq.Address, Amount = dreq.Amount };
            Options opts = GetOptions();
            ContractCall contract = new ContractCall(opts);
            var tokenSent = await contract.SendTokensAsync(deposit.Address, deposit.Amount, opts.GasAmount);
            return new OkObjectResult(tokenSent);
        }

        [FunctionName("getMarginBalance")]
        public static async Task<object> GetMarginBalance([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "contract/margin/{address}")]HttpRequest req, TraceWriter log, string address)
        {
            log.Info("Getting Margin Balance");
            var dreq = new JsonSerializer().Deserialize<Deposit>(new JsonTextReader(new StreamReader(req.Body)));
            Options opts = GetOptions();
            ContractCall contract = new ContractCall(opts);
            var marginBalance = await contract.GetUserMarginBalanceAsync(address);
            return new OkObjectResult(marginBalance);
        }

        [FunctionName("sendFeedback")]
        public static OkObjectResult SendFeedback([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "feedback")]HttpRequest req, TraceWriter log, [SignalR(HubName = "exchange")]IAsyncCollector<SignalRMessage> signalRMessages)
        {
            log.Info("Creating feedback");
            string response = new StreamReader(req.Body).ReadToEnd();
            var fb = JsonConvert.DeserializeAnonymousType(response, new { UserId = 0, Feedback = "" });            
            Options opts = GetOptions();
            Feedback feedback = new Feedback(opts, fb.UserId);
            Task<string> result = feedback.Save(Environment.GetEnvironmentVariable("EmailAPIKey"),
                                                Environment.GetEnvironmentVariable("EmailRecipients"),
                                                fb.Feedback);
            result.Wait();
            return new OkObjectResult(result.Result);
        }

        public static Options GetOptions()
        {
            Options opts = new Options();
            opts.ConnectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
            opts.ABI = Environment.GetEnvironmentVariable("ContractABI");
            opts.URL = Environment.GetEnvironmentVariable("ContractURL");
            opts.ContractAddress = Environment.GetEnvironmentVariable("ContractAddress");
            opts.SigningKey = Environment.GetEnvironmentVariable("ContractSigningKey");
            opts.GasAmount = Convert.ToInt32(Environment.GetEnvironmentVariable("GasAmount"));
            return opts;
        }

   
        /*
        [FunctionName("messages")]
        public static Task SendMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequest req,
            [SignalR(HubName = "exchange")]IAsyncCollector<SignalRMessage> signalRMessages)
        {

            var message = new JsonSerializer().Deserialize<ChatMessage>(new JsonTextReader(new StreamReader(req.Body)));

            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    UserId = message.recipient,
                    GroupName = message.groupname,
                    Target = "newMessage",
                    Arguments = new[] { message }
                });
        }


        [Obsolete("Function Not Being Used")]
        [FunctionName("addToGroup")]
        public static Task AddToGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequest req,
            [SignalR(HubName = "exchange")]IAsyncCollector<SignalRGroupAction> signalRGroupActions)
        {

            var message = new JsonSerializer().Deserialize<ChatMessage>(new JsonTextReader(new StreamReader(req.Body)));


            return signalRGroupActions.AddAsync(
                new SignalRGroupAction
                {
                    UserId = message.recipient,
                    GroupName = message.groupname,
                    Action = GroupAction.Add
                });
        }

        [Obsolete("Function Not Being Used")]
        [FunctionName("removeFromGroup")]
        public static Task RemoveFromGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequest req,
            [SignalR(HubName = "exchange")]IAsyncCollector<SignalRGroupAction> signalRGroupActions)
        {

            var message = new JsonSerializer().Deserialize<ChatMessage>(new JsonTextReader(new StreamReader(req.Body)));


            return signalRGroupActions.AddAsync(
                new SignalRGroupAction
                {
                    UserId = message.recipient,
                    GroupName = message.groupname,
                    Action = GroupAction.Remove
                });
        }

        public class ChatMessage
        {
            public string sender { get; set; }
            public string text { get; set; }
            public string groupname { get; set; }
            public string recipient { get; set; }
            public bool isPrivate { get; set; }
        }

        public class Shit
        {
            public string ShitText { get; set; }
        }
        */

        public class Order
        {
            public int UserId { get; set; }
            public int EntityId { get; set; }
            public int TradeType { get; set; }
            public Decimal Price { get; set; }
            public int Quantity { get; set; }
            public int UnsetQuantity { get; set; }
            public string PublicKey { get; set; }
        }

        public class Deposit
        {
            public string Address { get; set; }
            public double Amount { get; set; }
        }
                
    }
}
