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
        public List<RoleBL> LoadRoles()
        {
            roleslst.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT RoleId, Name FROM Role"; // Update with your actual table name and column names
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            RoleBL role = new RoleBL(id, name);
                            roleslst.Add(role);
                        }
                    }
                }
            }

            return roleslst;
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
    }
}
