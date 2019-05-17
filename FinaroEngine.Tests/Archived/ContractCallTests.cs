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
            opts = new Options();
            opts.URL = "https://ropsten.infura.io/54b96774a4654d7287a593d687eef1e1";
            opts.ABI = @" [ { 'constant': true, 'inputs': [], 'name': 'name', 'outputs': [ { 'name': '', 'type': 'string' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function' }, { 'constant': true, 'inputs': [], 'name': 'decimals', 'outputs': [ { 'name': '', 'type': 'uint8' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function' }, { 'constant': false, 'inputs': [], 'name': 'acceptOwnership', 'outputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function' }, { 'constant': true, 'inputs': [], 'name': 'owner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function' }, { 'constant': true, 'inputs': [], 'name': 'symbol', 'outputs': [ { 'name': '', 'type': 'string' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function' }, { 'constant': true, 'inputs': [], 'name': 'newOwner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function' }, { 'constant': false, 'inputs': [ { 'name': '_newOwner', 'type': 'address' } ], 'name': 'transferOwnership', 'outputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function' }, { 'inputs': [], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'constructor' }, { 'payable': true, 'stateMutability': 'payable', 'type': 'fallback' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': '_from', 'type': 'address' }, { 'indexed': true, 'name': '_to', 'type': 'address' } ], 'name': 'OwnershipTransferred', 'type': 'event' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': 'from', 'type': 'address' }, { 'indexed': true, 'name': 'to', 'type': 'address' }, { 'indexed': false, 'name': 'tokens', 'type': 'uint256' } ], 'name': 'Transfer', 'type': 'event' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': 'from', 'type': 'address' }, { 'indexed': true, 'name': 'to', 'type': 'address' }, { 'indexed': false, 'name': 'tokens', 'type': 'uint256' }, { 'indexed': false, 'name': 'destorigin', 'type': 'uint256' } ], 'name': 'Margin', 'type': 'event' }, { 'anonymous': false, 'inputs': [ { 'indexed': true, 'name': 'tokenOwner', 'type': 'address' }, { 'indexed': true, 'name': 'spender', 'type': 'address' }, { 'indexed': false, 'name': 'tokens', 'type': 'uint256' } ], 'name': 'Approval', 'type': 'event' }, { 'constant': true, 'inputs': [], 'name': 'contractOwner', 'outputs': [ { 'name': '', 'type': 'address' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function' }, { 'constant': true, 'inputs': [], 'name': 'totalSupply', 'outputs': [ { 'name': '', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' } ], 'name': 'balanceOf', 'outputs': [ { 'name': 'balance', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' } ], 'name': 'marginBalanceOf', 'outputs': [ { 'name': 'balance', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' }, { 'name': 'conversionRate', 'type': 'uint256' } ], 'name': 'balanceOfInEth', 'outputs': [ { 'name': 'balance', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function' }, { 'constant': false, 'inputs': [ { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transfer', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function' }, { 'constant': false, 'inputs': [ { 'name': 'spender', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'approve', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function' }, { 'constant': false, 'inputs': [ { 'name': 'from', 'type': 'address' }, { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferFrom', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function' }, { 'constant': false, 'inputs': [ { 'name': 'from', 'type': 'address' }, { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferFromOwner', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function' }, { 'constant': false, 'inputs': [ { 'name': 'from', 'type': 'address' }, { 'name': 'to', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' }, { 'name': 'destorigin', 'type': 'uint256' } ], 'name': 'transferMargin', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function' }, { 'constant': true, 'inputs': [ { 'name': 'tokenOwner', 'type': 'address' }, { 'name': 'spender', 'type': 'address' } ], 'name': 'allowance', 'outputs': [ { 'name': 'remaining', 'type': 'uint256' } ], 'payable': false, 'stateMutability': 'view', 'type': 'function' }, { 'constant': false, 'inputs': [ { 'name': 'spender', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' }, { 'name': 'data', 'type': 'bytes' } ], 'name': 'approveAndCall', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function' }, { 'constant': false, 'inputs': [ { 'name': 'tokenAddress', 'type': 'address' }, { 'name': 'tokens', 'type': 'uint256' } ], 'name': 'transferAnyERC20Token', 'outputs': [ { 'name': 'success', 'type': 'bool' } ], 'payable': false, 'stateMutability': 'nonpayable', 'type': 'function' } ]";
            opts.ContractAddress = "0x1f1149b1010f8ae8B3512Ebb962B7B3b54f3fFA4";
            opts.SigningKey = "d347a941a2999d3ea3e79f37bafcc37141a832187804614cba5ac5572456e012";
            opts.GasAmount = 400000;
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
        public async Task RetrieveUserAccountMarginBalance()
        {
            string user = "0x2f7e50c572b51c2352636ca0be931ce5b26b95e4";
            ContractCall contractCall = new ContractCall(opts);
            var result = await contractCall.GetUserMarginBalanceAsync(user);
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
            var result = await contractCall.SendTokensAsync(RopstenAccount.Mark, 1.23, opts.GasAmount);            
        }

        [Fact]
        public async Task SendSingleTokenFromUserToUser()
        {
            ContractCall contractCall = new ContractCall(opts);
            var result = await contractCall.SendTokensFromAsync(RopstenAccount.Mark, RopstenAccount.Chris, .25, opts.GasAmount);
        }

        [Fact]
        public async Task SendOneHundredTokensFromUserBalanceToMargin()
        {
            ContractCall contractCall = new ContractCall(opts);            
            var result = await contractCall.TransferMargin(RopstenAccount.Chris, RopstenAccount.Chris, 100, (int)MarginMove.BalanceToMargin, opts.GasAmount);
            var balance = await contractCall.GetUserMarginBalanceAsync(RopstenAccount.Chris);
            var swayBal = await contractCall.GetUserBalanceAsync(RopstenAccount.Chris);
            Assert.True(balance == 100);
            Assert.True(swayBal == 900);
        }

        [Fact]
        public async Task SendOneHundredTokensFromUserMarginToBalance()
        {
            ContractCall contractCall = new ContractCall(opts);            
            var result = await contractCall.TransferMargin(RopstenAccount.Chris, RopstenAccount.Chris, 100, (int)MarginMove.MarginToBalance, opts.GasAmount);
            var swayBal = await contractCall.GetUserBalanceAsync(RopstenAccount.Chris);
            var balance = await contractCall.GetUserMarginBalanceAsync(RopstenAccount.Chris);
            Assert.True(swayBal == 1000);
            Assert.True(balance == 0);
        }

        [Fact]
        public async Task SendOneHundredTokensFromUserMarginToMargin()
        {
            ContractCall contractCall = new ContractCall(opts);
            var result = await contractCall.TransferMargin(RopstenAccount.Chris, RopstenAccount.Chris, 100, (int)MarginMove.BalanceToMargin, opts.GasAmount);
            var result2 = await contractCall.TransferMargin(RopstenAccount.Chris, RopstenAccount.Mark, 50, (int)MarginMove.MarginToMargin, opts.GasAmount);
            var balance = await contractCall.GetUserMarginBalanceAsync(RopstenAccount.Chris);
            var balance2 = await contractCall.GetUserMarginBalanceAsync(RopstenAccount.Mark);
            Assert.True(balance == 50);
            Assert.True(balance2 == 50);
        }

        [Fact]
        public async Task SendFiftyTokensFromUserBackToBalance()
        {
            ContractCall contractCall = new ContractCall(opts);
            var result = await contractCall.TransferMargin(RopstenAccount.Chris, RopstenAccount.Chris, 50, (int)MarginMove.MarginToBalance, opts.GasAmount);
            var result2 = await contractCall.TransferMargin(RopstenAccount.Mark, RopstenAccount.Chris, 50, (int)MarginMove.MarginToBalance, opts.GasAmount);
            var balance = await contractCall.GetUserBalanceAsync(RopstenAccount.Chris);
            Assert.True(balance == 1000);
        }
    }

}
