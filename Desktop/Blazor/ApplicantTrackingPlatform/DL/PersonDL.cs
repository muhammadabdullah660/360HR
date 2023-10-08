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
     class PersonDL
    {
        private string connectionString;
        private Dictionary<int, PersonBL> personsDict = new Dictionary<int, PersonBL>();
        public PersonDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadPersons();
        }

        private void LoadPersons()
        {
            personsDict.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from Person", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PersonBL person = new PersonBL();
                    person.Id = Convert.ToInt32(reader["PersonId"]);
                    person.Firstname = reader["FirstName"].ToString();
                    person.Lastname = reader["LastName"].ToString();
                    person.Gender = Convert.ToChar(reader["Gender"]);
                    person.Email = reader["Email"].ToString();
                    person.MobileNumber = reader["PhoneNumber"].ToString();
                    person.Password = reader["Password"].ToString();                    
                    person.RoleId = Convert.ToInt32(reader["RoleId"]);                    
                    person.AddressId = Convert.ToInt32(reader["AddressId"]);
                    personsDict.Add(person.Id, person);
                }
                reader.Close();
            }
        }

        public List<PersonBL> GetAllPersons()
        {
            return personsDict.Values.ToList();
        }

        public string SignIn(string email, string password)
        {
            foreach (PersonBL person in personsDict.Values)
            {
                if (person.Email == email && person.Password == password)
                {
                    RoleDL role = new RoleDL();
                    string roleName = role.SearchRole(person.RoleId);// Return the role if username and password match
                    if (roleName != null)
                    {
                        return roleName;
                    }
                }
            }
            return null; // Return null if username and password do not match
        }

        public int InsertPerson(PersonBL person, out string error)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("Insert into Person (FirstName,LastName,Gender,Email,PhoneNumber,RoleId,AddressId) values (@FirstName,@LastName,@Gender,@Email,@PhoneNumber,@RoleId,@AddressId)", connection))
                {
                   
                    // Add input parameters
                    command.Parameters.AddWithValue("@FirstName", person.Firstname);
                    command.Parameters.AddWithValue("@LastName", person.Lastname);
                    command.Parameters.AddWithValue("@Gender", person.Gender);
                    command.Parameters.AddWithValue("@Email", person.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", person.MobileNumber);
                    command.Parameters.AddWithValue("@Password", person.Password);
                    command.Parameters.AddWithValue("@RoleId", person.RoleId);
                    command.Parameters.AddWithValue("@AddressId", person.AddressId);

                    // Add output parameter
                    SqlParameter outputParameter = new SqlParameter();
                    outputParameter.ParameterName = "@PersonId";
                    outputParameter.SqlDbType = SqlDbType.Int;
                    outputParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputParameter);

                    try
                    {
                        command.ExecuteNonQuery();
                        person.Id = Convert.ToInt32(outputParameter.Value);
                        personsDict.Add(person.Id, person); // Add the inserted person to the list
                        error = null;
                        return person.Id;
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                        return 0;
                    }
                }

            }
        }

    }
}
