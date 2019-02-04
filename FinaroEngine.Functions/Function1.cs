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
            var conn = Environment.GetEnvironmentVariable("SQLConnectionString");
            var orders = OrderProcess.GetNewOrders(conn, userid, entityid);
            return new OkObjectResult(orders);
        }

        [FunctionName("createOrder")]
        public static Task CreateOrder([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "orders")]HttpRequest req, TraceWriter log, [SignalR(HubName = "exchange")]IAsyncCollector<SignalRMessage> signalRMessages)
        {
            log.Info("Creating a new order");
            var neworder = new JsonSerializer().Deserialize<Order>(new JsonTextReader(new StreamReader(req.Body)));
            var conn = Environment.GetEnvironmentVariable("SQLConnectionString");
            var orders = OrderProcess.AddNewOrder(conn, neworder.UserId, neworder.EntityId, (TradeType)neworder.TradeType, neworder.Price, neworder.Quantity);
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    UserId = "",
                    GroupName = "",
                    Target = "newOrders",
                    Arguments = new[] { orders }
                });
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
        }
    }
}
