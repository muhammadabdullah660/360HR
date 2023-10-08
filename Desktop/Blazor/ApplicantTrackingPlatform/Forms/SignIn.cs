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
            this.Close();
            if (role != null)
            {
                //  int personId = person.SearchPersonId(username, password);
                // Handle successful sign-in, e.g., show a message box with the role
                MessageBox.Show("Role: " + role, "Sign In Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {
                MessageBox.Show("Error in finding role");
            }
            HRManagerMenu m = new HRManagerMenu();
            m.Show();
        }

        private void SignIn_Load(object sender, EventArgs e)
        {

        }
    }
}
