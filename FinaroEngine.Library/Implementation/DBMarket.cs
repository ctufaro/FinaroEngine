using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FinaroEngine.Library
{
    public class DBMarket : OptInit<Options>, IMarketData
    {
        private int userId;
        private int entityId;
        private Options opts;

        public DBMarket(Options opts, int userId, int entityId)
            : base(opts)
        {
            this.opts = opts;
            this.userId = userId;
            this.entityId = entityId;
        }

        public MarketData GetMarketData(int userId, int entityId)
        {            
            List<SqlParameter> prms = new List<SqlParameter> { new SqlParameter("@ENTITYID", entityId) };
            DataTable marketTable = DBUtility.GetDataTable(opts.ConnectionString, "spSelectMarketData", prms);
            DataRow dr = marketTable.Rows[0];
            return new MarketData
            {
                EntityId = entityId,
                LastTradePrice = Convert.ToDecimal(dr["LastTradePrice"]),
                LastTradeTime = Convert.ToDateTime(dr["LastTradeTime"]),
                MarketPrice = Convert.ToDecimal(dr["MarketPrice"]),
                Volume = Convert.ToInt32(dr["Volume"]),
                ChangeInPrice = Convert.ToDecimal(dr["ChangeInPrice"])
            };            
        }

        public void UpdateMarketData(MarketData marketData)
        {
            List<SqlParameter> prms = new List<SqlParameter>
            {
                new SqlParameter("@ENTITYID", marketData.EntityId),
                new SqlParameter("@VOLUME", marketData.Volume),
                new SqlParameter("@LASTTRADETIME", marketData.LastTradeTime),
                new SqlParameter("@LASTTRADEPRICE", marketData.LastTradePrice),
                new SqlParameter("@MARKETPRICE", marketData.MarketPrice)
            };

            DBUtility.ExecuteQuery(opts.ConnectionString, "spInsertMarketData", prms);
        }
        
    }
}