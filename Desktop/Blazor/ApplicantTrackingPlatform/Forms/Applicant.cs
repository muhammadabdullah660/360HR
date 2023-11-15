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
    public partial class Applicant : Form
    {
        private int pid;
        private int aid;
        private Form activeForm = null;
        public Applicant(int pid,int aid)
        {
            this.pid = pid;
            this.aid = aid;
            InitializeComponent();
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
        private void button2_Click(object sender, EventArgs e)
        {

            var openHomeForms = Application.OpenForms.OfType<Home>().ToList();

            // Iterate over the list and close each form.
            foreach (Home form in openHomeForms)
            {
                form.Hide();
            }
            Home m = new Home("", -1);
            m.Show();
        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Applicant_Load(object sender, EventArgs e)
        {
            ApplicantDL m = new ApplicantDL();
            ApplicantBL ma = m.GetApplicantbyId(pid);
            if (ma != null)
            {
                this.aid = ma.Id;
               
                //  OpenChildForm(new ManagerProfile(pid, co.Name, co.Description, co.Contact, ad.StreetNo, ad.State, ad.Country));
                OpenChildForm(new ApplicantProfile(pid, aid));

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("First Enter Information!!");
                OpenChildForm(new ApplicantProfile(pid, -1));

            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ApplicantDL m = new ApplicantDL();
            ApplicantBL ma = m.GetApplicantbyId(pid);
            if (ma != null)
            {
                this.aid = ma.Id;

                //  OpenChildForm(new ManagerProfile(pid, co.Name, co.Description, co.Contact, ad.StreetNo, ad.State, ad.Country));
                OpenChildForm(new ApplicantProfile(pid, aid));

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("First Enter Information!!");
                OpenChildForm(new ApplicantProfile(pid, -1));

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplicantDL m = new ApplicantDL();
            ApplicantBL ma = m.GetApplicantbyId(pid);
            if (ma != null)
            {
                aid = ma.Id;
                OpenChildForm(new ApplyJob(pid, aid));
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("First Complete Profile!!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ApplicantDL m = new ApplicantDL();
            ApplicantBL ma = m.GetApplicantbyId(pid);
            if (ma != null)
            {
                aid = ma.Id;
                if(ma.Isfreelancer)
                {
                    OpenChildForm(new ApplyProject(pid, aid));
                }
                else
                {
                    MessageBox.Show("This is only for freelancer!!");
                }
               
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("First Complete Profile!!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }
    }
}
