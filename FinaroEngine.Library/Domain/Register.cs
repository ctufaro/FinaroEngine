using System;
using System.Collections.Generic;
using System.Data;
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

        public Register()
        {

        }

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

        public async Task<List<Register>> SelectRegisterAsync()
        {
            DataTable dt = await DBUtility.GetDataTableAsync(_connection, "spSelectRegister", null);
            List<Register> registers = new List<Register>();
            foreach(DataRow dr in dt.Rows)
            {
                Register r = new Register()
                {
                    ActivitySheetSubject = dr["ActivitySheetSubject"].ToString(),
                    Address = dr["Address"].ToString(),
                    CreatedOn = Convert.ToDateTime(dr["CreatedOn"]),
                    EmailPrimary = dr["EmailPrimary"].ToString(),
                    EmailSecondary = dr["EmailSecondary"].ToString(),
                    FirstName = dr["FirstName"].ToString(),
                    Id = Convert.ToInt32(dr["Id"]),
                    LastName = dr["LastName"].ToString(),
                    Position = Convert.ToInt32(dr["Position"]),
                    RegisterCode = dr["RegisterCode"].ToString(),
                    RegisterId = dr["RegisterId"].ToString(),
                    SchoolDay = Convert.ToInt32(dr["SchoolDay"]),
                    SchoolName = dr["SchoolName"].ToString(),
                    Street = dr["Street"].ToString(),
                    Town = dr["Town"].ToString(),
                    Zip = dr["Zip"].ToString()
                };
                registers.Add(r);
            }
            return registers;
        }
    }
}
