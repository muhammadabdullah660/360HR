using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicantTrackingPlatform.DL;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class DisplayJob : UserControl
    {
        public DisplayJob()
        {
            InitializeComponent();
        }
        #region Properties
        private string companyANme;
        private string companydescription;
        private string comapnyContact;
        private string jobName;
        private string jobDescription;

        [Category("Custom props")]
        public string CompanyANme
        {
            get { return companyANme; }
            set { companyANme = value; lblcom.Text = value; }
        }
        [Category("Custom props")]
        public string Companydescription 
        { get { return companydescription; }
            set { companydescription = value; label1.Text = value; }        }
        [Category("Custom props")]
        public string CompanyContact 
        {
            get {return comapnyContact; }
            set { comapnyContact = value; label2.Text = value; }
        }
        [Category("Custom props")]
        public string JobName
        {
            get { return jobName; }
            set { jobName = value; lbltitle.Text = value; }
        }
        [Category("Custom props")]
        public string JobDescription
        {
            get { return jobDescription; }
            set { jobDescription = value; txtdes.Text = value; }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            SignIn im = new SignIn();
            im.ShowDialog();
        }

        private void txtdes_Click(object sender, EventArgs e)
        {

        }

        private void DisplayJob_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lbltitle_Click(object sender, EventArgs e)
        {

        }
    }
}
