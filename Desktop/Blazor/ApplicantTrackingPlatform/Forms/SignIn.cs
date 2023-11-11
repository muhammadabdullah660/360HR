using ApplicantTrackingPlatform.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            
            string username = textBox1.Text;
            string password = textBox2.Text;
            PersonDL person = new PersonDL();
            string role = person.SignIn(username, password);
            int pid = person.getPersonId(username, password);

            if (role != null)
            {// Create a list of all the open Home pages.
                var openHomeForms = Application.OpenForms.OfType<Home>().ToList();

                // Iterate over the list and close each form.
                foreach (Home form in openHomeForms)
                {
                    form.Hide();
                }
                var newHomePage = new Home(role, pid);
                newHomePage.Show();


                // Handle successful sign-in, e.g., show a message box with the role.
                //  MessageBox.Show("Role: " + role, "Sign In Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);



                // Handle successful sign-in, e.g., show a message box with the role.
                //  MessageBox.Show("Role: " + role, "Sign In Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            else
            {
                
                MessageBox.Show("Error in finding role");
                
            }
            this.Hide();

           
        }

        private void SignIn_Load(object sender, EventArgs e)
        {

        }

        private void SignIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked) // Assuming 'checkBox1' is the name of your CheckBox
            {
                textBox2.PasswordChar = '*'; // Set the PasswordChar to a character like '*' to hide the text
            }
            else
            {
                textBox2.PasswordChar = '\0'; // Set PasswordChar to '\0' to show the text in plain text
            }
        }
    }
}
