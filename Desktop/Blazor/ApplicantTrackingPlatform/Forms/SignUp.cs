using ApplicantTrackingPlatform.BL;
using ApplicantTrackingPlatform.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Entered in Register");
            PersonBL person = new PersonBL();
            person.Email = txtemail.Text;
            person.Password = txtpassword.Text;
            person.RoleId = Convert.ToInt32(cmbrole.SelectedValue);
            MessageBox.Show(person.RoleId.ToString());
            AddressBL address = new AddressBL(txtcountry.Text, txtstate.Text, txtstreet.Text);
            AddressDL addressid = new AddressDL();
            int addressId;
            string error;
            // Call the InsertAddress function and pass the address object as a parameter
            bool isSuccess = addressid.InsertAddress(address, out addressId, out error);

            // Check the return value and output values
            if (isSuccess)
            {    MessageBox.Show("Entered in condition and addres registered");
            
                Console.WriteLine("Address inserted successfully. Address ID: " + addressId);
                person.AddressId = addressId;
                person.Firstname = txtfname.Text;
                person.Lastname = txtlname.Text;
                object selectedValue = cmbgender.SelectedValue;

                if (selectedValue != null)
                {
                    if (selectedValue is char)
                    {
                        person.Gender = (char)selectedValue;
                    }
                    else
                    {
                        // Handle the case where the selected value is not a valid char
                        // This could be due to incorrect data binding or an unexpected value
                        // You may want to log an error or set a default value in this case.
                        person.Gender = 'M'; // Replace defaultChar with your desired default value.
                    }
                }
                else
                {
                    // Handle the case where nothing is selected (selectedValue is null)
                    person.Gender = 'M'; // Set a default value in this case as well.
                }

                MessageBox.Show(person.Gender.ToString());
                person.MobileNumber = txtphone.Text;
                // Call the InsertPerson method to insert the person into the database
                PersonDL personDL = new PersonDL();
                string errorMessage;
                int PersonId = personDL.InsertPerson(person, out errorMessage);
                if (PersonId != 0)
                {
                    MessageBox.Show("Person inserted successfully!");

                }
                else
                {
                    MessageBox.Show("Failed to insert. Error: " + errorMessage);
                }

            }
            else
            {
                MessageBox.Show("Failed to insert. Error: " + error);
            }
        }
    }
}
