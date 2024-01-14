using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IMAPFeeSolution.DataTier
{
    public class FeeSolutionDO
    {
        public static void LoadIMAPFeeSolutionData(string xml)
        {
            using (var con = new SqlConnection("Server=tcp:himacsserver.database.windows.net,1433;Initial Catalog=himacsdb;Persist Security Info=False;User ID=himacsserver;Password=Testdb123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                using (SqlCommand cmd = new SqlCommand("InsertFeeSolutionXML"))
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
