using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class PersonBL
    {
            private int id;
            private string firstname;
            private string lastname;
            private string email;
            private string password;
            private char gender;
            private int roleId;
            private int addressId;
            private string mobileNumber;

        public int Id { get => id; set => id = value; }
        public string Firstname { get => firstname; set => firstname = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public int RoleId { get => roleId; set => roleId = value; }
        public int AddressId { get => addressId; set => addressId = value; }
        public string MobileNumber { get => mobileNumber; set => mobileNumber = value; }
        public char Gender { get => gender; set => gender = value; }


        // Constructor with parameters
        public PersonBL(string firstname, string lastname, string email, string password,char gender, int roleId, int addressId, string mobileNumber)
            {
                this.Firstname = firstname;
                this.Lastname = lastname;
                this.Email = email;
                this.Password = password;
            
                this.RoleId = roleId;
                this.AddressId = addressId;
                this.MobileNumber = mobileNumber;
            }

            // Constructor with ID parameter
            public PersonBL(int id, string firstname,string lastname, string email, string password,char gender, int roleId, int addressId, string mobileNumber)
                : this(firstname,lastname, email, password,gender, roleId, addressId, mobileNumber)
            {
                this.Id = id;
            }

            // Default constructor
            public PersonBL()
            {
            }
        }
}
