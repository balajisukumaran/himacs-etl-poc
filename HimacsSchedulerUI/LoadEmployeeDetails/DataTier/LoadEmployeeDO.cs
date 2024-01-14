using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LoadEmployeeDetails.DataTier
{
    public class LoadEmployeeDO
    {
        public static void LoadEmployeeData(string xml)
        {
            using (var con = new SqlConnection("Server=tcp:himacsserver.database.windows.net,1433;Initial Catalog=himacsdb;Persist Security Info=False;User ID=himacsserver;Password=Testdb123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                using (SqlCommand cmd = new SqlCommand("InsertEmployeeXML"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@xml", xml);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
