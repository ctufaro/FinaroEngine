using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FinaroEngine.Library
{
    public class DBUtility
    {
        public static DataTable GetDataTable(string connectionstring, string storedProcedure, List<SqlParameter> prm)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if(prm!=null)
                        cmd.Parameters.AddRange(prm.ToArray());
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        public static int ExecuteQuery(string connectionstring, string storedProcedure, List<SqlParameter> prm)
        {
            using (var con = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(storedProcedure, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(prm.ToArray());
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
