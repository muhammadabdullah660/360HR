using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    internal class CourseDL
    {
        private string connectionString;
        public List<CourseBL> Courselist = new List<CourseBL>();

        public CourseDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadCourseData();
        }

        public List<CourseBL> GetAllCourse()
        {
            return Courselist;
        }
        public int InsertCourse(CourseBL Course, out int CourseId, out string error)
        {
            CourseId = 0;
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Courses (Type,Name,StartDate,ProfileID) OUTPUT INSERTED.ID VALUES (@Type,@Name,@StartDate,@ProfileID)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@Type", Course.Type);
                command.Parameters.AddWithValue("Name", Course.Name);
                command.Parameters.AddWithValue("@StartDate", Course.Start);
                command.Parameters.AddWithValue("@ProfileID", Course.Profileid);

                try
                {
                    // Execute the SQL command and retrieve the newly created address ID
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out CourseId))
                    {
                        return CourseId;
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
                        command2.Parameters.AddWithValue("@FunctionName", "InsertCourse");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return -1;
                }
            }
            return -1;
        }

        private void LoadCourseData()
        {
            Courselist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from Courses where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CourseBL Course = new CourseBL();
                    Course.Id = Convert.ToInt32(reader["ID"]);
                    Course.Start = Convert.ToDateTime(reader["StartDate"]);
                    Course.Name = reader["Name"].ToString();
                    Course.Profileid = Convert.ToInt32(reader["ProfileID"]);
                    Course.Type = reader["Type"].ToString();
                    Courselist.Add(Course);
                }
                reader.Close();
            }
        }

        public bool DeleteCourse(int Courseid, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE Courses SET Active = 0 WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", Courseid);

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
                        command2.Parameters.AddWithValue("@FunctionName", "DeleteCourse");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        public bool UpdateCourse(int Courseid, string type, string name,  DateTime start, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE Courses SET updatedAT = GetDate(),Type=@Type,Name=@Name,StartDate=@StartDate WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", Courseid);
                // command.Parameters.AddWithValue("@CompanyID", jb.Companyid);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Type", type);
                command.Parameters.AddWithValue("@StartDate", start);
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
                        command2.Parameters.AddWithValue("@FunctionName", "UpdateCourse");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        public CourseBL GetCourseById(int jid)
        {
            foreach (CourseBL jo in Courselist)
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
