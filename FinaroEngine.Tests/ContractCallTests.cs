using FinaroEngine.Library;
using System;
using System.Data;
using System.Threading.Tasks;
using Xunit;


namespace FinaroEngine.Tests
{
    public class ContractCallTests
    {
        private Options opts;

        public ContractCallTests()
        {
            opts.URL = "https://ropsten.infura.io/54b96774a4654d7287a593d687eef1e1"; //ROPSTEN
            opts.Address = "0x628322763cF6a2214bd04ab727E68FF11C13dcE0"; //ROPSTEN
            //opts.URL = "http://127.0.0.1:9545/";
            //opts.Address = "0xc59EfFF7150A0B946fD990EAd0a5663f9E6C66c3";
            opts.ABI = @"[ { 'constant': true, 'inputs': [], 'name': 'name', 'outputs': [ { 'name': '', 'type': 'string' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x06fdde03' }, { 'constant': true, 'inputs': [], 'name': 'decimals', 'outputs': [ { 'name': '', 'type': 'uint8' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x313ce567' }, { 'constant': false, 'inputs': [], 'name': 'acceptOwnership', 'outputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x79ba5097' }, { 'constant': true, 'inputs': [], 'name': 'owner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x8da5cb5b' }, { 'constant': true, 'inputs': [], 'name': 'symbol', 'outputs': [ { 'name': '', 'type': 'string' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x95d89b41' }, { 'constant': true, 'inputs': [], 'name': 'newOwner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xd4ee1d90' }, { 'constant': false, 'inputs': [ { 'name': '_newOwner', 'type': 'address' } ], 'name': 'transferOwnership', 'outputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xf2fde38b' }, { 'inputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'constructor', 'signature': 'constructor' }, { 'payable': true, 'stateMutability': 'payable', 'type': 'fallback' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': '_from', 'type': 'address' }, { 'indexed': true, 'name': '_to', 'type': 'address' } ], 'name': 'OwnershipTransferred', 'type': 'event', 'signature': '0x8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': 'from', 'type': 'address' }, { 'indexed': true, 'name': 'to', 'type': 'address' }, { 'indexed': false, 'name': 'tokens', 'type': 'uint256' } ], 'name': 'Transfer', 'type': 'event', 'signature': '0xddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': 'tokenOwner', 'type': 'address' }, { 'indexed': true, 'name': 'spender', 'type': 'address' }, { 'indexed': false, 'name': 'tokens', 'type': 'uint256' } ], 'name': 'Approval', 'type': 'event', 'signature': '0x8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925' }, { 'constant': true, 'inputs': [], 'name': 'contractOwner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xce606ee0' }, { 'constant': true, 'inputs': [], 'name': 'totalSupply', 'outputs': [ { 'name': '', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x18160ddd' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' } ], 'name': 'balanceOf', 'outputs': [ { 'name': 'balance', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0x70a08231' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' }, { 'name': 'conversionRate', 'type': 'uint256' } ], 'name': 'balanceOfInEth', 'outputs': [ { 'name': 'balance', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xab604682' }, { 'constant': false, 'inputs': [ { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transfer', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xa9059cbb' }, { 'constant': false, 'inputs': [ { 'name': 'spender', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'approve', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x095ea7b3' }, { 'constant': false, 'inputs': [ { 'name': 'from', 'type': 'address' }, { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferFrom', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0x23b872dd' }, { 'constant': false, 'inputs': [ { 'name': 'from', 'type': 'address' }, { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferFromOwner', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xb8dbf876' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' }, { 'name': 'spender', 'type': 'address' } ], 'name': 'allowance', 'outputs': [ { 'name': 'remaining', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function', 'signature': '0xdd62ed3e' }, { 'constant': false, 'inputs': [ { 'name': 'spender', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' }, { 'name': 'data', 'type': 'bytes' } ], 'name': 'approveAndCall', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xcae9ca51' }, { 'constant': false, 'inputs': [ { 'name': 'tokenAddress', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferAnyERC20Token', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function', 'signature': '0xdc39d06d' } ]";
            opts.SigningKey = "466aa69475dd2bed841db861f9bf36d94ba3f8852195317e54718efda22978e8";
        }

        [Fact]
        public async Task RetrieveUserAccountBalance()
        {
            string user = "0xD64c013d4676F832D9BC69b4D65412dF6a393a76";
            ContractCall contractCall = new ContractCall(opts);
            var result = await contractCall.GetUserBalanceAsync(user);
            Assert.True(result > 0);
        }

        [Fact]
        public async Task RetrieveTotalSupplyOfTokens()
        {
            ContractCall contractCall = new ContractCall(opts);
            var result = await contractCall.GetTotalSupplyAsync();
            Assert.True(result > 0);
        }

        [Fact]
        public async Task RetrieveContractAddressOfContractOwner()
        {
            ContractCall contractCall = new ContractCall(opts);
            var result = await contractCall.GetContractAddressAsync();
            Assert.Equal("0x08564660169a2559492ef1315fd2fbd17bf84d54", result);            
        }

        [Fact]
        public async Task SendSingleTokenFromContractToUser()
        {
            ContractCall contractCall = new ContractCall(opts);
            var result = await contractCall.SendTokensAsync(RopstenAccount.Mark, 1.23);            
        }

        [Fact]
        public async Task SendSingleTokenFromUserToUser()
        {
            ContractCall contractCall = new ContractCall(opts);
            var result = await contractCall.SendTokensFromAsync(RopstenAccount.Mark, RopstenAccount.Chris, .69);
        }
    }

}
