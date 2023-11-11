using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Linq.Expressions;

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
                SqlCommand command = new SqlCommand("Select * from Person where Active=1", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PersonBL person = new PersonBL();
                    person.Id = Convert.ToInt32(reader["PersonID"]);
                    person.Firstname = reader["FirstName"].ToString();
                    person.Lastname = reader["LastName"].ToString();
                    person.Gender = Convert.ToChar(reader["Gender"]);
                    person.Email = reader["Email"].ToString();
                    person.MobileNumber = reader["PhoneNumber"].ToString();
                    person.Password = reader["Password"].ToString();                    
                    person.RoleId = Convert.ToInt32(reader["RoleID"]);
                    if (reader["AddressID"] != null)
                    {
                        person.AddressId = Convert.ToInt32(reader["AddressID"]);
                    }
                    else
                    {
                        person.AddressId = 0;

                    }
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

                    using (SqlCommand command = new SqlCommand("InsertPersonWithOutput", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
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
                        outputParameter.ParameterName = "@PersonID";
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
                            using (SqlConnection connection1 = new SqlConnection(connectionString))
                            {
                                connection1.Open();
                                SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                                //  command.CommandType = CommandType.StoredProcedure;
                                command2.Parameters.AddWithValue("@FunctionName", "InsertPerson");
                                command2.Parameters.AddWithValue("@ExceptionMessage", error);

                                // Execute the stored procedure
                                command2.ExecuteNonQuery();
                            }
                            return 0;
                        }
                    }
                }
           
           

            
        }
        public bool UpdatePerson(int pid, string fname, string lname, string con, out string error)
        {
            error = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Person SET updatedAT=GetDate(),FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber WHERE PersonID = @PersonID", connection);
                  //  command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", pid);
                    command.Parameters.AddWithValue("@FirstName", fname);
                    command.Parameters.AddWithValue("@LastName", lname);
                    command.Parameters.AddWithValue("@PhoneNumber", con);

                    // Execute the stored procedure
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception and set error message
                error = "Error updating address in database: " + ex.Message;
                using (SqlConnection connection1 = new SqlConnection(connectionString))
                {
                    connection1.Open();
                    SqlCommand command2 = new SqlCommand("Insert into ExceptionTable (FunctionName,ExceptionMessage) values (@FunctionName,@ExceptionMessage)", connection1);
                    //  command.CommandType = CommandType.StoredProcedure;
                    command2.Parameters.AddWithValue("@FunctionName", "UpdatePerson");
                    command2.Parameters.AddWithValue("@ExceptionMessage", error);

                    // Execute the stored procedure
                    command2.ExecuteNonQuery();
                }
                return false;
            }
        }
        public int getPersonId(string username,string password)
        {
            foreach (PersonBL person in personsDict.Values)
            {
                if (person.Email == username && person.Password == password)
                {
                    return person.Id;
                }
            }
            return -1;
        }

        public PersonBL GetPersonById(int id)
        {
            if (personsDict.ContainsKey(id))
            {
                return personsDict[id]; // Return the person object with the given ID
            }
            else
            {
                return null; // Person with the specified ID not found
            }
        }

    }
}
