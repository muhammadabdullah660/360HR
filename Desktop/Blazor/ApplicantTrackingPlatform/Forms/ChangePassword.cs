using ApplicantTrackingPlatform.BL;
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
    public partial class ChangePassword : Form
    {
        private int pid;
        public ChangePassword(int pid)
        {
            InitializeComponent();
            this.pid = pid;
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            PersonDL p=new PersonDL();
            PersonBL pe = p.GetPersonById(pid);
            label11.Text = pe.Firstname + " " + pe.Lastname;
            label10.Text = pe.Email;
            label9.Text = pe.MobileNumber;
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(textBox3) == "")
            {
                PersonDL p = new PersonDL();
                PersonBL pe = p.GetPersonById(pid);
                if (textBox1.Text == pe.Password)
                {
                    if (p.UpdatePassword(pid, textBox3.Text, out string er))
                    {
                        MessageBox.Show("Updated Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Error while Updating!");
                    }

                }
                else
                {
                    MessageBox.Show("Old Password Incorrect");
                }
            }
            else
            {
                MessageBox.Show("Something wrong with password try again");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox3.Text) && textBox3.Text.Length == 8)
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox3, "");

            }
            else
            {
                e.Cancel = true;
                textBox3.Focus();
                errorProvider1.SetError(textBox3, "Invalid");
            }
        }
    }
}
