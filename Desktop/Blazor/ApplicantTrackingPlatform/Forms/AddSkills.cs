using ApplicantTrackingPlatform.BL;
using ApplicantTrackingPlatform.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class AddSkills : Form
    {
        private int pid;
        private int aid;
        public AddSkills(int pid, int aid)
        {
            InitializeComponent();
            this.pid = pid;
            this.aid = aid;
        }

        private void AddSkills_Load(object sender, EventArgs e)
        {
            PersonDL p = new PersonDL();
            PersonBL pe = p.GetPersonById(pid);
            label11.Text = pe.Email;
            label10.Text = pe.Firstname + " " + pe.Lastname;
            label9.Text = pe.MobileNumber;
            LoadData();
        }
        private void LoadData()
        {
            flowLayoutPanel1.Controls.Clear();
            SkillDL a = new SkillDL();
            List<SkillBL> abl = a.Skilllist;

            foreach (SkillBL ab in abl)
            {
                if (ab.Proid == aid)
                {
                    // Create a label for the achievement
                    Label skillLabel = new Label();
                    skillLabel.Text = ab.Des;
                    skillLabel.Enabled = false;
                    skillLabel.ForeColor = Color.Black;
                    skillLabel.Font = new Font("Calibri", 10, FontStyle.Bold);
                    skillLabel.BorderStyle = BorderStyle.FixedSingle;
                    skillLabel.AutoSize = true;

                    // Create a "Remove" button
                    Button removeButton = new Button();
                    removeButton.Text = "X";
                    removeButton.BackColor = Color.Red;
                    removeButton.ForeColor = Color.White;
                    removeButton.Size = new Size(25, 25);
                    removeButton.Click += removeButton_Click;

                    // Add the label and button to the FlowLayoutPanel
                    flowLayoutPanel1.Controls.Add(skillLabel);
                    flowLayoutPanel1.Controls.Add(removeButton);
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
                // The label representing the achievement is at the previous index
                Control labelToRemove = parentControl.Controls[clickedButtonIndex - 1];

                if (labelToRemove is Label)
                {
                    SkillDL a = new SkillDL();
                    int id = a.GetSkillId(labelToRemove.Text);

                    // Try to delete the achievement
                    if (a.DeleteSkill(id, labelToRemove.Text, out string err))
                    {
                        MessageBox.Show("Delete Successfully!!");
                    }
                    else
                    {
                        MessageBox.Show("Delete Failed. Error: " + err);
                    }

                    // Remove both the label and the button
                    parentControl.Controls.Remove(labelToRemove);
                    parentControl.Controls.Remove(clickedButton);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != null)
            {
               SkillDL a = new SkillDL();
               SkillBL ab = new SkillBL(aid, richTextBox1.Text);
                if (a.InsertCourse(ab, out string err))
                {
                    MessageBox.Show("Insert Successfully!!");
                }
                else
                {
                    MessageBox.Show("Error Occurr!!");
                }
            }
            LoadData();
        }
    }
}
