using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    class ApplicantDL
    {
        private string connectionString;
        public List<ApplicantBL> Applicantlist = new List<ApplicantBL>();

        public ApplicantDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadApplicantData();
        }

        public List<ApplicantBL> GetAllApplicant()
        {
            return Applicantlist;
        }
        public int InsertApplicant(ApplicantBL Applicant, out int ApplicantId, out string error)
        {
            ApplicantId = 0;
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Profile (PersonID,ProfilePicture,LinkedInLink,isFreelancer) OUTPUT INSERTED.ID VALUES (@PersonID,@ProfilePicture,@LinkedInLink,@IsFreelancer)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@PersonID", Applicant.Personid);
                command.Parameters.Add("@ProfilePicture", SqlDbType.VarBinary, -1).Value = Applicant.Image;
                command.Parameters.AddWithValue("@LinkedInLink", Applicant.Linkutl);
                command.Parameters.AddWithValue("@isFreelancer", Applicant.Isfreelancer);
                // Execute the SQL command and retrieve the newly created address ID
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out ApplicantId))
                {
                    return ApplicantId;
                }
                else
                {
                    // Handle the case where the insertion failed or the addressId couldn't be retrieved.
                    error = "Insertion failed or addressId couldn't be retrieved.";
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                        //  command.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@FunctionName", "InsertApplicant");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return -1;
                }
            }
        }
        public bool UpdateApplicant(int aid, string url, bool isfree,byte[] img, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE Profile SET updatedAT = GetDate(),LinkedInLink=@LinkedInLink,isFreelancer=@isFreelancer,ProfilePicture=@ProfilePicture WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", aid);
                // command.Parameters.AddWithValue("@CompanyID", jb.Companyid);
                command.Parameters.AddWithValue("@LinkedInLink", url);
                command.Parameters.AddWithValue("@isFreelancer", isfree);
                command.Parameters.AddWithValue("@ProfilePicture", img);
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
                    error = "Error updating the database: " + ex.Message;
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                        //  command.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@FunctionName", "UpdateApplicant");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }
        private void LoadApplicantData()
        {
            Applicantlist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from Profile where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ApplicantBL Applicant = new ApplicantBL();
                    Applicant.Id = Convert.ToInt32(reader["ID"]);
                    Applicant.Personid = Convert.ToInt32(reader["PersonID"]);
                    Applicant.Linkutl = reader["LinkedInLink"].ToString();
                    Applicant.Isfreelancer = Convert.ToBoolean(reader["isFreelancer"]);
                    byte[] imageBytes = (byte[])reader["ProfilePicture"];
                    Applicant.Image = imageBytes;
                    Applicantlist.Add(Applicant);
                }
                reader.Close();
            }
        }

        public ApplicantBL GetApplicantbyId(int pid)
        {
            foreach (ApplicantBL a in Applicantlist)
            {
                if (pid == a.Personid)
                {
                    return a;
                }
            }
            return null;
        }

        public bool isApplicant(int pid)
        {
            foreach (ApplicantBL a in Applicantlist)
            {
                if (pid == a.Personid)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
