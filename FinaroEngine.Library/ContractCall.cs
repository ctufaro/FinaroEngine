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
    public class ContractCall
    {
        public string URL { get; set; }
        public string Address { get; set; }
        public string ABI { get; set; }

        public ContractCall(string url, string address, string abi)
        {
            this.URL = url;
            this.Address = address;
            this.ABI = abi;
        }

        public async Task<int> GetUserBalanceAsync(string address)
        {
            Web3 web3 = new Web3(this.URL);
            Contract swayContract = web3.Eth.GetContract(this.ABI, this.Address);
            int balance = (int)await swayContract.GetFunction("balanceOf").CallAsync<BigInteger>(address);
            return balance/100;
        }

        public async Task<int> GetTotalSupplyAsync()
        {
            Web3 web3 = new Web3(this.URL);
            Contract swayContract = web3.Eth.GetContract(this.ABI, this.Address);
            int balance = (int)await swayContract.GetFunction("totalSupply").CallAsync<BigInteger>();
            return balance / 100;
        }

        public async Task<string> GetContractAddress()
        {
            Web3 web3 = new Web3(this.URL);
            Contract swayContract = web3.Eth.GetContract(this.ABI, this.Address);
            return await swayContract.GetFunction("contractOwner").CallAsync<string>();            
        }

        public async Task<string> SendTokensAsync(string privateKey, string recipient, double amount, int gas = 400000)
        {
            var account = new Account(privateKey);
            var web3 = new Web3(account, this.URL);
            Contract swayContract = web3.Eth.GetContract(this.ABI, this.Address);            
            HexBigInteger gasAmt = new HexBigInteger(new BigInteger(gas));
            HexBigInteger value = new HexBigInteger(new BigInteger(0));
            return await swayContract.GetFunction("transfer").SendTransactionAsync(account.Address, gasAmt, value, recipient, amount*100);            
        }
    }
}
