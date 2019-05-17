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
                LastTradePrice = dr.IsNull("LastTradePrice") ? (decimal?)null : Convert.ToDecimal(dr["LastTradePrice"]),
                LastTradeTime = dr.IsNull("LastTradeTime") ? (DateTime?)null : Convert.ToDateTime(dr["LastTradeTime"]),
                MarketPrice = dr.IsNull("MarketPrice") ? (decimal?)null : Convert.ToDecimal(dr["MarketPrice"]),
                Volume = dr.IsNull("Volume") ? (int?)null : Convert.ToInt32(dr["Volume"]),
                ChangeInPrice = dr.IsNull("ChangeInPrice") ? (decimal?)null : Convert.ToDecimal(dr["ChangeInPrice"]),
                CurrentAsk = dr.IsNull("CurrentAsk") ? (decimal?)null : Convert.ToDecimal(dr["CurrentAsk"]),
                CurrentBid = dr.IsNull("CurrentBid") ? (decimal?)null : Convert.ToDecimal(dr["CurrentBid"])
            };            
        }

        public MarketData UpdateMarketData(MarketData marketData)
        {
            List<SqlParameter> prms = new List<SqlParameter>
            {
                new SqlParameter("@ENTITYID", marketData.EntityId),
                new SqlParameter("@VOLUME", marketData.Volume),
                new SqlParameter("@LASTTRADETIME", marketData.LastTradeTime),
                new SqlParameter("@LASTTRADEPRICE", marketData.LastTradePrice),
                new SqlParameter("@MARKETPRICE", marketData.MarketPrice)
            };

            DataTable marketTable = DBUtility.GetDataTable(opts.ConnectionString, "spInsertMarketData", prms);
            DataRow dr = marketTable.Rows[0];
            return new MarketData
            {
                EntityId = entityId,
                LastTradePrice = dr.IsNull("LastTradePrice") ? (decimal?)null : Convert.ToDecimal(dr["LastTradePrice"]),
                LastTradeTime = dr.IsNull("LastTradeTime") ? (DateTime?)null : Convert.ToDateTime(dr["LastTradeTime"]),
                MarketPrice = dr.IsNull("MarketPrice") ? (decimal?)null : Convert.ToDecimal(dr["MarketPrice"]),
                Volume = dr.IsNull("Volume") ? (int?)null : Convert.ToInt32(dr["Volume"]),
                ChangeInPrice = dr.IsNull("ChangeInPrice") ? (decimal?)null : Convert.ToDecimal(dr["ChangeInPrice"]),
                CurrentAsk = dr.IsNull("CurrentAsk") ? (decimal?)null : Convert.ToDecimal(dr["CurrentAsk"]),
                CurrentBid = dr.IsNull("CurrentBid") ? (decimal?)null : Convert.ToDecimal(dr["CurrentBid"])
            };
        }
        
    }
}