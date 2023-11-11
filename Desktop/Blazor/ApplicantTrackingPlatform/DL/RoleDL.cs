using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
     class RoleDL
    {
        // Connection string
        private string connectionString;
        private List<RoleBL> roleslst = new List<RoleBL>();


        // Constructor
        public RoleDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadRoles();
        }

        // Function to load data from the database and return a list of RoleBL objects
        public void LoadRoles()
        {
            roleslst.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT RoleID, Name FROM Role"; // Update with your actual table name and column names
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["RoleID"]);
                            string name = reader["Name"].ToString();
                            RoleBL role = new RoleBL(id, name);
                            roleslst.Add(role);
                        }
                    }
                }
            }

        }
        public string SearchRole(int roleId)
        {
            RoleBL role = roleslst.Find(r => r.Id == roleId);
            if (role != null)
            {
                return role.Name; // Return the role name if username and password match
            }
            return null;
        }

        public int GetRoleID(string name)
        {
            int roleID = -1;             

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT RoleID FROM Role WHERE Name = @Name";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            roleID = reader.GetInt32(0); // Assuming the Role ID is in the first column (index 0)
                        }
                    }
                }
            }

            return roleID;
        }
    }
}
