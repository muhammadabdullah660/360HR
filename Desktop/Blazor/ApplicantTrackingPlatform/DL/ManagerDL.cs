using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    class ManagerDL
    {
        private string connectionString;
        public List<ManagerBL> Managerlist = new List<ManagerBL>();

        public ManagerDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadManagerData();
        }

        public List<ManagerBL> GetAllManager()
        {
            return Managerlist;
        }
        public int InsertManager(ManagerBL Manager, out int ManagerId, out string error)
        {
            ManagerId = 0;
            error = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Manager (Personid,Companyid,isManager ) OUTPUT INSERTED.Managerid VALUES (@Personid, @Companyid,@isManager)", connection);

                // Assuming "address" is an instance of your Address class
                command.Parameters.AddWithValue("@Personid", Manager.Personid);
                command.Parameters.AddWithValue("@Companyid", Manager.Companyid);
                command.Parameters.AddWithValue("@isManager", Manager.IsManager);

                // Execute the SQL command and retrieve the newly created address ID
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out ManagerId))
                {
                    return ManagerId;
                }
                else
                {
                    // Handle the case where the insertion failed or the addressId couldn't be retrieved.
                    error = "Insertion failed or addressId couldn't be retrieved.";
                    return -1;
                }
            }
        }

        private void LoadManagerData()
        {
            Managerlist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from Manager where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ManagerBL Manager = new ManagerBL();
                    Manager.Id = Convert.ToInt32(reader["Managerid"]);
                    Manager.Personid = Convert.ToInt32(reader["Personid"]);
                    Manager.Companyid = Convert.ToInt32(reader["Companyid"]);
                    Manager.IsManager = Convert.ToBoolean(reader["isManager"]);
                    Managerlist.Add(Manager);
                }
                reader.Close();
            }
        }

        public ManagerBL GetManagerbyId(int pid)
        {
            foreach(ManagerBL m in Managerlist)
            {
                if (pid == m.Personid)
                {
                    return m;
                }
            }
            return null;
        }

        public bool isManager(int pid)
        {
            foreach (ManagerBL m in Managerlist)
            {
                if (pid == m.Personid)
                {
                    return true;
                }
            }
            return false;
        }

    }
}

