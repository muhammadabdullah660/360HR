using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    internal class SkillDL
    {
        private string connectionString;
        public List<SkillBL> Skilllist = new List<SkillBL>();

        public SkillDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadSkillData();
        }

        public List<SkillBL> GetAllSkill()
        {
            return Skilllist;
        }
        public bool InsertCourse(SkillBL Skill, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Skills (ProfileID,Name) VALUES (@ProfileID,@Name)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@Name", Skill.Des);
                command.Parameters.AddWithValue("ProfileID", Skill.Proid);
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
                        command2.Parameters.AddWithValue("@FunctionName", "InsertSkill");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }
            }
            return false;
        }

        private void LoadSkillData()
        {
            Skilllist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from Skills where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SkillBL Course = new SkillBL();
                    Course.Proid = Convert.ToInt32(reader["ProfileID"]);
                    Course.Des = reader["Name"].ToString();
                    Skilllist.Add(Course);
                }
                reader.Close();
            }
        }

        public bool DeleteSkill(int Achivementid, string name, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE Skills SET Active = 0 WHERE ProfileID = @ProfileID AND Name=@Name";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ProfileID", Achivementid);
                command.Parameters.AddWithValue("@Name", name);


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
                        error = "No rows were updated.Skill ID may not exist.";
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
                        command2.Parameters.AddWithValue("@FunctionName", "DeleteSkill");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }


        public SkillBL GetSkillById(int jid)
        {
            foreach (SkillBL jo in Skilllist)
            {
                if (jo.Proid == jid)
                {
                    return jo;
                }
            }
            return null;

        }
        public int GetSkillId(string name)
        {
            foreach (SkillBL jo in Skilllist)
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
