using ApplicantTrackingPlatform.DL;
using System;
using ApplicantTrackingPlatform.BL;
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
    public partial class DisplayEducation : UserControl
    {
        public event EventHandler DeleteButtonClicked;
        public DisplayEducation()
        {
            InitializeComponent();
           // this.button2.Click += new System.EventHandler(this.button2_Click);
        }

        #region Properties
        private string degree;
        private string institute;
        private string start;
        private string end;
        private string grade;
        private string id;

        [Category("Custom props")]
        public string Id
        {
            get { return id; }
            set { id = value;}
        }

        [Category("Custom props")]
        public string Degree
        {
            get { return degree; }
            set { degree = value; label6.Text = value; }
        }
        [Category("Custom props")]
        public string Institute
        {
            get { return institute; }
            set { institute = value; label7.Text = value; }
        }
        [Category("Custom props")]
        public string Start
        {
            get { return start; }
            set { start = value; label8.Text = value; }
        }
        [Category("Custom props")]
        public string End
        {
            get { return end; }
            set { end = value; label9.Text = value; }
        }
        [Category("Custom props")]
        public string Grade
        {
            get { return grade; }
            set {grade = value; label10.Text = value; }
        }

        #endregion
       

        private void button2_Click(object sender, EventArgs e)
        {
         

            OnDeleteButtonClicked();

        }
        protected virtual void OnDeleteButtonClicked()
        {
            // Check if there are subscribers to the event
            DeleteButtonClicked?.Invoke(this, EventArgs.Empty);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox5.Visible = true;
            dateTimePicker1.Visible = true;
            dateTimePicker2.Visible = true;
            textBox1.Text = label6.Text;
            textBox2.Text = label7.Text;
            dateTimePicker1.Value= Convert.ToDateTime(label8.Text);
            dateTimePicker2.Value = Convert.ToDateTime(label9.Text);
            textBox5.Text= label10.Text;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            EducationDL ed = new EducationDL();
            EducationBL eb = ed.GetEducationById(int.Parse(Id));
            if (eb.Degree != textBox1.Text || eb.Institute != textBox2.Text || eb.Grade != textBox5.Text || eb.Start != dateTimePicker1.Value || dateTimePicker2.Value != eb.End)
            {
                if (ed.UpdateEducation(int.Parse(Id), textBox1.Text, textBox2.Text, textBox5.Text, dateTimePicker1.Value, dateTimePicker2.Value, out string er))
                {
                    MessageBox.Show("Update Successfully!!");

                }
                else
                {
                    MessageBox.Show("Error Occurr");
                }
            }
            button3.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox5.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            label6.Text=textBox1.Text;
            label7.Text= textBox2.Text ;
            label8.Text = dateTimePicker1.Value.ToShortDateString();
            label9.Text = dateTimePicker2.Value.ToShortDateString();
            label10.Text = textBox5.Text;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;

        }

        private void DisplayEducation_Load(object sender, EventArgs e)
        {

        }
    }
}
