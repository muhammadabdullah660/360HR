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
    public partial class Recruiter : Form
    {
        private Form activeForm = null;
        private int pid;
        private int mid;
        public Recruiter(int pid, int mid)
        {
            InitializeComponent();
            this.pid = pid;
            this.mid = mid;
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
        private void Recruiter_Load(object sender, EventArgs e)
        {

            ManagerDL m = new ManagerDL();
            ManagerBL ma = m.GetManagerbyId(pid);
            if (ma != null)
            {
                this.mid = ma.Id;
                CompanyDL c = new CompanyDL();
                foreach (CompanyBL co in c.Companylist)
                {
                    if (co.Id == ma.Companyid)
                    {
                        AddressDL a = new AddressDL();

                        List<AddressBL> al = a.LoadAddresses();
                        foreach (AddressBL ad in al)
                        {
                            if (co.Addressid == ad.Id)
                            {
                                //  OpenChildForm(new ManagerProfile(pid, co.Name, co.Description, co.Contact, ad.StreetNo, ad.State, ad.Country));
                                OpenChildForm(new ManagerProfile(pid, ma.Id));
                            }
                        }
                    }
                }

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("First Enter Information!!");
                OpenChildForm(new ManagerProfile(pid, -1));

            }
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
    }
}
