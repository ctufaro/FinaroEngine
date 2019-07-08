using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace FinaroEngine.Library
{
    public class PriceVols : OptInit<Options>, IPriceVol
    {
        private Options opts;

        public PriceVols(Options opts) : base(opts)
        {
            this.opts = opts;
        }

        public PriceVol GetPriceVol(string name)
        {
            List<SqlParameter> prms = new List<SqlParameter> { new SqlParameter("@NAME", name) };
            var dt = DBUtility.GetDataTable(opts.ConnectionString, "spSelectPriceVolData", prms);
            var dr = dt.Rows[0];
            PriceVol priceVols = new PriceVol();
            priceVols.Name = name;
            string priceHistory = dr["PriceDate"].ToString().Trim();
            priceHistory = (priceHistory.EndsWith(',')) ? priceHistory.Remove(priceHistory.Length - 1) : priceHistory;
            priceVols.Prices = Utility.ConvertToDecArray(dr["Price"].ToString(), true);
            priceVols.Times = priceHistory.Split(',');            
            return priceVols;
        }

        public string GetPriceVolJSON(string name)
        {
            return JsonConvert.SerializeObject(GetPriceVol(name));
        }
    }
}
