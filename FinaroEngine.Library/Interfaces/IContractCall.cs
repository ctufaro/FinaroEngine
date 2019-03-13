using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinaroEngine.Library
{
    public interface IContractCall
    {
        Task<string> SendTokensAsync(string recipient, double amount, int gas);
        Task<string> SendTokensFromAsync(string sender, string recipient, double amount, int gas);
        
    }
}
