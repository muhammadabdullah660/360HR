using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicantTrackingPlatform.BL;
using static System.Windows.Forms.AxHost;
using System.Security.Cryptography;

namespace ApplicantTrackingPlatform.DL
{
    class CompanyDL
    {
        private string connectionString;
        public List<CompanyBL> Companylist = new List<CompanyBL>();

        public CompanyDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadCompanyData();
        }

        public List<CompanyBL> GetAllCompany()
        {
            return Companylist;
        }
        public bool InsertCompany(CompanyBL Company, out int CompanyId, out string error)
        {
            CompanyId = 0;
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Company (CompanyName, Description, AddressID,Contact) OUTPUT INSERTED.ID VALUES (@CompanyName, @Description, @AddressID,@Contact)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@CompanyName", Company.Name);
                command.Parameters.AddWithValue("@Description", Company.Description);
                command.Parameters.AddWithValue("@AddressID", Company.Addressid);
                command.Parameters.AddWithValue("@Contact", Company.Contact);


                // Execute the SQL command and retrieve the newly created address ID
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out CompanyId))
                {
                    return true;
                }
                else
                {
                    // Handle the case where the insertion failed or the addressId couldn't be retrieved.
                    error = "Insertion failed couldn't be retrieved.";
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                        //  command.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@FunctionName", "InsertCompany");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }
            }
        }

        private void LoadCompanyData()
        {
            Companylist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from Company where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CompanyBL Company = new CompanyBL();
                    Company.Id = Convert.ToInt32(reader["ID"]);
                    Company.Name = reader["CompanyName"].ToString();
                    Company.Description = reader["Description"].ToString();
                    Company.Addressid = Convert.ToInt32(reader["AddressID"]);
                    Company.Contact = reader["Contact"].ToString();
                    Companylist.Add(Company);
                }
                reader.Close();
            }
        }

        public int GetCompanyId(string name,string des,string con,int aid)
        {
            foreach(CompanyBL co in Companylist)
            {
                if(co.Name==name && co.Description==des&& co.Contact==con && co.Addressid==aid)
                {
                    return co.Id;
                }

            }
            return -1;
        }
        public CompanyBL GetCompanyById(int id)
        {
            foreach (CompanyBL co in Companylist)
            {
                if (id == co.Id)
                {
                    return co;
                }

            }
            return null; 
        }

        public bool UpdateCompany(int cid,string name,string des,string con, out string error)
        {
            error = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Company SET updatedAT=GetDate(),CompanyName = @CompanyName, Description = @Description, Contact = @Contact WHERE ID = @ID", connection);
                    //command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID",cid);
                    command.Parameters.AddWithValue("@CompanyName",name);
                    command.Parameters.AddWithValue("@Description", des);
                    command.Parameters.AddWithValue("@Contact", con);

                    // Execute the stored procedure
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception and set error message
                error = "Error updating in database: " + ex.Message;
                using (SqlConnection connection1 = new SqlConnection(connectionString))
                {
                    connection1.Open();
                    SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                    //  command.CommandType = CommandType.StoredProcedure;
                    command2.Parameters.AddWithValue("@FunctionName", "UpdateCompany");
                    command2.Parameters.AddWithValue("@ExceptionMessage", error);

                    // Execute the stored procedure
                    command2.ExecuteNonQuery();
                }
                return false;
            }
        }

    }
}
