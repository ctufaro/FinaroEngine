﻿using System;
using System.Collections.Generic;
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

        public async Task CreateUser(string email, string username, string password, string mobile, string publickey, string privatekey)
        {
            List<SqlParameter> prms = new List<SqlParameter> {
                new SqlParameter("@EMAIL",email),
                new SqlParameter("@USERNAME", username),
                new SqlParameter("@PASSWORD", password),
                new SqlParameter("@MOBILE", mobile),
                new SqlParameter("@PUBLICKEY", publickey),                
                new SqlParameter("@PRIVATEKEY", privatekey)
            };
            await DBUtility.ExecuteQueryAsync(opts.ConnectionString, "spInsertUser", prms);            
        }

        public async Task<bool> LoginUser(string username, string password)
        {
            List<SqlParameter> prms = new List<SqlParameter> {
                new SqlParameter("@USERNAME", username),
                new SqlParameter("@PASSWORD", password)
            };

            var dt = await DBUtility.GetDataTableAsync(opts.ConnectionString, "spSelectUser", prms);
            return dt.Rows.Count > 0;
        }
    }
}
