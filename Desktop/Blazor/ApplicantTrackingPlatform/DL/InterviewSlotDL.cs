using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    class InterviewSlotDL
    {
        private string connectionString;
        public List<InterviewSlotBL> InterviewSlotlist = new List<InterviewSlotBL>();

        public InterviewSlotDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadInterviewSlotData();
        }

        public List<InterviewSlotBL> GetAllInterviewSlot()
        {
            return InterviewSlotlist;
        }
        public bool InsertInterviewSlot(InterviewSlotBL Skill, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Interviewslot (interviewid,slotdatetime) VALUES (@interviewid,@slotdatetime)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@interviewid", Skill.Jobid);
                command.Parameters.AddWithValue("@slotdatetime", Skill.Slot);
                try
                {
                    // Execute the SQL command and retrieve the newly created address ID
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // The update was successful
                        return true;
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
                        command2.Parameters.AddWithValue("@FunctionName", "InsertInterviewSlot");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }
            }
            return false;
        }
        private void LoadInterviewSlotData()
        {
            InterviewSlotlist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from Interviewslot", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    InterviewSlotBL Course = new InterviewSlotBL();
                    Course.Jobid = Convert.ToInt32(reader["interviewid"]);
                    Course.Slot = reader["slotdatetime"].ToString();
                    InterviewSlotlist.Add(Course);
                }
                reader.Close();
            }
        }
        public bool Deleteslot(int id, string name, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "Delete from InterviewFeedback WHERE interviewid = @interviewid AND slotdatetime=@slotdatetime";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@interviewid", id);
                command.Parameters.AddWithValue("@slotdatetime", name);


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
                        error = "No rows were delete. ID may not exist.";
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
                        command2.Parameters.AddWithValue("@FunctionName", "DeleteInterviewSLot");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        public int GetInterviewSlotId(string name)
        {
            foreach (InterviewSlotBL jo in InterviewSlotlist)
            {
                if (jo.Slot == name)
                {
                    return jo.Jobid;
                }
            }
            return -1;

        }


    }
}
