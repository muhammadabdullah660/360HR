using ApplicantTrackingPlatform.BL;
using ApplicantTrackingPlatform.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class AddWorkExperience : Form
    {
        private int pid;
        private int aaid;
        public AddWorkExperience(int pid, int aaid)
        {
            InitializeComponent();
            this.pid = pid;
            this.aaid = aaid;
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(textBox11) == "" && errorProvider1.GetError(textBox10) == "" && errorProvider1.GetError(textBox8) == "" && errorProvider1.GetError(textBox5) == "" && errorProvider1.GetError(textBox9) == "" && textBox10.Text != "" && textBox11.Text != "" && textBox5.Text != "" && textBox8.Text != "" && textBox9.Text != "")
            {
                WorkDL man = new WorkDL();
                AddressDL ad = new AddressDL();
                int aid = -1;
                int addressId = -1; // Initialize addressId outside of if blocks
                bool isSuccess = false; // Initialize isSuccess outside of if blocks

                // Check if the address already exists
                if (aid == -1)
                {
                    AddressBL address = new AddressBL(textBox4.Text, textBox6.Text, textBox7.Text);

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
                }
                if (isSuccess || addressId != -1)
                {
                    CompanyDL co = new CompanyDL();
                    int cid = co.GetCompanyId(textBox11.Text, textBox10.Text, textBox8.Text, addressId);

                    int CompId = -1; // Initialize addressId outside of if blocks
                    bool ComapnyInserted = false; // Initialize isSuccess outside of if blocks

                    // Check if the address already exists
                    if (cid == -1)
                    {
                        CompanyBL company = new CompanyBL(textBox11.Text, textBox10.Text, textBox8.Text, addressId);

                        string error;
                        // Call the InsertAddress function and pass the address object as a parameter
                        ComapnyInserted = co.InsertCompany(company, out CompId, out error);

                        if (ComapnyInserted)
                        {
                            Console.WriteLine("Comapny inserted successfully. Company ID: " + CompId);
                        }
                        else
                        {
                            Console.WriteLine("Failed to insert the Company. Error: " + error);
                        }
                    }
                    else
                    {
                        CompId = cid;
                    }
                    if (ComapnyInserted || CompId != -1)
                    {
                        WorkBL m = new WorkBL(CompId, textBox9.Text, (float)Convert.ToDecimal(textBox5.Text), dateTimePicker3.Value, aaid);
                        int mi = -1;
                        string err;
                        if (man.InsertWork(m, out mi, out err) != -1)
                        {
                            MessageBox.Show("Work Experience Inserted!!");

                        }
                        else
                        {
                            MessageBox.Show("Error Occur" + err);
                        }
                    }


                }
                LoadData();
            }
            else
            {
                MessageBox.Show("Enter Information");
            }

        }

        private void AddWorkExperience_Load(object sender, EventArgs e)
        {

            PersonDL p = new PersonDL();
            PersonBL pe = p.GetPersonById(pid);
            label11.Text = pe.Email;
            label10.Text = pe.Firstname + " " + pe.Lastname;
            label9.Text = pe.MobileNumber;
            LoadData();

        }
        private void DisplayEducation_DeleteButtonClicked(object sender, EventArgs e)
        {
            if (sender is DiaplayWork displayEducationControl)
            {
                WorkDL el = new WorkDL();
                if (el.DeleteWork(int.Parse(displayEducationControl.Id), out string error))
                {
                    MessageBox.Show("Successfully Delete!!");
                }
                else
                {
                    MessageBox.Show("Error Occurr!!");
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            WorkDL ed = new WorkDL();
            CompanyDL co = new CompanyDL();
            AddressDL ad = new AddressDL();
            List<WorkBL> edl = ed.Worklist;
            flowLayoutPanel1.Controls.Clear();
            foreach (WorkBL eb in edl)
            {
                if (eb.Profileid == aaid)
                {
                    DiaplayWork de = new DiaplayWork();

                    CompanyBL com = co.GetCompanyById(eb.Companyid);
                    AddressBL ab = ad.GetAddressById(com.Addressid);
                    de.Comname = com.Name;
                    de.Comdes = com.Description;
                    de.Comcontact = com.Contact;
                    de.Country = ab.Country;
                    de.State = ab.State;
                    de.Street = ab.StreetNo;
                    de.Start = eb.Start.ToShortDateString();
                    de.Duration = eb.Duration.ToString();
                    de.Role = eb.Role;
                    de.Id = eb.Id.ToString();
                    de.Proid = aaid.ToString();
                    de.Comid = eb.Companyid.ToString();
                    de.Adid=com.Addressid.ToString();
                    de.DeleteButtonClicked += DisplayEducation_DeleteButtonClicked;
                    flowLayoutPanel1.Controls.Add(de);

                }

            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox11_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox11.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox11, "");

            }
            else
            {
                e.Cancel = true;
                textBox11.Focus();
                errorProvider1.SetError(textBox11, "Invalid");
            }
        }

        private void textBox10_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox10.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox10, "");

            }
            else
            {
                e.Cancel = true;
                textBox10.Focus();
                errorProvider1.SetError(textBox10, "Invalid");
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
        private void textBox8_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox8.Text) && IsValidEmail(textBox8.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox8, "");

            }
            else
            {
                e.Cancel = true;
                textBox8.Focus();
                errorProvider1.SetError(textBox8, "Invalid");
            }
        }

        private void textBox7_Validating(object sender, CancelEventArgs e)
        {

        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox5.Text) && int.TryParse(textBox5.Text, out _))
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox5, "");

            }
            else
            {
                e.Cancel = true;
                textBox5.Focus();
                errorProvider1.SetError(textBox5, "Invalid");
            }
        }

        private void textBox9_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox9.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox9, "");

            }
            else
            {
                e.Cancel = true;
                textBox9.Focus();
                errorProvider1.SetError(textBox9, "Invalid");
            }
        }
    }
}
