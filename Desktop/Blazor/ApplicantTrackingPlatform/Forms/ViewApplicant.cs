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
    public partial class ViewApplicant : Form
    {
        private int pid;
        private int mid;

        private Form activeForm = null;
        public ViewApplicant(int pid, int mid)
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
        public void LoadData()
        {
            DataTable dt = new DataTable();

            // Add columns to the DataTable
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("ApplicantName", typeof(string));
            dt.Columns.Add("ApplicantEmail", typeof(string));
            dt.Columns.Add("JobName", typeof(string));
            dt.Columns.Add("JobDescription", typeof(string));
            dt.Columns.Add("CompanyName", typeof(string));
            dt.Columns.Add("CompanyDescription", typeof(string));
            dt.Columns.Add("CompanyContact", typeof(string));
            dt.Columns.Add("JobSkills", typeof(string));
            dt.Columns.Add("ApplicantSkills", typeof(string));

            JobDL j = new JobDL();
            ApplicantDL ad = new ApplicantDL();
            JobSkillDL js = new JobSkillDL();
            SkillDL ask=new SkillDL();
            JobApplicantDL jobapp = new JobApplicantDL();
            CompanyDL cl = new CompanyDL();
            ManagerDL m = new ManagerDL();
            foreach(JobBL jb in j.GetAllJob())
            {
                
                foreach (JobApplicantBL jba in jobapp.GetAllJobApplicant())
                {

                    if (jb.Recid == mid && jb.Id==jba.Jobid && jba.Statusid==1)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Id"] = jba.Id;
                        dr["JobName"] = jb.Title;
                        dr["JobDescription"] = jb.Description;

                        foreach (CompanyBL co in cl.GetAllCompany())
                        {
                            if (co.Id == jb.Companyid)
                            {
                                dr["CompanyName"] = co.Name;
                                dr["CompanyDescription"] = co.Description;
                                dr["CompanyContact"] = co.Contact;
                            }
                        }
                        foreach(ApplicantBL ma in ad.GetAllApplicant())
                        {
                            if(ma.Id==jba.Profileid)
                            {
                                PersonDL p = new PersonDL();
                                PersonBL pe = p.GetPersonById(ma.Personid);
                                dr["ApplicantName"] = pe.Firstname + " " + pe.Lastname;
                                dr["ApplicantEmail"] = pe.Email;
                            }
                        }
                        string skill = ""; // Initialize the variable
                        foreach (JobSkillBL jsa in js.GetAllJobSkill())
                        {
                            if (jsa.Jobid1 == jb.Id)
                            {
                                skill += jsa.Name + ", "; // Concatenate skill names with a comma and space
                            }
                        }
                        dr["JobSkills"] = skill.TrimEnd(',', ' '); // Remove the trailing comma and space


                        string skills = ""; // Initialize the variable
                        foreach (SkillBL jsa in ask.GetAllSkill())
                        {
                            if (jsa.Proid == jba.Profileid)
                            {
                                skills += jsa.Des + ", "; // Concatenate skill names with a comma and space
                            }
                        }
                        dr["ApplicantSkills"] = skills.TrimEnd(',', ' '); // Remove the trailing comma and space

                        dt.Rows.Add(dr);

                    }

                }
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            dataGridView1.Columns["Id"].Visible = false;
        }
        private void ViewApplicant_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int jobId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {

                OpenChildForm(new AddInterview(pid, mid, jobId));

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
