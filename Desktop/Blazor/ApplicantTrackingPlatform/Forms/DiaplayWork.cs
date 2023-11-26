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
    public partial class DiaplayWork : UserControl
    {
        public event EventHandler DeleteButtonClicked;
        public DiaplayWork()
        {
            InitializeComponent();
        }

        #region Properties
        private string comname;
        private string comdes;
        private string comcontact;
        private string country;
        private string state;
        private string street;
        private string role;
        private string duration;
        private string id;
        private string proid;
        private string comid;
        private string adid;
        private string startdate;


        [Category("Custom props")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [Category("Custom props")]
        public string Proid
        {
            get { return proid; }
            set { proid = value; }
        }
        [Category("Custom props")]
        public string Comid
        {
            get { return comid; }
            set { comid = value; }
        }
        [Category("Custom props")]
        public string Adid
        {
            get { return adid; }
            set { adid = value; }
        }
        [Category("Custom props")]
        public string Comname
        {
            get { return comname; }
            set { comname = value; label6.Text = value; }
        }
        [Category("Custom props")]
        public string Comdes
        {
            get { return comdes; }
            set { comdes = value; label7.Text = value; }
        }
        [Category("Custom props")]
        public string Comcontact
        {
            get { return comcontact; }
            set { comcontact = value; label16.Text = value; }
        }
    
       
        [Category("Custom props")]
        public string Street
        {
            get { return street; }
            set { street = value; label15.Text += value+"-"; }
        }
        [Category("Custom props")]
        public string State
        {
            get { return state; }
            set { state = value; label15.Text += value + "-"; }
        }
        [Category("Custom props")]
        public string Country
        {
            get { return country; }
            set { country = value; label15.Text = value + "-"; }
        }


        [Category("Custom props")]
        public string Role
        {
            get { return role; }
            set { role = value; label10.Text = value; }
        }
        [Category("Custom props")]
        public string Duration
        {
            get { return duration; }
            set { duration = value; label2.Text = value; }
        }
        [Category("Custom props")]
        public string Start
        {
            get { return startdate; }
            set { startdate = value; label8.Text = value; }
        }

        #endregion
        private void DiaplayWork_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            WorkDL w = new WorkDL();
            WorkBL wo = w.GetWorkById(int.Parse(Id));
            if(wo.Role!=textBox5.Text||wo.Duration!=(float)Convert.ToDecimal(textBox3.Text)|| wo.Start!=dateTimePicker1.Value)
            {
                if(w.UpdateWork(int.Parse(Id),textBox5.Text,dateTimePicker1.Value, (float)Convert.ToDecimal(textBox3.Text),out string er))
                {
                    MessageBox.Show("Updated Work Succesfully!!!");
                }
                else
                {
                    MessageBox.Show("Error in Work Updating");
                }

            }
            AddressDL a = new AddressDL();
            List<AddressBL> adl = a.LoadAddresses();
            CompanyDL c = new CompanyDL();
            CompanyBL co = c.GetCompanyById(int.Parse(Comid));
            if (co.Name != textBox1.Text || textBox2.Text != co.Description || co.Contact != textBox8.Text)
            {
                if (c.UpdateCompany(co.Id, textBox1.Text, textBox2.Text, textBox5.Text, out String er))
                {
                    MessageBox.Show("Updated Company Succesfully!!!");
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
                    if (ade.StreetNo != textBox7.Text || ade.State != textBox6.Text || ade.Country != textBox4.Text)
                    {
                        if (a.UpdateAddress(co.Addressid, textBox4.Text, textBox6.Text, textBox7.Text, out string errrr))
                        {
                            MessageBox.Show("Updated Address Successfully!!!");
                        }
                        else
                        {
                            MessageBox.Show("Enter Unique Address");
                        }
                    }
                }
            }
            label6.Text = textBox1.Text ;
           label7.Text = textBox2.Text ;
            label16.Text = textBox8.Text;
           label8.Text =dateTimePicker1.Value.ToShortDateString() ;
           label2.Text = textBox3.Text ;
           label10.Text = textBox5.Text ;
            label15.Text = textBox4.Text + "-" + textBox6.Text + "-" + textBox7.Text+"-";
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            dateTimePicker1.Visible = false;
            button3.Visible = false;


            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label15.Visible = true;
            label16.Visible = true;
            label10.Visible = true;
            label2.Visible = true;
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
            label15.Visible = false;
            label16.Visible = false;
            label10.Visible = false;
            label2.Visible = false;

            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = true;
            textBox7.Visible = true;
            textBox8.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            dateTimePicker1.Visible = true;
            button3.Visible = true;

            textBox1.Text = label6.Text;
            textBox2.Text = label7.Text;
            textBox8.Text = label16.Text;
            dateTimePicker1.Value = Convert.ToDateTime(label8.Text);
            textBox3.Text = label2.Text;
            textBox5.Text = label10.Text;
            string[] words = label15.Text.Split('-');
            textBox4.Text = words[0];
            textBox6.Text = words[1];
            textBox7.Text = words[2];
        }
    }
}
