using ApplicantTrackingPlatform.DL;
using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class AddApplyProject : Form
    {
        private int pid;
        private int aid;
        private int proid;
        private Form activeForm = null;
        public AddApplyProject(int pid, int aid, int proid)
        {
            InitializeComponent();
            this.pid = pid;
            this.aid = aid;
            this.proid = proid;
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

        private void AddApplyProject_Load(object sender, EventArgs e)
        {
            ApplicantDL a = new ApplicantDL();
            ApplicantBL ap = a.GetApplicantbyId(pid);
            PersonDL p = new PersonDL();
            PersonBL pe=p.GetPersonById(pid);
            ProjectDL project = new ProjectDL();
            ProjectBL project1 = project.GetProjectById(proid);
            label11.Text = pe.Email;
            label10.Text = pe.Firstname + " " + pe.Lastname;
            label9.Text = pe.MobileNumber;
            textBox1.Text = project1.Title;
            richTextBox1.Text = project1.Description;
            dateTimePicker1.Value = project1.Start;
            dateTimePicker2.Value = project1.End;
            textBox1.Enabled = false;
            richTextBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            ProjectSkillDL js = new ProjectSkillDL();
            List<string> skills = js.GetSkill(proid);

            foreach (string skill in skills)
            {
                // Create a new label
                string val = skill;

                // Create a new label
                 Label myLabel = new Label();
                myLabel.Margin = new Padding(5);
                myLabel.Text = val;
                myLabel.Font = new Font("Calibri", 9);
                myLabel.BorderStyle = BorderStyle.FixedSingle;
                myLabel.AutoSize = true;
                myLabel.Visible = true;

                //Button Skill = new Button();
                //Skill.Text = val;
                //Skill.Enabled = false;

                // Create a new "Remove" button
                //Button removeButton = new Button();
                //removeButton.Text = "X";
                //removeButton.BackColor = Color.Red;
                //removeButton.ForeColor = Color.White;
                //removeButton.Size = new Size(25, 25);
                //removeButton.Name = val;
                //removeButton.Click += removeButton_Click;

                // Add the label and button to the FlowLayoutPanel
                flowLayoutPanel1.Controls.Add(myLabel);
                //flowLayoutPanel1.Controls.Add(removeButton);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProjectApplicantDL pj = new ProjectApplicantDL();
            ProjectApplicantBL pjb = new ProjectApplicantBL(aid, proid, 1, (float)Convert.ToDecimal(textBox2.Text));
           if(pj.InsertProjectApplicant(pjb,out int id,out string er)!=-1)
            {
                MessageBox.Show("Your request send Successfully!!");
                OpenChildForm(new ApplyProject(pid, aid));
            }
            else
            {
                MessageBox.Show("Error Occur!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplicantDL m = new ApplicantDL();
            ApplicantBL ma = m.GetApplicantbyId(pid);
            if (ma != null)
            {
                aid = ma.Id;
                if (ma.Isfreelancer)
                {
                    OpenChildForm(new ApplyProject(pid, aid));
                }
              

            }
        }
    }
}
