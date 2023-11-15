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
using ApplicantTrackingPlatform.BL;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class Home : Form
    {
        private string role;
        private int pid;
        public Home(string role, int pid)
        {
            this.Role = role;
            InitializeComponent();
            this.Pid = pid;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //OpenChildForm(new SignIn());
            SignIn im = new SignIn();
            im.Show();

        }
        private Form activeForm = null;

        public string Role { get => role; set => role = value; }
        public int Pid { get => pid; set => pid = value; }

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

       
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //this.Hide();
            SignUp m = new SignUp();
            m.ShowDialog();
            //OpenChildForm(new HRManagerMenu());
            //OpenChildForm(new SignUp());
        }

        private void Home_Load(object sender, EventArgs e)
        {
            

            if (this.Role == "Applicant")
            {
                ApplicantDL m = new ApplicantDL();
                ApplicantBL ma = m.GetApplicantbyId(Pid);
                if (ma != null)
                {
                    OpenChildForm(new Applicant(pid,ma.Id));
                }
                else
                {
                    OpenChildForm(new Applicant(pid,-1));

                }
                linkLabel1.Enabled = false;
                linkLabel2.Enabled = false;
            }
            else if (this.Role == "Manager")
            {
                ManagerDL m = new ManagerDL();
                ManagerBL ma=m.GetManagerbyId(Pid);
                if(ma!=null)
                {
                    OpenChildForm(new HRManagerMenu(Pid, ma.Id));
                }
                else
                {
                    OpenChildForm(new HRManagerMenu(Pid, -1));

                }
                linkLabel1.Enabled = false;
                linkLabel2.Enabled = false;
               
            }
            else if (this.Role == "Recruiter")
            {
                OpenChildForm(new Recruiter());
            }
            else
            {
                OpenChildForm(new Job());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
