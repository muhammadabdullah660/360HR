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
using System.Text.RegularExpressions;
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
            if (errorProvider1.GetError(txtfname) != "" || errorProvider1.GetError(txtlname) != "" || errorProvider1.GetError(txtemail) != "" || errorProvider1.GetError(txtphone) != "" || errorProvider1.GetError(txtpassword) != "" || errorProvider1.GetError(txtstate) != "" || errorProvider1.GetError(txtstreet) != "" || errorProvider1.GetError(txtcountry) != "" || errorProvider1.GetError(cmbrole) != "" || errorProvider1.GetError(cmbgender) != "" || txtfname.Text=="" || txtlname.Text==""||txtemail.Text==""||txtphone.Text==""||txtpassword.Text==""||cmbgender.Text==""||cmbrole.Text==""||txtcountry.Text==""||txtstate.Text==""||txtstreet.Text=="")
            {
                MessageBox.Show("Enter Information Correctly");
            }
            else
            {
                RoleDL role = new RoleDL();
                PersonBL person = new PersonBL();
                AddressDL ad = new AddressDL();
                int aid = -1;
               
                int addressId = -1; // Initialize addressId outside of if blocks
                bool isSuccess = false; // Initialize isSuccess outside of if blocks

                // Check if the address already exists
                if (aid == -1)
                {
                    AddressBL address = new AddressBL(txtcountry.Text, txtstate.Text, txtstreet.Text);

                    string error;
                    // Call the InsertAddress function and pass the address object as a parameter
                    isSuccess = ad.InsertAddress(address, out addressId, out error);

                    if (isSuccess)
                    {
                        Console.WriteLine("Address inserted successfully. Address ID: " + addressId);
                    }
                    else
                    {
                        Console.WriteLine("Failed to insert the address. Error: " + error);
                    }
                }
                else
                {
                    Console.WriteLine("Address already exists. Using existing Address ID: " + aid);
                    addressId = aid;
                    MessageBox.Show(addressId.ToString());
                }

                // Check the return value and output values
                if (isSuccess || addressId != -1)
                {
                    Console.WriteLine("Address inserted successfully. Address ID: " + addressId);
                    person.Email = txtemail.Text;
                    person.Password = txtpassword.Text;
                    person.RoleId = role.GetRoleID(cmbrole.Text);
                    person.AddressId = addressId;
                    person.Firstname = txtfname.Text;
                    person.Lastname = txtlname.Text;
                    person.Gender = cmbgender.Text[0];
                    person.MobileNumber=txtphone.Text;
                    // Call the InsertPerson method to insert the person into the database
                    PersonDL personDL = new PersonDL();
                    string errorMessage;
                    int PersonId = personDL.InsertPerson(person, out errorMessage);
                    Console.WriteLine("Address inserted successfully. Address ID: " + PersonId);
                    if (PersonId != 0)
                    {
                        MessageBox.Show("Person inserted successfully!");
                        this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("Failed to insert");
                    }

                }
               
            }
        }

        private void txtstreet_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtpassword_Validating(object sender, CancelEventArgs e)
        {
            //Regex.IsMatch(txtweightage.Text, @"\d"
                if (!string.IsNullOrWhiteSpace(txtpassword.Text) && txtpassword.Text.Length==8)
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtpassword, "");

                }
                else
                {
                    e.Cancel = true;
                    txtpassword.Focus();
                    errorProvider1.SetError(txtpassword, "Invalid");
                }
            
        }

        private void txtfname_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtfname.Text) && !Regex.IsMatch(txtfname.Text, ".*\\d.*"))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtfname, "");

            }
            else
            {
                e.Cancel = true;
                txtfname.Focus();
                errorProvider1.SetError(txtfname, "Invalid");
            }
        }

        private void txtlname_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtlname.Text) && !Regex.IsMatch(txtfname.Text, ".*\\d.*"))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtlname, "");

            }
            else
            {
                e.Cancel = true;
                txtlname.Focus();
                errorProvider1.SetError(txtlname, "Invalid");
            }
        }

        static bool IsValidEmail(string email)
        {
            // Define a regular expression pattern for a simple email validation
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            // Create a Regex object and match the email against the pattern
            Regex regex = new Regex(pattern);
            Match match = regex.Match(email);

            // Return true if the email matches the pattern, otherwise false
            return match.Success;
        }
        private void txtemail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtemail.Text) && IsValidEmail(txtemail.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtemail, "");

            }
            else
            {
                e.Cancel = true;
                txtemail.Focus();
                errorProvider1.SetError(txtemail, "Invalid");
            }
        }

        private void txtphone_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtphone.Text) && txtphone.Text.Length == 11)
            {
                e.Cancel = false;
                errorProvider1.SetError(txtphone, "");

            }
            else
            {
                e.Cancel = true;
                txtphone.Focus();
                errorProvider1.SetError(txtphone, "Invalid");
            }

        }

        private void txtstreet_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtstreet.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtstreet, "");

            }
            else
            {
                e.Cancel = true;
                txtstreet.Focus();
                errorProvider1.SetError(txtstreet, "Invalid");
            }
        }

        private void txtstate_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtstate.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtstate, "");

            }
            else
            {
                e.Cancel = true;
                txtstate.Focus();
                errorProvider1.SetError(txtstate, "Invalid");
            }
        }

        private void txtcountry_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtcountry.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtcountry, "");

            }
            else
            {
                e.Cancel = true;
                txtcountry.Focus();
                errorProvider1.SetError(txtcountry, "Invalid");
            }
        }

        private void cmbrole_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cmbrole.Text)) 
            {
                e.Cancel = false;
                errorProvider1.SetError(cmbrole, "");

            }
            else
            {
                e.Cancel = true;
                cmbrole.Focus();
                errorProvider1.SetError(cmbrole, "Invalid");
            }
        }

        private void cmbgender_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cmbgender.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(cmbgender, "");

            }
            else
            {
                e.Cancel = true;
                cmbgender.Focus();
                errorProvider1.SetError(cmbgender, "Invalid");
            }
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }
    }
}
