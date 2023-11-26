using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    class EducationDL
    {
        private string connectionString;
        public List<EducationBL> Educationlist = new List<EducationBL>();

        public EducationDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadEducationData();
        }

        public List<EducationBL> GetAllEducation()
        {
            return Educationlist;
        }
        public int InsertEducation(EducationBL Education, out int EducationId, out string error)
        {
            EducationId = 0;
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Education (InstituteName,Degree,StartYear,EndYear,Grade,ProfileID) OUTPUT INSERTED.ID VALUES (@InstituteName,@Degree,@StartYear,@EndYear,@Grade,@ProfileID)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@InstituteName", Education.Institute);
                command.Parameters.AddWithValue("@Degree", Education.Degree);
                command.Parameters.AddWithValue("@StartYear", Education.Start);
                command.Parameters.AddWithValue("@EndYear", Education.End);
                command.Parameters.AddWithValue("@Grade", Education.Grade);
                command.Parameters.AddWithValue("@ProfileID", Education.Profileid);

                try
                {
                    // Execute the SQL command and retrieve the newly created address ID
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out EducationId))
                    {
                        return EducationId;
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
                        command2.Parameters.AddWithValue("@FunctionName", "InsertEducation");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return -1;
                }
            }
            return -1;
        }

        private void LoadEducationData()
        {
            Educationlist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from Education where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EducationBL Education = new EducationBL();
                    Education.Id = Convert.ToInt32(reader["ID"]);
                    Education.Start = Convert.ToDateTime(reader["StartYear"]);
                    Education.End = Convert.ToDateTime(reader["EndYear"]);
                    Education.Profileid = Convert.ToInt32(reader["ProfileID"]);
                    Education.Degree = reader["Degree"].ToString();
                    Education.Institute = reader["InstituteName"].ToString();
                    Education.Grade = reader["Grade"].ToString();
                    Educationlist.Add(Education);
                }
                reader.Close();
            }
        }

        public bool DeleteEducation(int Educationid, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE Education SET Active = 0 WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", Educationid);

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
                        error = "No rows were updated.Education ID may not exist.";
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
                        command2.Parameters.AddWithValue("@FunctionName", "DeleteEducation");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        public bool UpdateEducation(int Educationid, string degree, string ins,string grade,DateTime start,DateTime end, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE Education SET updatedAT = GetDate(),Degree=@Degree,InstituteName=@InstituteName,StartYear=@StartYear,EndYear=@EndYear,Grade=@Grade WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", Educationid);
                // command.Parameters.AddWithValue("@CompanyID", jb.Companyid);
                command.Parameters.AddWithValue("@InstituteName", ins);
                command.Parameters.AddWithValue("@Degree", degree);
                command.Parameters.AddWithValue("@StartYear", start);
                command.Parameters.AddWithValue("@EndYear", end);
                command.Parameters.AddWithValue("@Grade", grade);
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
                        error = "No rows were updated. Education ID may not exist.";
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
                        command2.Parameters.AddWithValue("@FunctionName", "UpdateEducation");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        public EducationBL GetEducationById(int jid)
        {
            foreach (EducationBL jo in Educationlist)
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
