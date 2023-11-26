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
using System.Xml;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class AddJob : Form
    {
        private int jid;
        private int pid;
        private int mid;
        private Form activeForm = null;
        public AddJob(int pid, int mid,int jid)
        {
            InitializeComponent();
            this.pid = pid;
            this.mid = mid;
            this.jid = jid;
        }
        private void OpenChildForm(Form childfrom)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childfrom;
            childfrom.TopLevel = false;
            childfrom.FormBorderStyle = FormBorderStyle.None;
            childfrom.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childfrom);
            panelChildForm.Tag = childfrom;
            childfrom.BringToFront();
            childfrom.Show();
        }
        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            
        }

        private void textBox7_AcceptsTabChanged(object sender, EventArgs e)
        {
           
        }
        private void removeButton_Click(object sender, EventArgs e)
        {
            // Get the container control of the clicked button
            Control parentControl = ((Button)sender).Parent;

            // Remove the button immediately following the clicked button
            Button clickedButton = (Button)sender;
            int clickedButtonIndex = parentControl.Controls.IndexOf(clickedButton);

            if (clickedButtonIndex >= 0 && clickedButtonIndex < parentControl.Controls.Count )
            {
                Control buttonToRemove = parentControl.Controls[clickedButtonIndex - 1];
                if (buttonToRemove is Button)
                {
                    parentControl.Controls.Remove(buttonToRemove);
                    parentControl.Controls.Remove(clickedButton);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // Create a new label
            string val = textBox7.Text;

            // Create a new label
           // Label myLabel = new Label();
            //myLabel.Margin = new Padding(5);
            //myLabel.Text = val;
            //myLabel.Font = new Font("Calibri", 9);
            //myLabel.BorderStyle = BorderStyle.FixedSingle;
            //myLabel.AutoSize = true;
            //myLabel.Visible = true;

            Button Skill = new Button();
            Skill.Text= val;
            Skill.Enabled = false;

            // Create a new "Remove" button
            Button removeButton = new Button();
            removeButton.Text = "X";
            removeButton.BackColor = Color.Red;
            removeButton.ForeColor = Color.White;
            removeButton.Size= new Size(25, 25);
            removeButton.Name = val;
            removeButton.Click += removeButton_Click;

            // Add the label and button to the FlowLayoutPanel
            flowLayoutPanel1.Controls.Add(Skill);
            flowLayoutPanel1.Controls.Add(removeButton);

            //label14.Text = textBox7.Text;
            textBox7.Text = null;

        }

        private void label14_Click(object sender, EventArgs e)
        {
            
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(textBox1) != "" || errorProvider1.GetError(richTextBox1) != "" || errorProvider1.GetError(textBox5) != "" || errorProvider1.GetError(textBox2) != "" || errorProvider1.GetError(textBox3) != "" || errorProvider1.GetError(textBox4) != "" || errorProvider1.GetError(textBox6) != ""  || errorProvider1.GetError(richTextBox2) != "" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || richTextBox1.Text == "" || richTextBox2.Text == "" )
            {
                MessageBox.Show("Enter Information Correctly");
            }
            else
            {
                List<string> s = new List<string>();
                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    if (control is Button)
                    {

                        Button button = (Button)control;
                        if (button.Text != "X" && button.Text != null)
                        {
                            s.Add(button.Text);
                        }
                    }
                }
                JobDL job = new JobDL();
                AddressDL ad = new AddressDL();
                int aid = ad.GetAddressId(textBox4.Text, textBox2.Text, textBox3.Text);

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
                    }
                    if (ComapnyInserted || CompId != -1)
                    {
                        ManagerDL m = new ManagerDL();
                        ManagerBL ma = m.GetManagerbyId(pid);
                        int ji = 0;
                        string error = "";
                        JobBL j = new JobBL();
                        j.Title = textBox6.Text;
                        j.Description = richTextBox2.Text;
                        j.Companyid = CompId;
                        j.Managerid = ma.Id;
                        jid = job.InsertJob(j, out ji, out error);
                        JobSkillDL js = new JobSkillDL();
                        List<JobSkillBL> sjl = new List<JobSkillBL>();
                        sjl = js.GetLsitBySkill(s, jid);
                        string err = "";
                        js.InsertJobSkill(sjl, out err);
                        if(jid!=-1)
                        {
                            MessageBox.Show("Insert Successfully!!");

                        }
                        else
                        {
                            MessageBox.Show("Error While insert!!");
                        }

                    }
                }
            }
        }

        private void AddJob_Load(object sender, EventArgs e)
        {

            PersonDL p = new PersonDL();
            PersonBL pb = p.GetPersonById(pid);
            label1.Text = pb.Email;
            label2.Text = pb.Firstname + " " + pb.Lastname;
            label3.Text = pb.MobileNumber;
            if (jid == -1)
            {
                button4.Enabled=false;
                ManagerDL m = new ManagerDL();
                ManagerBL ma = m.GetManagerbyId(pid);
                CompanyDL c = new CompanyDL();
                CompanyBL co = c.GetCompanyById(ma.Companyid);
                textBox1.Text = co.Name;
                richTextBox1.Text = co.Description;
                textBox5.Text = co.Contact;
                AddressDL a = new AddressDL();
                AddressBL ad = a.GetAddressById(co.Addressid);
                textBox3.Text = ad.StreetNo;
                textBox2.Text = ad.State;
                textBox4.Text = ad.Country;
            }
            else
            {
                button2.Enabled=false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                richTextBox1.Enabled = false;
                JobDL j = new JobDL();
                JobBL jo = j.GetJobById(jid);
                textBox6.Text = jo.Title;
                richTextBox2.Text = jo.Description;
                CompanyDL c = new CompanyDL();
                CompanyBL co = c.GetCompanyById(jo.Companyid);
                textBox1.Text = co.Name;
                richTextBox1.Text = co.Description;
                textBox5.Text = co.Contact;
                AddressDL a = new AddressDL();
                AddressBL ad = a.GetAddressById(co.Addressid);
                textBox3.Text = ad.StreetNo;
                textBox2.Text = ad.State;
                textBox4.Text = ad.Country;
                JobSkillDL js = new JobSkillDL();
                List<string> skills = js.GetSkill(jid);

                foreach (string skill in skills)
                {
                    textBox7.Text = skill;
                    button3_Click(sender,new EventArgs());
                }


            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ViewJob(mid,pid));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(textBox1) != "" || errorProvider1.GetError(richTextBox1) != "" || errorProvider1.GetError(textBox5) != "" || errorProvider1.GetError(textBox2) != "" || errorProvider1.GetError(textBox3) != "" || errorProvider1.GetError(textBox4) != "" || errorProvider1.GetError(textBox6) != "" || errorProvider1.GetError(richTextBox2) != "" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || richTextBox1.Text == "" || richTextBox2.Text == "")
            {
                MessageBox.Show("Enter Information Correctly");
            }
            else
            {
                List<string> ns = new List<string>();
                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    if (control is Button)
                    {
                        Button button = (Button)control;
                        if (button.Text != "X" && button.Text != null)
                        {
                            ns.Add(button.Text);
                        }
                    }
                }
                JobSkillDL js = new JobSkillDL();
                List<string> os = js.GetSkill(jid);
                List<string> toInsert = ns.Except(os).ToList();
                List<string> toDelete = os.Except(ns).ToList();
                string err = "";
                List<JobSkillBL> de = js.GetLsitBySkill(toDelete, jid);
                js.DeleteJobSkill(de, out err);

                List<JobSkillBL> i = js.GetLsitBySkill(toInsert, jid);
                js.InsertJobSkill(i, out err);

                JobDL j = new JobDL();
                JobBL jb = j.GetJobById(jid);
                if (jb.Title!=textBox6.Text||jb.Description!=richTextBox2.Text) 
                {
                    if (j.UpdateJob(jid, textBox6.Text, richTextBox2.Text, out err))
                    {
                        MessageBox.Show("Updated Successfully!!");
                    }
                }
                OpenChildForm(new ViewJob(mid, pid));

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

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox6.Text))
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

        private void richTextBox2_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(richTextBox2.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(richTextBox2, "");

            }
            else
            {
                e.Cancel = true;
                richTextBox2.Focus();
                errorProvider1.SetError(richTextBox2, "Invalid");
            }
        }
    }
}
