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
    public partial class AddInterview : Form
    {
        private int pid;
        private int mid;
        private int jod;
        private Form activeForm = null;
        public AddInterview(int pid, int mid, int jod)
        {
            InitializeComponent();
            this.pid = pid;
            this.mid = mid;
            this.jod = jod;
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
        private void AddInterview_Load(object sender, EventArgs e)
        {
            PersonDL p = new PersonDL();
            PersonBL pe = p.GetPersonById(pid);
            label11.Text = pe.Firstname + " " + pe.Lastname;
            label10.Text = pe.Email;
            label9.Text = pe.MobileNumber;
        }
        public void LoadData()
        {
            flowLayoutPanel1.Controls.Clear();
            InterviewSlotDL s = new InterviewSlotDL();
            foreach (InterviewSlotBL sb in s.GetAllInterviewSlot())
            {
                if (sb.Jobid == jod)
                {
                    // Create a label for the achievement
                    Label skillLabel = new Label();
                    skillLabel.Text = sb.Slot;
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
                    InterviewSlotDL a = new InterviewSlotDL();
                    int id = a.GetInterviewSlotId(labelToRemove.Text);

                    // Try to delete the achievement
                    if (a.Deleteslot(id, labelToRemove.Text, out string err))
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
            if (radioButton1.Checked == true || radioButton2.Checked == true)
            {
                int status;
                if(radioButton1.Checked)
                {
                    status = 2;
                    if (comboBox1.Text != "" && comboBox1.SelectedIndex != -1)
                    {
                        InterviewSlotDL s = new InterviewSlotDL();
                        InterviewSlotBL sb = new InterviewSlotBL(jod, comboBox1.Text);
                        if (s.InsertInterviewSlot(sb, out string er))
                        {
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Error!");
                        }

                    }
                }
                else
                {
                    status = 3;
                    comboBox1.Enabled = false;
                    button3.Enabled = false;

                }

                JobApplicantDL japp = new JobApplicantDL();
                japp.UpdateJobApplicant(jod, status, out string err);

            }
            else
            {
                MessageBox.Show("First accept or reject request");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ViewApplicant(pid, mid));
        }
    }
}
