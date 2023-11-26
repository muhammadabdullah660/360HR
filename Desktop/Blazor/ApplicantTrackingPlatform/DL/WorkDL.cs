using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    class WorkDL
    {
        private string connectionString;
        public List<WorkBL> Worklist = new List<WorkBL>();

        public WorkDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadWorkData();
        }

        public List<WorkBL> GetAllWork()
        {
            return Worklist;
        }
        public int InsertWork(WorkBL Work, out int WorkId, out string error)
        {
            WorkId = 0;
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO WorkExperience (CompanyID,Duration,StartDate,Role,ProfileID) OUTPUT INSERTED.ID VALUES (@CompanyID,@Duration,@StartDate,@Role,@ProfileID)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@CompanyID", Work.Companyid);
                command.Parameters.AddWithValue("@Duration", Work.Duration);
                command.Parameters.AddWithValue("@StartDate", Work.Start);
                command.Parameters.AddWithValue("@Role", Work.Role);
                command.Parameters.AddWithValue("@ProfileID", Work.Profileid);
                try
                {

                    // Execute the SQL command and retrieve the newly created address ID
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out WorkId))
                    {
                        return WorkId;
                    }
                }
                catch (Exception ex)
                {
                    // Handle the case where the insertion failed or the addressId couldn't be retrieved.
                    error = "Insertion failed couldn't be retrieved."+ex.Message;
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                        //  command.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@FunctionName", "InsertWork");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return -1;
                }
            }
            return -1;
        }

        private void LoadWorkData()
        {
            Worklist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from WorkExperience where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    WorkBL Work = new WorkBL();
                    Work.Id = Convert.ToInt32(reader["ID"]);
                    Work.Companyid = Convert.ToInt32(reader["CompanyID"]);
                    Work.Start = Convert.ToDateTime(reader["StartDate"]);
                    Work.Duration = (float)Convert.ToDecimal(reader["Duration"]);
                    Work.Profileid = Convert.ToInt32(reader["ProfileID"]);
                    Work.Role = reader["Role"].ToString();
                    Worklist.Add(Work);
                }
                reader.Close();
            }
        }

        public bool DeleteWork(int Workid, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE WorkExperience SET Active = 0 WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", Workid);

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
                        error = "No rows were updated.work ID may not exist.";
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
                        command2.Parameters.AddWithValue("@FunctionName", "DeleteWork");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        public bool UpdateWork(int Workid, string role, DateTime start, float duration, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE WorkExperience SET updatedAT = GetDate(),Duration=@Duration,StartDate=@StartDate,Role=@Role WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", Workid);
                command.Parameters.AddWithValue("@StartDate", start);
                command.Parameters.AddWithValue("@Duration", duration);
                command.Parameters.AddWithValue("@Role", role);
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
                        error = "No rows were updated. Work ID may not exist.";
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
                        command2.Parameters.AddWithValue("@FunctionName", "UpdateWork");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        public WorkBL GetWorkById(int jid)
        {
            foreach (WorkBL jo in Worklist)
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
