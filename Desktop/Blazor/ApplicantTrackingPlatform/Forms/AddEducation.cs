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
    public partial class AddEducation : Form
    {
        private int pid;
        private int aid;
        public AddEducation(int pid, int aid)
        {
            InitializeComponent();
            this.pid = pid;
            this.aid = aid;
            LoadData();
       
        }
        private void DisplayEducation_DeleteButtonClicked(object sender, EventArgs e)
        {
            if (sender is DisplayEducation displayEducationControl)
            {
                EducationDL el = new EducationDL();
                if (el.DeleteEducation(int.Parse(displayEducationControl.Id), out string error))
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
      
        private void AddEducation_Load(object sender, EventArgs e)
        {
            LoadData();
            PersonDL p = new PersonDL();
            PersonBL pe=p.GetPersonById(pid);
            label11.Text = pe.Email;
            label10.Text = pe.Firstname + " " + pe.Lastname;
            label9.Text = pe.MobileNumber;

           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EducationDL el = new EducationDL();
            EducationBL ed = new EducationBL(textBox1.Text, textBox3.Text, dateTimePicker1.Value, dateTimePicker2.Value, textBox2.Text, aid);
            if (el.InsertEducation(ed, out int eid, out string error) != -1)
            {
                MessageBox.Show("Inserted Successfully!!");

            }
            else
            {
                MessageBox.Show("Error Occur");
            }
            LoadData();
           

           

        }
      

        public void LoadData()
        {
            EducationDL ed = new EducationDL();
            List<EducationBL> edl = ed.Educationlist;
            flowLayoutPanel1.Controls.Clear();
            foreach (EducationBL eb in edl)
            {
                if(eb.Profileid==aid)
                {
                    DisplayEducation de = new DisplayEducation();
                    de.Degree=eb.Degree;
                    de.Institute = eb.Institute;
                    de.Start = eb.Start.ToShortDateString();
                    de.End = eb.End.ToShortDateString();
                    de.Grade=eb.Grade;
                    de.Id=eb.Id.ToString();
                    de.DeleteButtonClicked += DisplayEducation_DeleteButtonClicked;
                    flowLayoutPanel1.Controls.Add(de);
                    
                }

            }

        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {
          
        }
    }
}
