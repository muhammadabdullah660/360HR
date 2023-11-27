using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    class InterviewFeedbackDL
    {
        private string connectionString;
        public List<InterviewFeedbackBL> InterviewFeedbacklist = new List<InterviewFeedbackBL>();

        public InterviewFeedbackDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadInterviewFeedbackData();
        }

        public List<InterviewFeedbackBL> GetAllInterviewFeedback()
        {
            return InterviewFeedbacklist;
        }
        public int InsertInterviewFeedback(InterviewFeedbackBL Job, out int InterviewFeedbackId, out string error)
        {
            InterviewFeedbackId = 0;
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO InterviewFeedback (InterviewID,FeedbackText,FeedbackDate) OUTPUT INSERTED.FeedbackID VALUES (@InterviewID,@FeedbackText,@FeedbackDate)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@InterviewID", Job.Jid);
                command.Parameters.AddWithValue("@FeedbackText", Job.Message);
                command.Parameters.AddWithValue("@FeedbackDate", Job.SentDate);


                try
                {
                    // Execute the SQL command and retrieve the newly created address ID
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out InterviewFeedbackId))
                    {
                        return InterviewFeedbackId;
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
                        command2.Parameters.AddWithValue("@FunctionName", "InsertInterviewFeedback");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return -1;
                }
            }
            return -1;
        }

        private void LoadInterviewFeedbackData()
        {
            InterviewFeedbacklist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from InterviewFeedback where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    InterviewFeedbackBL JobApplicant = new InterviewFeedbackBL();
                    JobApplicant.Id = Convert.ToInt32(reader["FeedbackID"]);
                    JobApplicant.Jid = Convert.ToInt32(reader["InterviewID"]);
                    JobApplicant.Message = Convert.ToString(reader["FeedbackText"]);
                    JobApplicant.SentDate = Convert.ToDateTime(reader["FeedbackDate"]);
                    InterviewFeedbacklist.Add(JobApplicant);
                }
                reader.Close();
            }
        }

       
        public InterviewFeedbackBL GetInterviewFeedbackById(int jid)
        {
            foreach (InterviewFeedbackBL jo in InterviewFeedbacklist)
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
