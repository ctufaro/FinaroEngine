using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace FinaroEngine.Library.Domain
{
    public class Register
    {
        private string _connection;
        public int Id { get; set; }
        public string RegisterCode { get; set; }
        public int Position { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RegisterId { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string Zip { get; set; }
        public string EmailPrimary { get; set; }
        public string EmailSecondary { get; set; }
        public string  SchoolName { get; set; }
        public string ActivitySheetSubject { get; set; }
        public int SchoolDay { get; set; }
        public DateTime CreatedOn { get; set; }

        public Register(string connection)
        {
            this._connection = connection;
        }

        public async Task<bool> InsertRegisterAsync(Register register)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@REGISTERCODE", register.RegisterCode));
            parameters.Add(new SqlParameter("@POSITION", register.Position));
            parameters.Add(new SqlParameter("@FIRSTNAME", register.FirstName));
            parameters.Add(new SqlParameter("@LASTNAME", register.LastName));
            parameters.Add(new SqlParameter("@REGISTERID", register.RegisterId));
            parameters.Add(new SqlParameter("@ADDRESS", register.Address));
            parameters.Add(new SqlParameter("@STREET", register.Street));
            parameters.Add(new SqlParameter("@TOWN", register.Town));
            parameters.Add(new SqlParameter("@ZIP", register.Zip));
            parameters.Add(new SqlParameter("@EMAILPRIMARY", register.EmailPrimary));
            parameters.Add(new SqlParameter("@EMAILSECONDARY", register.EmailSecondary));
            parameters.Add(new SqlParameter("@SCHOOLNAME", register.SchoolName));
            parameters.Add(new SqlParameter("@ACTIVITYSHEETSUBJECT", register.ActivitySheetSubject));
            parameters.Add(new SqlParameter("@SCHOOLDAY", register.SchoolDay));
            await DBUtility.ExecuteQueryAsync(this._connection, "spInsertRegister", parameters);
            return true;
        }
    }
}
