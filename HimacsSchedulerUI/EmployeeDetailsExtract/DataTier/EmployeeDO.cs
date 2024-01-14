using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeeDetailsExtract.DataTier
{
    public class EmployeeDO
    {
        public static DataTable GetEmployeeData()
        {
            DataTable fee = new DataTable();
            using (var con = new SqlConnection("Server=tcp:himacsserver.database.windows.net,1433;Initial Catalog=himacsdb;Persist Security Info=False;User ID=himacsserver;Password=Testdb123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            using (var cmd = new SqlCommand("GetEmployeeDetail", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(fee);
            }
            return fee;
        }
    }
}
