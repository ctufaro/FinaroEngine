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
            OrderProcess op = new OrderProcess(new Options { ConnectionString = conn }, userid, entityid);
            var orders = op.GetNewOrders();
            return new OkObjectResult(orders);
        }

        [FunctionName("getTradeHistory")]
        public static IActionResult GetTradeHistory([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tradehistory/{userid}/{entityid}")]HttpRequest req, TraceWriter log, int userid, int entityid)
        {
            log.Info("Getting trade history");
            var conn = Environment.GetEnvironmentVariable("SQLConnectionString");
            OrderProcess op = new OrderProcess(new Options { ConnectionString = conn }, userid, entityid);
            var orders = op.GetTradeHistory();
            return new OkObjectResult(orders);
        }

        [FunctionName("getMyOrders")]
        public static IActionResult GetMyOrders([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "myorders/{userid}/{entityid}")]HttpRequest req, TraceWriter log, int userid, int entityid)
        {
            log.Info("Getting my orders");
            var conn = Environment.GetEnvironmentVariable("SQLConnectionString");
            OrderProcess op = new OrderProcess(new Options { ConnectionString = conn }, userid, entityid);
            var myOrders = op.GetMyOrders();
            return new OkObjectResult(myOrders);
        }

        [FunctionName("getMarketData")]
        public static IActionResult GetMarketData([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "market/{userid}/{entityid}")]HttpRequest req, TraceWriter log, int userid, int entityid)
        {
            log.Info("Getting market data");
            var conn = Environment.GetEnvironmentVariable("SQLConnectionString");
            OrderProcess op = new OrderProcess(new Options { ConnectionString = conn }, userid, entityid);
            var orders = op.GetMarketData();
            return new OkObjectResult(orders);
        }

        [FunctionName("getTeamPayers")]
        public static IActionResult GetTeamPlayers([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "teamplayers/{entitytypeid}/{entityleagueid}")]HttpRequest req, TraceWriter log, int entitytypeid, int entityleagueid)
        {
            log.Info("Getting Team Players");
            var conn = Environment.GetEnvironmentVariable("SQLConnectionString");
            DBTeamPlayer tp = new DBTeamPlayer(new Options { ConnectionString = conn });
            var teamPlayers = tp.GetTeamPlayers(entitytypeid, entityleagueid);
            return new OkObjectResult(teamPlayers);
        }

        [FunctionName("createOrder")]
        public static Task CreateOrder([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "orders")]HttpRequest req, TraceWriter log, [SignalR(HubName = "exchange")]IAsyncCollector<SignalRMessage> signalRMessages)
        {
            log.Info("Creating a new order");
            var neworder = new JsonSerializer().Deserialize<Order>(new JsonTextReader(new StreamReader(req.Body)));
            var conn = Environment.GetEnvironmentVariable("SQLConnectionString");
            Options opts = new Options();
            opts.ConnectionString = conn;
            opts.ABI = @"[ { 'constant': true, 'inputs': [], 'name': 'name', 'outputs': [ { 'name': '', 'type': 'string' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x06fdde03' }, { 'constant': true, 'inputs': [], 'name': 'decimals', 'outputs': [ { 'name': '', 'type': 'uint8' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x313ce567' }, { 'constant': false, 'inputs': [], 'name': 'acceptOwnership', 'outputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x79ba5097' }, { 'constant': true, 'inputs': [], 'name': 'owner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x8da5cb5b' }, { 'constant': true, 'inputs': [], 'name': 'symbol', 'outputs': [ { 'name': '', 'type': 'string' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x95d89b41' }, { 'constant': true, 'inputs': [], 'name': 'newOwner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xd4ee1d90' }, { 'constant': false, 'inputs': [ { 'name': '_newOwner', 'type': 'address' } ], 'name': 'transferOwnership', 'outputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xf2fde38b' }, { 'inputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'constructor', 'signature': 'constructor' }, { 'payable': true, 'stateMutability': 'payable', 'type': 'fallback' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': '_from', 'type': 'address' }, { 'indexed': true, 'name': '_to', 'type': 'address' } ], 'name': 'OwnershipTransferred', 'type': 'event', 'signature': '0x8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': 'from', 'type': 'address' }, { 'indexed': true, 'name': 'to', 'type': 'address' }, { 'indexed': false, 'name': 'tokens', 'type': 'uint256' } ], 'name': 'Transfer', 'type': 'event', 'signature': '0xddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': 'tokenOwner', 'type': 'address' }, { 'indexed': true, 'name': 'spender', 'type': 'address' }, { 'indexed': false, 'name': 'tokens', 'type': 'uint256' } ], 'name': 'Approval', 'type': 'event', 'signature': '0x8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925' }, { 'constant': true, 'inputs': [], 'name': 'contractOwner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xce606ee0' }, { 'constant': true, 'inputs': [], 'name': 'totalSupply', 'outputs': [ { 'name': '', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x18160ddd' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' } ], 'name': 'balanceOf', 'outputs': [ { 'name': 'balance', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x70a08231' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' }, { 'name': 'conversionRate', 'type': 'uint256' } ], 'name': 'balanceOfInEth', 'outputs': [ { 'name': 'balance', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xab604682' }, { 'constant': false, 'inputs': [ { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transfer', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xa9059cbb' }, { 'constant': false, 'inputs': [ { 'name': 'spender', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'approve', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x095ea7b3' }, { 'constant': false, 'inputs': [ { 'name': 'from', 'type': 'address' }, { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferFrom', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x23b872dd' }, { 'constant': false, 'inputs': [ { 'name': 'from', 'type': 'address' }, { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferFromOwner', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xb8dbf876' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' }, { 'name': 'spender', 'type': 'address' } ], 'name': 'allowance', 'outputs': [ { 'name': 'remaining', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xdd62ed3e' }, { 'constant': false, 'inputs': [ { 'name': 'spender', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' }, { 'name': 'data', 'type': 'bytes' } ], 'name': 'approveAndCall', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xcae9ca51' }, { 'constant': false, 'inputs': [ { 'name': 'tokenAddress', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferAnyERC20Token', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xdc39d06d' } ]";
            opts.URL = "https://ropsten.infura.io/54b96774a4654d7287a593d687eef1e1";
            opts.Address = "0x628322763cF6a2214bd04ab727E68FF11C13dcE0";
            OrderProcess op = new OrderProcess(opts, neworder.UserId, neworder.EntityId);
            opts.SigningKey = "466aa69475dd2bed841db861f9bf36d94ba3f8852195317e54718efda22978e8";
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
            Options opts = new Options();
            opts.ABI = @"[ { 'constant': true, 'inputs': [], 'name': 'name', 'outputs': [ { 'name': '', 'type': 'string' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x06fdde03' }, { 'constant': true, 'inputs': [], 'name': 'decimals', 'outputs': [ { 'name': '', 'type': 'uint8' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x313ce567' }, { 'constant': false, 'inputs': [], 'name': 'acceptOwnership', 'outputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x79ba5097' }, { 'constant': true, 'inputs': [], 'name': 'owner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x8da5cb5b' }, { 'constant': true, 'inputs': [], 'name': 'symbol', 'outputs': [ { 'name': '', 'type': 'string' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x95d89b41' }, { 'constant': true, 'inputs': [], 'name': 'newOwner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xd4ee1d90' }, { 'constant': false, 'inputs': [ { 'name': '_newOwner', 'type': 'address' } ], 'name': 'transferOwnership', 'outputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xf2fde38b' }, { 'inputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'constructor', 'signature': 'constructor' }, { 'payable': true, 'stateMutability': 'payable', 'type': 'fallback' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': '_from', 'type': 'address' }, { 'indexed': true, 'name': '_to', 'type': 'address' } ], 'name': 'OwnershipTransferred', 'type': 'event', 'signature': '0x8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': 'from', 'type': 'address' }, { 'indexed': true, 'name': 'to', 'type': 'address' }, { 'indexed': false, 'name': 'tokens', 'type': 'uint256' } ], 'name': 'Transfer', 'type': 'event', 'signature': '0xddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': 'tokenOwner', 'type': 'address' }, { 'indexed': true, 'name': 'spender', 'type': 'address' }, { 'indexed': false, 'name': 'tokens', 'type': 'uint256' } ], 'name': 'Approval', 'type': 'event', 'signature': '0x8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925' }, { 'constant': true, 'inputs': [], 'name': 'contractOwner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xce606ee0' }, { 'constant': true, 'inputs': [], 'name': 'totalSupply', 'outputs': [ { 'name': '', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x18160ddd' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' } ], 'name': 'balanceOf', 'outputs': [ { 'name': 'balance', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x70a08231' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' }, { 'name': 'conversionRate', 'type': 'uint256' } ], 'name': 'balanceOfInEth', 'outputs': [ { 'name': 'balance', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xab604682' }, { 'constant': false, 'inputs': [ { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transfer', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xa9059cbb' }, { 'constant': false, 'inputs': [ { 'name': 'spender', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'approve', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x095ea7b3' }, { 'constant': false, 'inputs': [ { 'name': 'from', 'type': 'address' }, { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferFrom', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x23b872dd' }, { 'constant': false, 'inputs': [ { 'name': 'from', 'type': 'address' }, { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferFromOwner', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xb8dbf876' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' }, { 'name': 'spender', 'type': 'address' } ], 'name': 'allowance', 'outputs': [ { 'name': 'remaining', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xdd62ed3e' }, { 'constant': false, 'inputs': [ { 'name': 'spender', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' }, { 'name': 'data', 'type': 'bytes' } ], 'name': 'approveAndCall', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xcae9ca51' }, { 'constant': false, 'inputs': [ { 'name': 'tokenAddress', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferAnyERC20Token', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xdc39d06d' } ]";
            opts.URL = "https://ropsten.infura.io/54b96774a4654d7287a593d687eef1e1";
            opts.Address = "0x628322763cF6a2214bd04ab727E68FF11C13dcE0";
            ContractCall contract = new ContractCall(opts);
            var balance = await contract.GetUserBalanceAsync(address);
            log.Info("Getting User Game Units");
            var conn = Environment.GetEnvironmentVariable("SQLConnectionString");
            OrderProcess op = new OrderProcess(new Options { ConnectionString = conn }, userid, 0);
            var units = op.GetMyBalance();
            return new OkObjectResult(new { WalletBalance = balance, UnitBalance = units });
        }

        [FunctionName("getUserUnits")]
        public static IActionResult GetUserUnits([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "balance/units/{userid}/{entityid}")]HttpRequest req, TraceWriter log, int userid, int entityid)
        {
            log.Info("Getting User Units");
            var conn = Environment.GetEnvironmentVariable("SQLConnectionString");
            OrderProcess op = new OrderProcess(new Options { ConnectionString = conn }, userid, entityid);
            var myUnits = op.GetMyUnits();
            return new OkObjectResult(myUnits);

        }

        [FunctionName("sendTokens")]
        public static async Task<object> SendTokens([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "contract/tokens")]HttpRequest req, TraceWriter log, [SignalR(HubName = "exchange")]IAsyncCollector<SignalRMessage> signalRMessages)
        {
            log.Info("Sending Tokens");
            var dreq = new JsonSerializer().Deserialize<Deposit>(new JsonTextReader(new StreamReader(req.Body)));
            var deposit = new Deposit { Address = dreq.Address, Amount = dreq.Amount };
            Options opts = new Options();
            opts.ABI = @"[ { 'constant': true, 'inputs': [], 'name': 'name', 'outputs': [ { 'name': '', 'type': 'string' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x06fdde03' }, { 'constant': true, 'inputs': [], 'name': 'decimals', 'outputs': [ { 'name': '', 'type': 'uint8' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x313ce567' }, { 'constant': false, 'inputs': [], 'name': 'acceptOwnership', 'outputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x79ba5097' }, { 'constant': true, 'inputs': [], 'name': 'owner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x8da5cb5b' }, { 'constant': true, 'inputs': [], 'name': 'symbol', 'outputs': [ { 'name': '', 'type': 'string' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x95d89b41' }, { 'constant': true, 'inputs': [], 'name': 'newOwner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xd4ee1d90' }, { 'constant': false, 'inputs': [ { 'name': '_newOwner', 'type': 'address' } ], 'name': 'transferOwnership', 'outputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xf2fde38b' }, { 'inputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'constructor', 'signature': 'constructor' }, { 'payable': true, 'stateMutability': 'payable', 'type': 'fallback' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': '_from', 'type': 'address' }, { 'indexed': true, 'name': '_to', 'type': 'address' } ], 'name': 'OwnershipTransferred', 'type': 'event', 'signature': '0x8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': 'from', 'type': 'address' }, { 'indexed': true, 'name': 'to', 'type': 'address' }, { 'indexed': false, 'name': 'tokens', 'type': 'uint256' } ], 'name': 'Transfer', 'type': 'event', 'signature': '0xddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': 'tokenOwner', 'type': 'address' }, { 'indexed': true, 'name': 'spender', 'type': 'address' }, { 'indexed': false, 'name': 'tokens', 'type': 'uint256' } ], 'name': 'Approval', 'type': 'event', 'signature': '0x8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925' }, { 'constant': true, 'inputs': [], 'name': 'contractOwner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xce606ee0' }, { 'constant': true, 'inputs': [], 'name': 'totalSupply', 'outputs': [ { 'name': '', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x18160ddd' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' } ], 'name': 'balanceOf', 'outputs': [ { 'name': 'balance', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x70a08231' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' }, { 'name': 'conversionRate', 'type': 'uint256' } ], 'name': 'balanceOfInEth', 'outputs': [ { 'name': 'balance', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xab604682' }, { 'constant': false, 'inputs': [ { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transfer', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xa9059cbb' }, { 'constant': false, 'inputs': [ { 'name': 'spender', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'approve', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x095ea7b3' }, { 'constant': false, 'inputs': [ { 'name': 'from', 'type': 'address' }, { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferFrom', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x23b872dd' }, { 'constant': false, 'inputs': [ { 'name': 'from', 'type': 'address' }, { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferFromOwner', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xb8dbf876' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' }, { 'name': 'spender', 'type': 'address' } ], 'name': 'allowance', 'outputs': [ { 'name': 'remaining', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xdd62ed3e' }, { 'constant': false, 'inputs': [ { 'name': 'spender', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' }, { 'name': 'data', 'type': 'bytes' } ], 'name': 'approveAndCall', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xcae9ca51' }, { 'constant': false, 'inputs': [ { 'name': 'tokenAddress', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferAnyERC20Token', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xdc39d06d' } ]";
            opts.URL = "https://ropsten.infura.io/54b96774a4654d7287a593d687eef1e1";
            opts.Address = "0x628322763cF6a2214bd04ab727E68FF11C13dcE0";
            opts.SigningKey = "466aa69475dd2bed841db861f9bf36d94ba3f8852195317e54718efda22978e8";
            ContractCall contract = new ContractCall(opts);
            var tokenSent = await contract.SendTokensAsync(deposit.Address, deposit.Amount);
            return new OkObjectResult(tokenSent);
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
