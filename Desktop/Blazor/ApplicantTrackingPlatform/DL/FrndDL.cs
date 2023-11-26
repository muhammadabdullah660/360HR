using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    class FrndDL
    {
        private string connectionString;
        public List<FrndBL> Frndlist = new List<FrndBL>();

        public FrndDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadFrndData();
        }

        public List<FrndBL> GetAllFrnd()
        {
            return Frndlist;
        }
        public bool InsertFrnd(FrndBL f, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO FriendshipRequest (ProfileIDSender,ProfileIDReceiver,StatusID,DateSent) VALUES (@ProfileIDSender,@ProfileIDReceiver,@StatusID,@DateSent)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@ProfileIDSender", f.Senderid);
                command.Parameters.AddWithValue("ProfileIDReceiver", f.Receiverid);
                command.Parameters.AddWithValue("StatusID", f.Status);
                command.Parameters.AddWithValue("DateSent", f.Send);
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
                        command2.Parameters.AddWithValue("@FunctionName", "InsertFrnd");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }
            }
            return false;
        }
        public List<FrndBL> GetReceivedRequests(int receiverId)
        {
            List<FrndBL> receivedRequests = new List<FrndBL>();

            foreach (FrndBL frnd in Frndlist)
            {
                if (frnd.Receiverid == receiverId)
                {
                    receivedRequests.Add(frnd);
                }
            }

            return receivedRequests;
        }
        public List<FrndBL> GetSentRequests(int aid)
        {
            List<FrndBL> sentRequests = new List<FrndBL>();

            foreach (FrndBL f in Frndlist)
            {
                if (f.Senderid == aid)
                {
                    sentRequests.Add(f);
                }
            }

            return sentRequests;
        }

        private void LoadFrndData()
        {
            Frndlist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from FriendshipRequest where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FrndBL Course = new FrndBL();
                    Course.Send = Convert.ToDateTime(reader["DateSent"]);
                    Course.Senderid = Convert.ToInt32(reader["ProfileIDSender"]);
                    Course.Receiverid = Convert.ToInt32(reader["ProfileIDReceiver"]);
                    Course.Status = Convert.ToInt32(reader["StatusID"]);
                    Course.Id = Convert.ToInt32(reader["ID"]);
                   Frndlist.Add(Course);
                }
                reader.Close();
            }
        }

        public bool UpdatedFrnd(int id,int status, out string error)
        {
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("update FriendshipRequest set updatedAT=getdate(),StatusID=@StatusID where ID=@ID", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("StatusID", status);
                command.Parameters.AddWithValue("ID", id);
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
                    error = "Updation failed couldn't be retrieved." + ex.Message;
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                        //  command.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@FunctionName", "UpdateFrnd");
                        command2.Parameters.AddWithValue("@ExceptionMessage", error);

                        // Execute the stored procedure
                        command2.ExecuteNonQuery();
                    }
                    return false;
                }
            }
            return false;
        }
    }
}
