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
using System.Windows.Forms;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class AddCourses : Form
    {
        private int pid;
        private int aid;
        public AddCourses(int pid, int aid)
        {
            InitializeComponent();
            LoadData();
            this.pid = pid;
            this.aid = aid;
        }
        public void LoadData()
        {
            CourseDL ed = new CourseDL();
            List<CourseBL> edl = ed.Courselist;
            flowLayoutPanel1.Controls.Clear();
            foreach (CourseBL eb in edl)
            {
                if (eb.Profileid == aid)
                {
                    DisplayCourses de = new DisplayCourses();
                    de.Type = eb.Type;
                    de.Name = eb.Name;
                    de.Start = eb.Start.ToShortDateString();
                    de.Id = eb.Id.ToString();
                    de.DeleteButtonClicked += DisplayEducation_DeleteButtonClicked;
                    flowLayoutPanel1.Controls.Add(de);

                }

            }

        }
        private void DisplayEducation_DeleteButtonClicked(object sender, EventArgs e)
        {
            if (sender is DisplayCourses displayEducationControl)
            {
                CourseDL el = new CourseDL();
                if (el.DeleteCourse(int.Parse(displayEducationControl.Id), out string error))
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
        private void AddCourses_Load(object sender, EventArgs e)
        {

            PersonDL p = new PersonDL();
            PersonBL pe=p.GetPersonById(pid);
            label11.Text = pe.Email;
            label10.Text = pe.Firstname + " " + pe.Lastname;
            label9.Text = pe.MobileNumber;
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox3.Text != "" && errorProvider1.GetError(textBox3) != "" && errorProvider1.GetError(textBox1) != "")
            {
                CourseDL c = new CourseDL();
                CourseBL co = new CourseBL(textBox1.Text, textBox3.Text, dateTimePicker1.Value, aid);
                if (c.InsertCourse(co, out int cid, out string er) != -1)
                {
                    MessageBox.Show("Inserted Successfully!!");

                }
                else
                {
                    MessageBox.Show("Error while inserting!");
                }
                LoadData();
            }
            else
            {
                MessageBox.Show("Enter Information");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !Regex.IsMatch(textBox1.Text, ".*\\d.*"))
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

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox3.Text) && !Regex.IsMatch(textBox3.Text, ".*\\d.*"))
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
