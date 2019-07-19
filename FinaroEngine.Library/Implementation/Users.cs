using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace FinaroEngine.Library
{
    public class Users : OptInit<Options>
    {
        private Options opts;
        public Users(Options opts) : base(opts)
        {
            this.opts = opts;
        }

        public async Task<User> SignUpUser(string email, string username, string password, string mobile, string publickey, string privatekey)
        {
            string hashedPassed = Utility.GetStringSha256Hash(password);
            List<SqlParameter> prms = new List<SqlParameter> {
                new SqlParameter("@EMAIL",email),
                new SqlParameter("@USERNAME", username),
                new SqlParameter("@PASSWORD", hashedPassed),
                new SqlParameter("@MOBILE", mobile),
                new SqlParameter("@PUBLICKEY", publickey),                
                new SqlParameter("@PRIVATEKEY", privatekey)
            };
            var dt = await DBUtility.GetDataTableAsync(opts.ConnectionString, "spInsertUser", prms);
            var id = Convert.ToInt32(dt.Rows[0][0]);
            return await GetUserInformation(id);
        }

        public async Task<User> LoginUser(string username, string password)
        {
            string hashedPassed = Utility.GetStringSha256Hash(password);
            List<SqlParameter> prms = new List<SqlParameter> {
                new SqlParameter("@USERNAME", username),
                new SqlParameter("@PASSWORD", hashedPassed)
            };

            var dt = await DBUtility.GetDataTableAsync(opts.ConnectionString, "spLoginUser", prms);

            // USER DOES NOT EXIST, RETURN NULL
            if (dt.Rows.Count == 0) return null;

            // USER DOES EXIST, RETURN THEIR INFORMATION BASED ON THEIR ID
            int userId = Convert.ToInt32(dt.Rows[0]["Id"]);
            return await GetUserInformation(userId);
        }

        public async Task<bool> EmailExists(string email)
        {
            List<SqlParameter> prms = new List<SqlParameter> {
                new SqlParameter("@EMAIL", email)
            };

            var dt = await DBUtility.GetDataTableAsync(opts.ConnectionString, "spEmailExists", prms);
            return dt.Rows.Count > 0;
        }
        
        public async Task<User> GetUserInformation(int userId)
        {
            List<SqlParameter> prms = new List<SqlParameter> { new SqlParameter("@USERID", userId) };
            DataTable dt = await DBUtility.GetDataTableAsync(opts.ConnectionString, "spSelectUser", prms);
            DataRow dr = dt.Rows[0];

            return new User
            {
                Id = Convert.ToInt32(dr["Id"]),
                Username = dr["Username"].ToString(),
                Email = dr["Email"].ToString(),
                Balance = Convert.ToDecimal(dr["Balance"]),
                Avatar = dr["Avatar"].ToString()
            };
        }
    }
}
