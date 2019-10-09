using System;
using System.Net;
using System.Web;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace FinaroEngine.Runner
{
    class Program
    {
        static void Main2(string[] args)
        {
            //string constring = @"Data Source=CHRIS\SQLEXPRESS;Initial Catalog=FinaroDB;persist security info=True; Integrated Security=SSPI;";
            //string constring = @"Data Source=VM-DEV-SQL\sql2014;Initial Catalog=Sandbox;persist security info=True; Integrated Security=SSPI;";

            //The URL endpoint for the blockchain network.
            string url = "https://ropsten.infura.io/a19089f9d4c54f518fca6588f04b43d2";

            //The contract address.
            string address = "0x08564660169a2559492EF1315FD2FBd17Bf84d54";

            //The ABI for the contract.
            string ABI = @"[ { 'constant': true, 'inputs': [], 'name': 'name', 'outputs': [ { 'name': '', 'type': 'string' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x06fdde03' }, { 'constant': true, 'inputs': [], 'name': 'decimals', 'outputs': [ { 'name': '', 'type': 'uint8' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x313ce567' }, { 'constant': false, 'inputs': [], 'name': 'acceptOwnership', 'outputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x79ba5097' }, { 'constant': true, 'inputs': [], 'name': 'owner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x8da5cb5b' }, { 'constant': true, 'inputs': [], 'name': 'symbol', 'outputs': [ { 'name': '', 'type': 'string' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x95d89b41' }, { 'constant': true, 'inputs': [], 'name': 'newOwner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xd4ee1d90' }, { 'constant': false, 'inputs': [ { 'name': '_newOwner', 'type': 'address' } ], 'name': 'transferOwnership', 'outputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xf2fde38b' }, { 'inputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'constructor', 'signature': 'constructor' }, { 'payable': true, 'stateMutability': 'payable', 'type': 'fallback' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': '_from', 'type': 'address' }, { 'indexed': true, 'name': '_to', 'type': 'address' } ], 'name': 'OwnershipTransferred', 'type': 'event', 'signature': '0x8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': 'from', 'type': 'address' }, { 'indexed': true, 'name': 'to', 'type': 'address' }, { 'indexed': false, 'name': 'tokens', 'type': 'uint256' } ], 'name': 'Transfer', 'type': 'event', 'signature': '0xddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': 'tokenOwner', 'type': 'address' }, { 'indexed': true, 'name': 'spender', 'type': 'address' }, { 'indexed': false, 'name': 'tokens', 'type': 'uint256' } ], 'name': 'Approval', 'type': 'event', 'signature': '0x8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925' }, { 'constant': true, 'inputs': [], 'name': 'contractOwner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xce606ee0' }, { 'constant': true, 'inputs': [], 'name': 'totalSupply', 'outputs': [ { 'name': '', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x18160ddd' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' } ], 'name': 'balanceOf', 'outputs': [ { 'name': 'balance', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x70a08231' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' }, { 'name': 'conversionRate', 'type': 'uint256' } ], 'name': 'balanceOfInEth', 'outputs': [ { 'name': 'balance', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xab604682' }, { 'constant': false, 'inputs': [ { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transfer', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xa9059cbb' }, { 'constant': false, 'inputs': [ { 'name': 'spender', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'approve', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x095ea7b3' }, { 'constant': false, 'inputs': [ { 'name': 'from', 'type': 'address' }, { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferFrom', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x23b872dd' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' }, { 'name': 'spender', 'type': 'address' } ], 'name': 'allowance', 'outputs': [ { 'name': 'remaining', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xdd62ed3e' }, { 'constant': false, 'inputs': [ { 'name': 'spender', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' }, { 'name': 'data', 'type': 'bytes' } ], 'name': 'approveAndCall', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xcae9ca51' }, { 'constant': false, 'inputs': [ { 'name': 'tokenAddress', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferAnyERC20Token', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xdc39d06d' } ]";

            string sender = "0x7D55aE48bC74b9047bb836488e54E06eF19Db74c";
            var privateKey = "466aa69475dd2bed841db861f9bf36d94ba3f8852195317e54718efda22978e8";
            var account = new Account(privateKey);
            //var account = new Account("privateKey"); // or load it from your keystore file as you are doing.
            var web3 = new Web3(account, url);

            //Creates the connecto to the network and gets an instance of the contract.
            //Web3 web3 = new Web3(url);
            Contract swayContract = web3.Eth.GetContract(ABI, address);
            Task<BigInteger> totalSupplyFunction = swayContract.GetFunction("totalSupply").CallAsync<BigInteger>();
            totalSupplyFunction.Wait();
            int totalSupply = (int)totalSupplyFunction.Result;       


            string beggar = "0xD64c013d4676F832D9BC69b4D65412dF6a393a76";


            HexBigInteger gas = new HexBigInteger(new BigInteger(400000));
            HexBigInteger value = new HexBigInteger(new BigInteger(0));
            
            var shit = swayContract.GetFunction("transfer").SendTransactionAsync(account.Address, gas, value, beggar, 90);//100 equals 1 sway
            shit.Wait();
            var result = shit.Result;
            string here = "";

            //Reads the vote count for Candidate 1 and Candidate 2
            /*
            Task<BigInteger> candidate1Function = voteContract.GetFunction("candidate1").CallAsync<BigInteger>();
            candidate1Function.Wait();
            int candidate1 = (int)candidate1Function.Result;
            Task<BigInteger> candidate2Function = voteContract.GetFunction("candidate2").CallAsync<BigInteger>();
            candidate2Function.Wait();
            int candidate2 = (int)candidate2Function.Result;
            Console.WriteLine("Candidate 1 votes: {0}", candidate1);
            Console.WriteLine("Candidate 2 votes: {0}", candidate2);

            //Prompts for the account address.
            Console.Write("Enter the address of your account: ");
            string accountAddress = Console.ReadLine();

            //Prompts for the users vote.
            int vote = 0;
            Console.Write("Press 1 to vote for candidate 1, Press 2 to vote for candidate 2: ");
            Int32.TryParse(Convert.ToChar(Console.Read()).ToString(), out vote);
            Console.WriteLine("You pressed {0}", vote);

            //Executes the vote on the contract.
            try
            {
                HexBigInteger gas = new HexBigInteger(new BigInteger(400000));
                HexBigInteger value = new HexBigInteger(new BigInteger(0));
                Task<string> castVoteFunction = voteContract.GetFunction("castVote").SendTransactionAsync(accountAddress, gas, value, vote);
                castVoteFunction.Wait();
                Console.WriteLine("Vote Cast!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            */
        }

        private static string API_KEY = "ad6e90dc-6a09-4285-8625-2ce6847c8847";
        private static string SANDBOX_KEY = "b7e4ecae-873a-4dd9-8a94-de3291b1193b";

        static void Main(string[] args)
        {
            try
            {
                var creds = GetCreds(false);
                Console.WriteLine(MarketCall(creds.url,creds.key));
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static string CoinCalls(string url, string key)
        {
            var URL = new UriBuilder($"https://{url}/v1/cryptocurrency/quotes/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["slug"] = "bitcoin,ethereum,electroneum,tomochain";

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", key);
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());
        }

        static string MarketCall(string url, string key)
        {
            var URL = new UriBuilder($"https://{url}/v1/global-metrics/quotes/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", SANDBOX_KEY);
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());
        }

        static (string url, string key) GetCreds(bool useProduction)
        {
            if (useProduction)
            {
                return ("pro-api.coinmarketcap.com", "ad6e90dc-6a09-4285-8625-2ce6847c8847");
            }
            else
            {
                return ("sandbox-api.coinmarketcap.com", "b7e4ecae-873a-4dd9-8a94-de3291b1193b");
            }
        }
    }
}
