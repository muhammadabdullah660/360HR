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
    public partial class DisplayCourses : UserControl
    {
        public event EventHandler DeleteButtonClicked;
        public DisplayCourses()
        {
            InitializeComponent();
        }

        #region Properties
        private string type;
        private string name;
        private string start;
        private string id;

        [Category("Custom props")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [Category("Custom props")]
        public string Type
        {
            get { return type; }
            set {type = value; label6.Text = value; }
        }
        [Category("Custom props")]
        public string Name
        {
            get { return name; }
            set { name= value; label7.Text = value; }
        }
        [Category("Custom props")]
        public string Start
        {
            get { return start; }
            set { start = value; label8.Text = value; }
        }
       

        #endregion
        private void DisplayCourses_Load(object sender, EventArgs e)
        {

        }

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
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            textBox1.Visible = true;
            textBox2.Visible = true;
            dateTimePicker1.Visible = true;
            button3.Visible = true;
            textBox1.Text = label6.Text;
            textBox2.Text = label7.Text;
            dateTimePicker1.Value = Convert.ToDateTime(label8.Text);
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            textBox1.Visible = false;
            textBox2.Visible = false;
            dateTimePicker1.Visible = false;
            button3.Visible = false;
            label6.Text = textBox1.Text;
            label7.Text = textBox2.Text;
            label8.Text = dateTimePicker1.Value.ToShortDateString();
            CourseDL c = new CourseDL();
            CourseBL co = c.GetCourseById(int.Parse(Id));
            if(co.Type!=textBox1.Text|| co.Name!=textBox2.Text||dateTimePicker1.Value!=co.Start)
            {
                if (c.UpdateCourse(int.Parse(Id), textBox1.Text, textBox2.Text, dateTimePicker1.Value, out string et))
                {
                    MessageBox.Show("Updated Successfully!!");
                }
                else
                {
                    MessageBox.Show("Error in Updating");

                }
            }
        }
    }
}
