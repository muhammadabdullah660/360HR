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
using ApplicantTrackingPlatform.BL;
using ApplicantTrackingPlatform.DL;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class ViewJob : Form
    {
        private int mid;
        private int pid;
        private Form activeForm = null;
        public ViewJob(int mid, int pid)
        {
            this.mid = mid;
            InitializeComponent();
            this.pid = pid;
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

        private void DataShow()
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

            foreach (JobBL jb in jl)
            {
                if (jb.Managerid == mid)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = jb.Id;

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

                    string skill = ""; // Initialize the variable
                    foreach (JobSkillBL jsa in jsl)
                    {
                        if (jsa.Jobid1 == jb.Id)
                        {
                            skill += jsa.Name + ", "; // Concatenate skill names with a comma and space
                        }
                    }
                    dr["Skills"] = skill.TrimEnd(',', ' '); // Remove the trailing comma and space

                    dt.Rows.Add(dr);
                }
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            dataGridView1.Columns["Id"].Visible = false;
        }

        private void ViewJob_Load(object sender, EventArgs e)
        {
            DataShow();
            AutoSizeDataGridView();
        }
        private void AutoSizeDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void button1_Click(object sender, EventArgs e)
        {
             
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            OpenChildForm(new AddJob(pid, mid,-1));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int jobId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
            if (e.ColumnIndex==0 && e.RowIndex>=0)
            {
                JobDL j = new JobDL();
                string err = "";
                if (j.DeleteJob(jobId, out err))
                {
                    MessageBox.Show("Delete Successfully");
                    DataShow();
                }
                else
                {
                    MessageBox.Show(err);
                }


            }
            else if (e.ColumnIndex==1 && e.RowIndex>=0)
            {
                MessageBox.Show("Edit");
                OpenChildForm(new AddJob(pid, mid, jobId));
            }
        }
    }
}
