using System;
using System.Data;
using System.Data.SqlClient;

namespace HIMACSScheduleDataTier
{
    public class HIMACSScheduleEngineDO
    {
        private static string connectionString = "Server=tcp:himacsserver.database.windows.net,1433;Initial Catalog=himacsdb;Persist Security Info=False;User ID=himacsserver;Password=Testdb123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    
        public static DataTable GetActiveJobSchedule()
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetActiveJobSchedule"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }
        public static DataTable GetJobSchedule(int jobid)
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetJobSchedule"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jobid", jobid);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }
        
        public static DataTable GetRunningJobSchedule()
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetRunningJobSchedule"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }

        public static void UpdateRunStatus(int jobid, string status)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateJobScheduleStatus"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jobid", jobid);
                    cmd.Parameters.AddWithValue("@status", status);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public static void UpdatePipelineConfig(int jobId, string tenantID, string applicationId, string authenticationKey, string subscriptionId, string resourceGroup, string dataFactoryName, string pipelineName)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdatePipelineConfig"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jobid", jobId);
                    cmd.Parameters.AddWithValue("@tenantID", tenantID);
                    cmd.Parameters.AddWithValue("@applicationId", applicationId);
                    cmd.Parameters.AddWithValue("@authenticationKey", authenticationKey);
                    cmd.Parameters.AddWithValue("@subscriptionId", subscriptionId);
                    cmd.Parameters.AddWithValue("@resourceGroup", resourceGroup);
                    cmd.Parameters.AddWithValue("@dataFactoryName", dataFactoryName);
                    cmd.Parameters.AddWithValue("@pipelineName", pipelineName);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public static void UpdateRunStatusAndNextRunOnCompletion(int jobid, string runStatus, DateTime nextRunDate)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateRunStatusAndNextRunOnCompletion"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jobid", jobid);
                    cmd.Parameters.AddWithValue("@runStatus", runStatus);
                    cmd.Parameters.AddWithValue("@nextRunDate", nextRunDate);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public static DataTable GetJobSpecificConfigByIDAndKey(int id, string key)
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetJobSpecificConfigByIDAndKey"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jobid", id);
                    cmd.Parameters.AddWithValue("@key", key);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }

        public static void UpdateJobSpecificConfigByIDAndKey(int jobId, string key, string value)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateJobSpecificConfigByIDAndKey"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jobid", jobId);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.Parameters.AddWithValue("@value", value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public static DataTable GetPipelineConfig(int jobid)
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetPipelineConfig"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@JobId", jobid);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }

        public static DataTable GetJobs()
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetJobs"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }

        public static DataTable Get_AllJobHistory()
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Get_AllJobHistory"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }

        public static DataTable Get_JobHistory(int jobid)
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Get_JobHistory"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jobid", jobid);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }

        public static DataTable JobNameById(int jobid)
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("JobNameById"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jobid", jobid);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }

        public static void UpdateJobScheduleNextRun(int jobid, DateTime nextrundate)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateJobScheduleNextRun"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jobid", jobid);
                    cmd.Parameters.AddWithValue("@nextrundate", nextrundate);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        //public static void UpdateJobScheduleNextRun(int jobid, DateTime nextrundate)
        //{
        //    using (var con = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("UpdateJobScheduleNextRun"))
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@jobid", jobid);
        //            cmd.Parameters.AddWithValue("@nextrundate", nextrundate);
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //        }
        //    }
        //}

        public static DataTable GetJobScheduleById(int jobid)
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetJobScheduleById"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jobid", jobid);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }

        public static DataTable GetJobSpecificConfig(int jobid)
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetJobSpecificConfig"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@JobId", jobid);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }
    }
}
