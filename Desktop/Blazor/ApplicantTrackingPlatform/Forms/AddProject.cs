using ApplicantTrackingPlatform.BL;
using ApplicantTrackingPlatform.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class AddProject : Form
    {
        private int proid;
        private int pid;
        private int mid;
        private Form activeForm = null;
        public AddProject(int pid, int mid, int proid)
        {
            this.pid = pid;
            this.mid = mid;
            this.proid = proid;
            InitializeComponent();
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void LoadData()
        {
            List<string> namesToShow = new List<string>();
            PersonDL p = new PersonDL();

            ManagerDL m = new ManagerDL();
            foreach (ManagerBL ma in m.GetAllManager())
            {
                if (ma.IsManager == false)
                {
                    PersonBL pe = p.GetPersonById(ma.Personid);
                    namesToShow.Add(pe.Email);
                }
            }

            // Bind the list of names to the ComboBox
            comboBox1.DataSource = namesToShow;
        }
        private void AddProject_Load(object sender, EventArgs e)
        {
            LoadData();
            ManagerDL m = new ManagerDL();
            ManagerBL ma = m.GetManagerbyId(pid);
            PersonDL p = new PersonDL();
            PersonBL pb = p.GetPersonById(pid);
            label1.Text = pb.Email;
            label2.Text = pb.Firstname + " " + pb.Lastname;
            label3.Text = pb.MobileNumber;
            if (proid == -1)
            {
                button4.Enabled = false;

            }
            else
            {
                button2.Enabled = false;
                ProjectDL j = new ProjectDL();
                ProjectBL jo = j.GetProjectById(proid);
                textBox1.Text = jo.Title;
                richTextBox1.Text = jo.Description;
                dateTimePicker1.Value = jo.Start;
                dateTimePicker2.Value = jo.End;
                ProjectSkillDL js = new ProjectSkillDL();
                List<string> skills = js.GetSkill(proid);

                foreach (string skill in skills)
                {
                    textBox7.Text = skill;
                    button3_Click(sender, new EventArgs());
                }


            }
        }
        private void removeButton_Click(object sender, EventArgs e)
        {
            // Get the container control of the clicked button
            Control parentControl = ((Button)sender).Parent;

            // Remove the button immediately following the clicked button
            Button clickedButton = (Button)sender;
            int clickedButtonIndex = parentControl.Controls.IndexOf(clickedButton);

            if (clickedButtonIndex >= 0 && clickedButtonIndex < parentControl.Controls.Count)
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
            Skill.Text = val;
            Skill.Enabled = false;

            // Create a new "Remove" button
            Button removeButton = new Button();
            removeButton.Text = "X";
            removeButton.BackColor = Color.Red;
            removeButton.ForeColor = Color.White;
            removeButton.Size = new Size(25, 25);
            removeButton.Name = val;
            removeButton.Click += removeButton_Click;

            // Add the label and button to the FlowLayoutPanel
            flowLayoutPanel1.Controls.Add(Skill);
            flowLayoutPanel1.Controls.Add(removeButton);

            //label14.Text = textBox7.Text;
            textBox7.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(textBox1) != "" || errorProvider1.GetError(richTextBox1) != "" || errorProvider1.GetError(dateTimePicker1) != "" || errorProvider1.GetError(dateTimePicker2) != "" || textBox1.Text == "" || richTextBox1.Text == "" || dateTimePicker1.Text == "" || dateTimePicker2.Text == "" )
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
                ProjectDL Project = new ProjectDL();

                ManagerDL m = new ManagerDL();
                ManagerBL ma = m.GetManagerbyId(pid);
                int pro = 0;
                string error = "";
                ProjectBL j = new ProjectBL();
                j.Title = textBox1.Text;
                j.Description = richTextBox1.Text;
                j.Start = dateTimePicker1.Value;
                j.End = dateTimePicker2.Value;
                j.Managerid = ma.Id;
                PersonDL p = new PersonDL();
                int id = -1;
                foreach (PersonBL pe in p.GetAllPersons())
                {
                    if (pe.Email == comboBox1.Text)
                    {
                        id = pe.Id;
                    }
                }
                ManagerBL me = m.GetManagerbyId(id);
                j.Recid = me.Id;
                proid = Project.InsertProject(j, out pro, out error);
                
                ProjectSkillDL js = new ProjectSkillDL();
                List<ProjectSkillBL> sjl = new List<ProjectSkillBL>();
                sjl = js.GetLsitBySkill(s, proid);
                string err = "";
                js.InsertProjectSkill(sjl, out err);
                if (proid!=- 1)
                {
                    MessageBox.Show("Inserted Successfully!!");
                }
                else
                {
                    MessageBox.Show("Error While Inserting!!");

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ViewProject(mid, pid));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(textBox1) != "" || errorProvider1.GetError(richTextBox1) != "" || errorProvider1.GetError(dateTimePicker1) != "" || errorProvider1.GetError(dateTimePicker2) != "" || textBox1.Text == "" || richTextBox1.Text == "" || dateTimePicker1.Text == "" || dateTimePicker2.Text == "")
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
                ProjectSkillDL js = new ProjectSkillDL();
                List<string> os = js.GetSkill(proid);
                List<string> toInsert = ns.Except(os).ToList();
                List<string> toDelete = os.Except(ns).ToList();
                string err = "";
                List<ProjectSkillBL> de = js.GetLsitBySkill(toDelete, proid);
                js.DeleteProjectSkill(de, out err);

                List<ProjectSkillBL> i = js.GetLsitBySkill(toInsert, proid);
                js.InsertProjectSkill(i, out err);
                PersonDL p = new PersonDL();
                int id = -1;
                foreach (PersonBL pe in p.GetAllPersons())
                {
                    if (pe.Email == comboBox1.Text)
                    {
                        id = pe.Id;
                    }
                }
                ManagerDL m = new ManagerDL();
                ManagerBL me = m.GetManagerbyId(id);
                
                ProjectDL j = new ProjectDL();
                ProjectBL pb = j.GetProjectById(proid);
                if (pb.Title!=textBox1.Text || pb.Description!=richTextBox1.Text||dateTimePicker1.Value!=pb.Start||dateTimePicker2.Value!=pb.End || pb.Recid!=me.Id)
                {
                    if (j.UpdateProject(proid, textBox1.Text, richTextBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value,me.Id, out err))
                    {
                        MessageBox.Show("Updated Successfully!!");
                    }
                }
                OpenChildForm(new ViewProject(mid, pid));

            }
        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

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

        private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(dateTimePicker1, "");

            }
            else
            {
                e.Cancel = true;
                dateTimePicker1.Focus();
                errorProvider1.SetError(dateTimePicker1, "Invalid");
            }
        }

        private void dateTimePicker2_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(dateTimePicker2, "");

            }
            else
            {
                e.Cancel = true;
                dateTimePicker2.Focus();
                errorProvider1.SetError(dateTimePicker2, "Invalid");
            }
        }
    } 
 }

