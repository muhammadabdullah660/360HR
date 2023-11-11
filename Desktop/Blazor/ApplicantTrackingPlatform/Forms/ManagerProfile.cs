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
using System.Windows.Controls;
using System.Windows.Forms;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class ManagerProfile : Form
    {
        private int pid;
        private int mid;
        private string companyName="";
        private string companyDescription="";
        private string comapnyContact="";
        private string street = "";
        private string state = "";
        private string country = "";
        public ManagerProfile(int pid,int mid)
        {
            InitializeComponent();
            this.pid = pid;
            this.mid = mid;
        }
        public ManagerProfile(int pid,string companyName,string companyDescription,string comapnyContact,string street,string state,string country)
        {
            InitializeComponent();
            this.pid = pid;
            this.companyName=companyName;
            this.companyDescription=companyDescription;
            this.comapnyContact=comapnyContact;
            this.street=street;
            this.state=state;
            this.country=country;

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void ManagerProfile_Load(object sender, EventArgs e)
        {
            PersonDL p = new PersonDL();
            PersonBL per = p.GetPersonById(pid);
            label1.Text = per.Email;
            label2.Text = per.Firstname +" " +per.Lastname;
            label3.Text = per.MobileNumber;
            textBox7.Text = per.Firstname;
            textBox6.Text = per.Lastname;
            textBox11.Text = per.MobileNumber;
            AddressDL a = new AddressDL();
            List<AddressBL> al = a.LoadAddresses();
            foreach(AddressBL ad in al)
            {
                if(ad.Id==per.AddressId)
                {
                    textBox8.Text = ad.Country;
                    textBox9.Text = ad.State;
                    textBox10.Text = ad.StreetNo;
                }
            }
            
            if (mid == -1)
            {
                textBox10.Enabled=false;
                textBox9.Enabled=false;
                textBox8.Enabled = false;
                textBox7.Enabled = false;
                textBox6.Enabled = false;
                textBox11.Enabled = false;
                //richTextBox1.Text = this.companyDescription;
                //textBox1.Text = this.companyName;
                //textBox5.Text = this.comapnyContact;
                //textBox4.Text = this.country;
                //textBox2.Text = this.state;
                //textBox3.Text = this.street;
                button1.Enabled = false;
            }
            else
            {
                button2.Enabled = false;
                ManagerDL m = new ManagerDL();
                ManagerBL ma=m.GetManagerbyId(pid);
                CompanyDL c = new CompanyDL();
                CompanyBL co = c.GetCompanyById(ma.Companyid);
                textBox1.Text= co.Name;
                richTextBox1.Text = co.Description;
                textBox5.Text = co.Contact;
                AddressBL ad = a.GetAddressById(co.Addressid);
                textBox4.Text = ad.Country;
                textBox2.Text = ad.State;
                textBox3.Text = ad.StreetNo;
                
            }
        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(textBox1) != "" || errorProvider1.GetError(richTextBox1) != "" || errorProvider1.GetError(textBox5) != "" || errorProvider1.GetError(textBox2) != "" || errorProvider1.GetError(textBox3) != "" || errorProvider1.GetError(textBox4) != "" || errorProvider1.GetError(textBox6) != "" || errorProvider1.GetError(textBox7) != "" || errorProvider1.GetError(textBox8) != "" || errorProvider1.GetError(textBox9) != "" || errorProvider1.GetError(textBox10) != "" || errorProvider1.GetError(textBox11) != ""  || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || richTextBox1.Text == "" || textBox7.Text == "" || textBox8.Text==""||textBox9.Text==""||textBox10.Text==""||textBox11.Text=="")
            {
                MessageBox.Show("Enter Information Correctly");
            }
            else
            {
                ManagerDL man = new ManagerDL();
                AddressDL ad = new AddressDL();
                int aid = -1;
                int addressId = -1; // Initialize addressId outside of if blocks
                bool isSuccess = false; // Initialize isSuccess outside of if blocks

                // Check if the address already exists
                if (aid == -1)
                {
                    AddressBL address = new AddressBL(textBox4.Text, textBox2.Text, textBox3.Text);

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
                if (isSuccess || addressId != -1)
                {
                    CompanyDL co = new CompanyDL();
                    int cid = co.GetCompanyId(textBox1.Text, richTextBox1.Text, textBox5.Text, addressId);

                    int CompId = -1; // Initialize addressId outside of if blocks
                    bool ComapnyInserted = false; // Initialize isSuccess outside of if blocks

                    // Check if the address already exists
                    if (cid == -1)
                    {
                        CompanyBL company = new CompanyBL(textBox1.Text, richTextBox1.Text, textBox5.Text, addressId);

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
                        MessageBox.Show(CompId.ToString());
                    }
                    if (ComapnyInserted || CompId != -1)
                    {
                        ManagerBL m = new ManagerBL(pid, CompId, true);
                        int mi = -1;
                        string err;
                        if (man.InsertManager(m, out mi, out err) != -1)
                        {
                            MessageBox.Show("Manager Inserted!!");

                        }
                        else
                        {
                            MessageBox.Show("Error Occur" + err);
                        }
                        this.mid = mi;
                    }

                }
                this.Refresh();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(textBox1) != "" || errorProvider1.GetError(richTextBox1) != "" || errorProvider1.GetError(textBox5) != "" || errorProvider1.GetError(textBox2) != "" || errorProvider1.GetError(textBox3) != "" || errorProvider1.GetError(textBox4) != "" || errorProvider1.GetError(textBox6) != "" || errorProvider1.GetError(textBox7) != "" || errorProvider1.GetError(textBox8) != "" || errorProvider1.GetError(textBox9) != "" || errorProvider1.GetError(textBox10) != "" || errorProvider1.GetError(textBox11) != "" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || richTextBox1.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "" || textBox11.Text == "")
            {
                MessageBox.Show("Enter Information Correctly");
            }
            else
            {
                PersonDL p = new PersonDL();
                PersonBL pe = p.GetPersonById(pid);
                if(pe.Firstname!=textBox7.Text || pe.Lastname!=textBox6.Text || pe.MobileNumber!=textBox11.Text)
                {
                    if(p.UpdatePerson(pid, textBox7.Text, textBox6.Text, textBox11.Text, out String err))
                    {
                        MessageBox.Show("Updated Succesfully!!!");
                    }
                    else
                    {
                        MessageBox.Show("Error in Updating");
                    }
                }
                AddressDL a = new AddressDL();
                List<AddressBL> adl = a.LoadAddresses();
                foreach(AddressBL ad in adl)
                {
                    if(ad.Id==pe.AddressId)
                    {
                        if (ad.StreetNo != textBox10.Text || ad.State != textBox9.Text || ad.Country != textBox8.Text)
                        {
                            if(a.UpdateAddress(pe.AddressId, textBox8.Text, textBox9.Text, textBox10.Text, out String error))
                            {
                                MessageBox.Show("Updated Succesfully!!!");
                            }
                            else
                            {
                                MessageBox.Show("Enter Unique Address");
                            }

                        }
                    }
                }
                

                ManagerDL m = new ManagerDL();
                ManagerBL ma = m.GetManagerbyId(pid);
                CompanyDL c = new CompanyDL();
                CompanyBL co = c.GetCompanyById(ma.Companyid);
                if(co.Name!=textBox1.Text || richTextBox1.Text!=co.Description || co.Contact!=textBox5.Text)
                {
                    if(c.UpdateCompany(co.Id, textBox1.Text, richTextBox1.Text, textBox5.Text, out String er))
                    {
                        MessageBox.Show("Updated Succesfully!!!");
                    }
                    else
                    {
                        MessageBox.Show("Error in Updating");
                    }

                }

                foreach (AddressBL ade in adl)
                {
                    if (ade.Id == co.Addressid)
                    {
                        if (ade.StreetNo != textBox3.Text || ade.State != textBox2.Text || ade.Country != textBox4.Text)
                        {
                            if(a.UpdateAddress(co.Addressid, textBox4.Text, textBox2.Text, textBox3.Text, out string errrr))
                            {
                                MessageBox.Show("Updated Successfully!!!");
                            }
                            else
                            {
                                MessageBox.Show("Enter Unique Address");
                            }
                        }
                    }
                }


            }
        }

        private void textBox7_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox7.Text) && !Regex.IsMatch(textBox7.Text, ".*\\d.*"))
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox7, "");

            }
            else
            {
                e.Cancel = true;
                textBox7.Focus();
                errorProvider1.SetError(textBox7, "Invalid");
            }
        }

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox6.Text) && !Regex.IsMatch(textBox6.Text, ".*\\d.*"))
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox6, "");

            }
            else
            {
                e.Cancel = true;
                textBox6.Focus();
                errorProvider1.SetError(textBox6, "Invalid");
            }
        }

        private void textBox11_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox11.Text) && textBox11.Text.Length == 11)
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

        private void textBox8_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox8.Text))
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

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox1, "");

            }
            else
            {
                e.Cancel = true;
                textBox1.Focus();
                errorProvider1.SetError(textBox1, "Invalid");
            }
        }

        private void richTextBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(richTextBox1, "");

            }
            else
            {
                e.Cancel = true;
                richTextBox1.Focus();
                errorProvider1.SetError(richTextBox1, "Invalid");
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

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox5.Text) && IsValidEmail(textBox5.Text))
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

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox3.Text))
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

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox2, "");

            }
            else
            {
                e.Cancel = true;
                textBox2.Focus();
                errorProvider1.SetError(textBox2, "Invalid");
            }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox4.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox4, "");

            }
            else
            {
                e.Cancel = true;
                textBox4.Focus();
                errorProvider1.SetError(textBox4, "Invalid");
            }
        }
    }
}
