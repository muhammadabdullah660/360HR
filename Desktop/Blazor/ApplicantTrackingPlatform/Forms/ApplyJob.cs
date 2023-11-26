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
    public partial class ApplyJob : Form
    {
        private int pid;
        private int aid;
        private Form activeForm = null;
        public ApplyJob(int pid, int aid)
        {
            InitializeComponent();
            this.pid = pid;
            this.aid = aid;
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow)
                {
                    // Skip the new row
                    continue;
                }

                bool rowVisible = false;

                // Clear any selected cells to avoid InvalidOperationException
                dataGridView1.CurrentCell = null;

                // Concatenate all the cell values in the row into a single string for searching
                string rowValue = string.Join(" ", row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value?.ToString()));

                if (rowValue.ToLower().Contains(txtSearch.Text))
                {
                    rowVisible = true;
                }

                row.Visible = rowVisible;
            }
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
            ApplicantDL app = new ApplicantDL();
            ApplicantBL appbl = app.GetApplicantbyId(pid);
            CompanyDL c = new CompanyDL();
            List<CompanyBL> cl = c.Companylist;
            JobDL j = new JobDL();
            List<JobBL> jl = j.Joblist;
            AddressDL a = new AddressDL();
            List<AddressBL> al = a.LoadAddresses();
            JobSkillDL js = new JobSkillDL();
            List<JobSkillBL> jsl = js.JobSkilllist;
            JobApplicantDL jbap = new JobApplicantDL();
            List<JobApplicantBL> jbapl = jbap.JobApplicantlist;

            foreach (JobBL jb in jl)
            {
                
                    if (!jbapl.Any(jbapd => jb.Id == jbapd.Jobid && jbapd.Profileid == appbl.Id))
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
        private void ApplyJob_Load(object sender, EventArgs e)
        {


            DataShow();
            AutoSizeDataGridView();
        }
        private void AutoSizeDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int jobId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                JobApplicantDL ap = new JobApplicantDL();
                DateTime d = DateTime.Now;
                JobApplicantBL ab = new JobApplicantBL(jobId,aid,1,d);
                if(ap.InsertJobApplicant(ab,out int jaid, out string er)!=-1)
                {
                    MessageBox.Show("Apply Successfully!!");

                }
                else
                {
                    MessageBox.Show("Error Occur while applying!!");
                }
               


            }
            DataShow();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ApplicantDL m = new ApplicantDL();
            ApplicantBL ma = m.GetApplicantbyId(pid);
            if (ma != null)
            {
                aid = ma.Id;
                OpenChildForm(new Status(pid, aid,"Job"));
            }
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
