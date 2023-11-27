using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    class JobApplicantDL
    {

        private string connectionString;
        public List<JobApplicantBL> JobApplicantlist = new List<JobApplicantBL>();

        public JobApplicantDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadJobApplicantData();
        }

        public List<JobApplicantBL> GetAllJobApplicant()
        {
            return JobApplicantlist;
        }
        public int InsertJobApplicant(JobApplicantBL Job, out int JobApplicantId, out string error)
        {
            JobApplicantId = 0;
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO JOBApplication (JobID,ProfileID,StatusID) OUTPUT INSERTED.ID VALUES (@JobID,@ProfileID,@StatusID)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@JobID", Job.Jobid);
                command.Parameters.AddWithValue("@ProfileID", Job.Profileid);
                command.Parameters.AddWithValue("@StatusID", Job.Statusid);


                try
                {
                    // Execute the SQL command and retrieve the newly created address ID
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out JobApplicantId))
                    {
                        return JobApplicantId;
                    }
                }
                catch(Exception ex)
                {
                    // Handle the case where the insertion failed or the addressId couldn't be retrieved.
                    error = "Insertion failed couldn't be retrieved."+ex.Message;
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                        //  command.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@FunctionName", "InsertJobApplicant");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return -1;
                }
            }
            return -1;
        }

        private void LoadJobApplicantData()
        {
            JobApplicantlist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from JobApplication where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    JobApplicantBL JobApplicant = new JobApplicantBL();
                    JobApplicant.Id = Convert.ToInt32(reader["ID"]);
                    JobApplicant.Jobid = Convert.ToInt32(reader["JobID"]);
                    JobApplicant.Profileid = Convert.ToInt32(reader["ProfileID"]);
                    JobApplicant.Statusid = Convert.ToInt32(reader["StatusID"]);
                    JobApplicant.DateofApply = Convert.ToDateTime(reader["DateOfApply"]);
                    JobApplicantlist.Add(JobApplicant);
                }
                reader.Close();
            }
        }

        public bool DeleteJobApplicant(int jobApplicantid, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE JobApplication SET Active = 0 WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", jobApplicantid);

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
                        error = "No rows were updated. ID may not exist.";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occurred during the update
                    error = "Error Deleting the database: " + ex.Message;
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                        //  command.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@FunctionName", "DeleteJobApplicant");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        public bool UpdateJobApplicant(int jobApplicantid, int statusid, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE JobApplication SET updatedAT = GetDate(),StatusID=@StatusID WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", jobApplicantid);
                command.Parameters.AddWithValue("@StatusID", statusid);
                
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
                        error = "No rows were updated.ID may not exist.";
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
                        command2.Parameters.AddWithValue("@FunctionName", "UpdateJobApplicant");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        public JobApplicantBL GetJobApplicantById(int jid)
        {
            foreach (JobApplicantBL jo in JobApplicantlist)
            {
                if (jo.Id == jid)
                {
                    return jo;
                }
            }
            return null;

        }

    }
}
