using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    class AddressDL
    {
        private List<AddressBL> addressList;
        private string connectionString;

        // Constructor
        public AddressDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            addressList = new List<AddressBL>();
           
        }
        public AddressBL GetAddressByIdfromList(int id)
        {
            return addressList.FirstOrDefault(s => s.Id == id);
        }
        // Load addresses from database
        public AddressBL GetAddressById(int id)
        {
            AddressBL address = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Address WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        address = new AddressBL
                        {
                            Id = (int)reader["ID"],
                            Country = (string)reader["country"],
                            State = (string)reader["state"],
                            StreetNo = (string)reader["streetNo"]
                        };
                    }
                }
            }

            return address;
        }

        public List<AddressBL> LoadAddresses()
        {
            addressList.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Address", connection);
                command.CommandType = CommandType.Text; // Use CommandType.Text for plain SQL SELECT

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AddressBL address = new AddressBL();
                        address.Id = Convert.ToInt32(reader["ID"]);
                        address.Country = reader["Country"].ToString();
                        address.State = reader["State"].ToString();
                        address.StreetNo = reader["StreetNo"].ToString();
                        // Set other properties of the AddressBL object as needed
                        addressList.Add(address);
                    }
                }
            }

            return addressList;
           
        }


        public bool InsertAddress(AddressBL address, out int addressId, out string error)
        {
            addressId = 0;
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Address (Country, State, StreetNo) OUTPUT INSERTED.ID VALUES (@Country, @State, @StreetNo)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@Country", address.Country);
                command.Parameters.AddWithValue("@State", address.State);
                command.Parameters.AddWithValue("@StreetNo", address.StreetNo);

                // Execute the SQL command and retrieve the newly created address ID
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out addressId))
                {
                    return true;
                }
                else
                {
                    // Handle the case where the insertion failed or the addressId couldn't be retrieved.
                    error = "Insertion failed or addressId couldn't be retrieved.";
                    return false;
                }
            }
        }

        // Update an existing address
        public void UpdateAddress(AddressBL previousUpdate, AddressBL newUpdate, out string error)
        {
            error = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Address SET Country = @Country, State = @State, StreetNo = @StreetNo WHERE ID = @ID", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", previousUpdate.Id);
                    command.Parameters.AddWithValue("@Country", newUpdate.Country);
                    command.Parameters.AddWithValue("@State", newUpdate.State);
                    command.Parameters.AddWithValue("@StreetNo", newUpdate.StreetNo);

                    // Execute the stored procedure
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exception and set error message
                error = "Error updating address in database: " + ex.Message;
            }
        }
    }
}
