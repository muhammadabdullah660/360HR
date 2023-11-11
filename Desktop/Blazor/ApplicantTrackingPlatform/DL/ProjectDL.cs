using ApplicantTrackingPlatform.BL;
using ApplicantTrackingPlatform.Forms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
     class ProjectDL
    {

        private string connectionString;
        public List<ProjectBL> Projectlist = new List<ProjectBL>();

        public ProjectDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadProjectData();
        }

        public List<ProjectBL> GetAllProject()
        {
            return Projectlist;
        }
        public int InsertProject(ProjectBL Project, out int ProjectId, out string error)
        {
            
            ProjectId = 0;
            error = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Project (Description,StartDate,EndDate,Managerid,Title) OUTPUT INSERTED.ID VALUES (@Description,@StartDate,@EndDate,@Managerid,@Title)", connection);

                    // Assuming "address" is an instance of your Address class
                    command.Parameters.AddWithValue("@Description", Project.Description);
                    command.Parameters.AddWithValue("@StartDate", Project.Start);
                    command.Parameters.AddWithValue("@EndDate", Project.End);
                    command.Parameters.AddWithValue("@Managerid", Project.Managerid);
                    command.Parameters.AddWithValue("@Title", Project.Title);



                    // Execute the SQL command and retrieve the newly created address ID
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out ProjectId))
                    {
                        return ProjectId;
                    }
                    else
                    {
                        // Handle the case where the insertion failed or the addressId couldn't be retrieved.
                        error = "Insertion failed or addressId couldn't be retrieved.";
                        return -1;
                    }
                }
            }
            catch(Exception ex)
            {
                error = "Error updating the database: " + ex.Message;
                using (SqlConnection connection1 = new SqlConnection(connectionString))
                {
                    connection1.Open();
                    SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                    //  command.CommandType = CommandType.StoredProcedure;
                    command2.Parameters.AddWithValue("@FunctionName", "InsertProject");
                    command2.Parameters.AddWithValue("@ExceptionMessage", error);

                    // Execute the stored procedure
                    command2.ExecuteNonQuery();
                }
                return -1;
            }
        }

        private void LoadProjectData()
        {
            Projectlist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from Project where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProjectBL Project = new ProjectBL();
                    Project.Id = Convert.ToInt32(reader["ID"]);
                    Project.Managerid = Convert.ToInt32(reader["Managerid"]);
                    Project.Start = Convert.ToDateTime(reader["StartDate"]);
                    Project.End = Convert.ToDateTime(reader["EndDate"]);
                    Project.Title = reader["Title"].ToString();
                    Project.Description = reader["Description"].ToString();
                    Projectlist.Add(Project);
                }
                reader.Close();
            }
        }

        public bool DeleteProject(int Projectid, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE Project SET Active = 0 WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", Projectid);

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
                        error = "No rows were updated. Job ID may not exist.";
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
                        command2.Parameters.AddWithValue("@FunctionName", "DeleteProject");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        public bool UpdateProject(int Projectid, string title, string des,DateTime start,DateTime end, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to avoid SQL injection
                string query = "UPDATE Project SET updatedAT = GetDate(),Title=@Title,Description=@Description,StartDate=@StartDate,EndDate=@EndDate WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameter
                command.Parameters.AddWithValue("@ID", Projectid);
                // command.Parameters.AddWithValue("@CompanyID", jb.Companyid);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Description", des);
                command.Parameters.AddWithValue("@StartDate", start);
                command.Parameters.AddWithValue("@EndDate", end);
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
                        error = "No rows were updated. Job ID may not exist.";
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
                        command2.Parameters.AddWithValue("@FunctionName", "UpdateProject");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }

            }
        }

        public ProjectBL GetProjectById(int proid)
        {
            foreach (ProjectBL pro in Projectlist)
            {
                if (pro.Id == proid)
                {
                    return pro;
                }
            }
            return null;

        }


    }
}
