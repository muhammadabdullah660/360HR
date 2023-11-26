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
    public partial class Job : Form
    {
        List<AddressBL> li = new List<AddressBL>();
        public Job()
        {
            InitializeComponent();
        }

        private void Job_Load(object sender, EventArgs e)
        {

            JobDL j = new JobDL();
            CompanyDL c = new CompanyDL();
            foreach(JobBL jb in j.Joblist)
            {
                
                foreach(CompanyBL co in c.Companylist)
                {
                    if (co.Id == jb.Companyid)
                    {
                        DisplayJob d = new DisplayJob();
                        d.JobName = jb.Title;
                        d.JobDescription = jb.Description;
                        d.CompanyANme = co.Name;
                        d.Companydescription = co.Description;
                        d.CompanyContact = co.Contact;
                        flowLayoutPanel1.Controls.Add(d);
                    }
                }
            
            
           

              



            }
        }

        private void displayJob1_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
