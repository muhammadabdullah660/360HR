using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    class ProjectApplicantDL
    {
        private string connectionString;
        public List<ProjectApplicantBL> ProjectApplicantlist = new List<ProjectApplicantBL>();

        public ProjectApplicantDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadProjectApplicantData();
        }

        public List<ProjectApplicantBL> GetAllProjectApplicant()
        {
            return ProjectApplicantlist;
        }
        public int InsertProjectApplicant(ProjectApplicantBL Job, out int ProjectApplicantId, out string error)
        {
            ProjectApplicantId = 0;
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO ProjectApplicants (ProjectID,ProfileID,StatusID,Rate) OUTPUT INSERTED.ID VALUES (@ProjectID,@ProfileID,@StatusID,@Rate)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@ProjectID", Job.Projectid);
                command.Parameters.AddWithValue("@ProfileID", Job.Profileid);
                command.Parameters.AddWithValue("@StatusID", Job.Statusid);
                command.Parameters.AddWithValue("@Rate", Job.Rate);

                try
                {
                    // Execute the SQL command and retrieve the newly created address ID
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out ProjectApplicantId))
                    {
                        return ProjectApplicantId;
                    }
                }
                catch(Exception ex)
                {
                    // Handle the case where the insertion failed or the addressId couldn't be retrieved.
                    error = "Insertion failed couldn't be retrieved." + ex.Message;
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                        //  command.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@FunctionName", "InsertProjectApplicant");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return -1;
                }
            }
            return -1;
        }

        private void LoadProjectApplicantData()
        {
            ProjectApplicantlist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from ProjectApplicants where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProjectApplicantBL ProjectApplicant = new ProjectApplicantBL();
                    ProjectApplicant.Id = Convert.ToInt32(reader["ID"]);
                    ProjectApplicant.Projectid = Convert.ToInt32(reader["ProjectID"]);
                    ProjectApplicant.Profileid = Convert.ToInt32(reader["ProfileID"]);
                    ProjectApplicant.Statusid = Convert.ToInt32(reader["StatusID"]);
                    ProjectApplicant.Rate = (float)Convert.ToDouble(reader["Rate"]);
                    ProjectApplicantlist.Add(ProjectApplicant);
                }
                reader.Close();
            }
        }

        public bool DeleteProjectApplicant(int ProjectApplicantid, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE ProjectApplicants SET Active = 0 WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", ProjectApplicantid);

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
                        command2.Parameters.AddWithValue("@FunctionName", "DeleteProjectApplicant");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        public bool UpdateProjectApplicant(int ProjectApplicantid, int statusid, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE ProjectApplicants SET updatedAT = GetDate(),StatusID=@StatusID WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", ProjectApplicantid);
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
                        command2.Parameters.AddWithValue("@FunctionName", "UpdateProjectApplicant");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        public ProjectApplicantBL GetProjectApplicantById(int jid)
        {
            foreach (ProjectApplicantBL jo in ProjectApplicantlist)
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
