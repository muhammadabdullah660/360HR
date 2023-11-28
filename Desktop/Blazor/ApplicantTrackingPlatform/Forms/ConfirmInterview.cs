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
    public partial class ConfirmInterview : Form
    {
        private int pid;
        private int aid;
        private int jid;
        public ConfirmInterview(int pid, int aid)
        {
            InitializeComponent();
            this.pid = pid;
            this.aid = aid;
        }
        public void LoadData()
        {
            DataTable dt = new DataTable();

            // Add columns to the DataTable
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("JobName", typeof(string));
            dt.Columns.Add("JobDescription", typeof(string));
            dt.Columns.Add("CompanyName", typeof(string));
            dt.Columns.Add("CompanyDescription", typeof(string));
            dt.Columns.Add("CompanyContact", typeof(string));
            dt.Columns.Add("Skills", typeof(string));
            dt.Columns.Add("Address", typeof(string));

            CompanyDL c = new CompanyDL();
            List<CompanyBL> cl = c.Companylist;
            JobDL j = new JobDL();
            List<JobBL> jl = j.Joblist;
            AddressDL a = new AddressDL();
            List<AddressBL> al = a.LoadAddresses();
            JobSkillDL js = new JobSkillDL();
            List<JobSkillBL> jsl = js.JobSkilllist;
            JobApplicantDL japp = new JobApplicantDL();
            InterviewFeedbackDL ife = new InterviewFeedbackDL();

            foreach (JobApplicantBL jbs in japp.GetAllJobApplicant())
            {
                // Check if the job application has status 2
                if (jbs.Profileid == aid && jbs.Statusid == 2)
                {
                    bool hasFeedback = false;

                    // Check if there is feedback for the current job application
                    foreach (InterviewFeedbackBL ifsb in ife.GetAllInterviewFeedback())
                    {
                        if (ifsb.Jid == jbs.Id)
                        {
                            hasFeedback = true;
                            break;
                        }
                    }

                    // If there is no feedback, proceed to add the job details to the DataTable
                    if (!hasFeedback)
                    {

                        foreach (JobBL jb in jl)
                        {
                            if (jb.Id == jbs.Jobid)
                            {
                                DataRow dr = dt.NewRow();
                                dr["Id"] = jbs.Id;
                                dr["JobName"] = jb.Title;
                                dr["JobDescription"] = jb.Description;

                                foreach (CompanyBL co in cl)
                                {
                                    if (co.Id == jb.Companyid)
                                    {
                                        dr["CompanyName"] = co.Name;
                                        dr["CompanyDescription"] = co.Description;
                                        dr["CompanyContact"] = co.Contact;

                                        foreach (AddressBL ad in al)
                                        {
                                            if (ad.Id == co.Addressid)
                                            {
                                                dr["Address"] = ad.StreetNo + " " + ad.State + " " + ad.Country;
                                            }
                                        }
                                    }
                                }

                                string skill = "";
                                foreach (JobSkillBL jsa in jsl)
                                {
                                    if (jsa.Jobid1 == jb.Id)
                                    {
                                        skill += jsa.Name + ", ";
                                    }
                                }
                                dr["Skills"] = skill.TrimEnd(',', ' ');

                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            dataGridView1.Columns["Id"].Visible = false;
        }
        private void ConfirmInterview_Load(object sender, EventArgs e)
        {
            LoadData();
            PersonDL p = new PersonDL();
            PersonBL pe = p.GetPersonById(pid);
            label11.Text = pe.Email;
            label10.Text = pe.Firstname + " " + pe.Lastname;
            label9.Text = pe.MobileNumber;
            comboBox1.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int jobId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
            this.jid = jobId;
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                comboBox1.Enabled = true;
                comboBox1.Text = "";
                comboBox1.Items.Clear();
                InterviewSlotDL iss = new InterviewSlotDL();
                foreach(InterviewSlotBL issb in iss.GetAllInterviewSlot())
                {
                    if(issb.Jobid==jobId)
                    {
                        comboBox1.Items.Add(issb.Slot);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                string mes = "I select the " + comboBox1.Text + " for the interview. thank your consideration";
                InterviewFeedbackDL ife = new InterviewFeedbackDL();
                InterviewFeedbackBL ifsb = new InterviewFeedbackBL(jid, DateTime.Now, mes);
                if (ife.InsertInterviewFeedback(ifsb, out int id, out string er) != -1)
                {
                    MessageBox.Show("Sucessfully sent!!");
                    comboBox1.Enabled = false;
                    comboBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Error");
                }
                LoadData();
            }
        }
    }
}
