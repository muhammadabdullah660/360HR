using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    class AchivementDL
    {
        private string connectionString;
        public List<AchivementBL> Achivementlist = new List<AchivementBL>();

        public AchivementDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadAchivementData();
        }

        public List<AchivementBL> GetAllAchivement()
        {
            return Achivementlist;
        }
        public bool InsertCourse(AchivementBL Achivement,  out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Achievement (ProfileID,Description) VALUES (@ProfileID,@Description)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@Description", Achivement.Des);
                command.Parameters.AddWithValue("ProfileID", Achivement.Proid);
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
                catch(Exception ex)
                {
                    // Handle the case where the insertion failed or the addressId couldn't be retrieved.
                    error = "Insertion failed couldn't be retrieved."+ex.Message;
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                        //  command.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@FunctionName", "InsertAchivement");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }
            }
            return false;
        }

        private void LoadAchivementData()
        {
            Achivementlist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from Achievement where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AchivementBL Course = new AchivementBL();
                    Course.Proid = Convert.ToInt32(reader["ProfileID"]);
                    Course.Des = reader["Description"].ToString();
                    Achivementlist.Add(Course);
                }
                reader.Close();
            }
        }

        public bool DeleteAchivement(int Achivementid,string name, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE Achievement SET Active = 0 WHERE ProfileID = @ProfileID AND Description=@Description";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ProfileID", Achivementid);
                command.Parameters.AddWithValue("@Description", name);


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
                        error = "No rows were updated.Achivement ID may not exist.";
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
                        command2.Parameters.AddWithValue("@FunctionName", "DeleteAchivement");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        
        public AchivementBL GetAchivementById(int jid)
        {
            foreach (AchivementBL jo in Achivementlist)
            {
                if (jo.Proid == jid)
                {
                    return jo;
                }
            }
            return null;

        }
        public int GetAchivementId(string name)
        {
            foreach (AchivementBL jo in Achivementlist)
            {
                if (jo.Des == name)
                {
                    return jo.Proid;
                }
            }
            return -1;

        }

    }
}
