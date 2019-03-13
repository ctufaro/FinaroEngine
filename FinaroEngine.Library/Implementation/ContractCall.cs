using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace FinaroEngine.Library
{
    public class ContractCall : OptInit<Options>, IContractCall
    {
        private Options opts;

        public ContractCall(Options opts)
            : base(opts)
        {
            this.opts = opts;
        }

        public async Task<double> GetUserBalanceAsync(string address)
        {
            Web3 web3 = new Web3(this.opts.URL);
            Contract swayContract = web3.Eth.GetContract(this.opts.ABI, this.opts.Address);
            int balance = (int)await swayContract.GetFunction("balanceOf").CallAsync<BigInteger>(address);
            return (double)balance / 100;
        }

        public async Task<int> GetTotalSupplyAsync()
        {
            Web3 web3 = new Web3(this.opts.URL);
            Contract swayContract = web3.Eth.GetContract(this.opts.ABI, this.opts.Address);
            int balance = (int)await swayContract.GetFunction("totalSupply").CallAsync<BigInteger>();
            return balance / 100;
        }

        public async Task<string> GetContractAddressAsync()
        {
            Web3 web3 = new Web3(this.opts.URL);
            Contract swayContract = web3.Eth.GetContract(this.opts.ABI, this.opts.Address);
            return await swayContract.GetFunction("contractOwner").CallAsync<string>();
        }

        public async Task<string> SendTokensAsync(string recipient, double amount, int gas = 400000)
        {
            var account = new Account(opts.SigningKey);
            var web3 = new Web3(account, this.opts.URL);
            Contract swayContract = web3.Eth.GetContract(this.opts.ABI, this.opts.Address);
            HexBigInteger gasAmt = new HexBigInteger(new BigInteger(gas));
            HexBigInteger value = new HexBigInteger(new BigInteger(0));
            return await swayContract.GetFunction("transfer").SendTransactionAsync(account.Address, gasAmt, value, recipient, amount * 100);
        }

        public async Task<string> SendTokensFromAsync(string sender, string recipient, double amount, int gas = 400000)
        {
            var account = new Account(opts.SigningKey);
            var web3 = new Web3(account, this.opts.URL);
            Contract swayContract = web3.Eth.GetContract(this.opts.ABI, this.opts.Address);
            HexBigInteger gasAmt = new HexBigInteger(new BigInteger(gas));
            HexBigInteger value = new HexBigInteger(new BigInteger(0));
            return await swayContract.GetFunction("transferFromOwner").SendTransactionAsync(account.Address, gasAmt, value, sender, recipient, amount * 100);
        }
    }
}
