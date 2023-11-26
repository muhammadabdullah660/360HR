using ApplicantTrackingPlatform.BL;
using ApplicantTrackingPlatform.Forms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
     class JobDL
    {
        private string connectionString;
        public List<JobBL> Joblist = new List<JobBL>();

        public JobDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadJobData();
        }

        public List<JobBL> GetAllJob()
        {
            return Joblist;
        }
        public int InsertJob(JobBL Job, out int JobId, out string error)
        {
            JobId = 0;
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO JOB (CompanyID,Title,Description,Managerid) OUTPUT INSERTED.ID VALUES (@CompanyID,@Title,@Description,@Managerid)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@CompanyID", Job.Companyid);
                command.Parameters.AddWithValue("@Title", Job.Title);
                command.Parameters.AddWithValue("@Description", Job.Description);
                command.Parameters.AddWithValue("@Managerid", Job.Managerid);

                try
                {

                    // Execute the SQL command and retrieve the newly created address ID
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out JobId))
                    {
                        return JobId;
                    }
                }
                catch (Exception ex)
                {
                    // Handle the case where the insertion failed or the addressId couldn't be retrieved.
                    error = "Insertion failed couldn't be retrieved." + ex.Message;
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                        //  command.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@FunctionName", "InsertJob");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return -1;
                }
                return -1;
            }
        }

        private void LoadJobData()
        {
            Joblist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from Job where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    JobBL Job = new JobBL();
                    Job.Id = Convert.ToInt32(reader["ID"]);
                    Job.Managerid = Convert.ToInt32(reader["Managerid"]);
                    Job.Companyid = Convert.ToInt32(reader["CompanyID"]);
                    Job.Title = reader["Title"].ToString();
                    Job.Description = reader["Description"].ToString();
                    Joblist.Add(Job);
                }
                reader.Close();
            }
        }

        public bool DeleteJob(int jobid, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE Job SET Active = 0 WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", jobid);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // The update was successful
                        return true;
                    }
                    else
                    {
                        // No rows were updated, meaning the job ID might not exist
                        error = "No rows were updated. Job ID may not exist.";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occurred during the update
                    error = "Error updating the database: " + ex.Message;
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                        //  command.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@FunctionName", "DeleteJob");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }
           
            }
        }

        public bool UpdateJob(int jobid,string title,string des,out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE Job SET updatedAT = GetDate(),Title=@Title,Description=@Description WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", jobid);
               // command.Parameters.AddWithValue("@CompanyID", jb.Companyid);
                command.Parameters.AddWithValue("@Title",title);
                command.Parameters.AddWithValue("@Description", des);
                //command.Parameters.AddWithValue("@IManagerid", jb.Managerid);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // The update was successful
                        return true;
                    }
                    else
                    {
                        // No rows were updated, meaning the job ID might not exist
                        error = "No rows were updated. Job ID may not exist.";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occurred during the update
                    error = "Error updating the database: " + ex.Message;
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                        //  command.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@FunctionName", "UpdateJob");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        public JobBL GetJobById(int jid)
        {
            foreach(JobBL jo in Joblist)
            {
                if(jo.Id==jid)
                {
                    return jo;
                }
            }
            return null; 

        }



    }
}
