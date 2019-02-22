using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FinaroEngine.Library
{
    public class DBTeamPlayer : OptInit<Options>, ITeamPlayer
    {
        private Options opts;
        public DBTeamPlayer(Options opts)
                    : base(opts)
        {
            this.opts = opts;
        }

        public IEnumerable<TeamPlayer> GetTeamPlayers(int entityTypeId, int entityLeagueId)
        {
            List<TeamPlayer> teamPlayers = new List<TeamPlayer>();
            List<SqlParameter> prms = new List<SqlParameter>
            {
                new SqlParameter("@ENTITYTYPEID", entityTypeId),
                new SqlParameter("@ENTITYLEAGUEID", entityLeagueId)
            };
            DataTable teamPlayerTable = DBUtility.GetDataTable(opts.ConnectionString, "spSelectTeamLeagueData", prms);
            foreach (DataRow dr in teamPlayerTable.Rows)
            {
                TeamPlayer teamPlayer = new TeamPlayer();
                teamPlayer.Id = Convert.ToInt32(dr["Id"]);
                teamPlayer.Name = Convert.ToString(dr["Name"]);
                teamPlayer.CurrentBid = dr.IsNull("CurrentBid") ? (decimal?)null : Convert.ToDecimal(dr["CurrentBid"]);
                teamPlayer.CurrentAsk = dr.IsNull("CurrentAsk") ? (decimal?)null : Convert.ToDecimal(dr["CurrentAsk"]);
                teamPlayer.LastPrice = dr.IsNull("LastPrice") ? (decimal?)null : Convert.ToDecimal(dr["LastPrice"]);
                teamPlayers.Add(teamPlayer);
            }
            return teamPlayers;
        }
    }
}
