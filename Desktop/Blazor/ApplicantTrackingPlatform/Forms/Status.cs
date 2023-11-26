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
    public partial class Status : Form
    {
        private int pid;
        private int aid;
        private string type;
        private Form activeForm = null;
        public Status(int pid, int aid, string type)
        {
            InitializeComponent();
            this.pid = pid;
            this.aid = aid;
            this.type = type;
            // In the form constructor or initialization method

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
        private void ProjectDataShow()
        {
            DataTable dt = new DataTable();

            // Add columns to the DataTable
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("ProjectName", typeof(string));
            dt.Columns.Add("ProjectDescription", typeof(string));
            dt.Columns.Add("Start Date", typeof(DateTime));
            dt.Columns.Add("End Date", typeof(DateTime));
            dt.Columns.Add("Skills", typeof(string));

            ApplicantDL app = new ApplicantDL();
            ApplicantBL appbl = app.GetApplicantbyId(pid);
            ProjectDL j = new ProjectDL();
            List<ProjectBL> jl = j.Projectlist;
            ProjectSkillDL js = new ProjectSkillDL();
            List<ProjectSkillBL> jsl = js.ProjectSkilllist;
            ProjectApplicantDL jbap = new ProjectApplicantDL();
            List<ProjectApplicantBL> jbapl = jbap.ProjectApplicantlist;

            foreach (ProjectApplicantBL jbapb in jbapl)
            {
                foreach (ProjectBL jb in jl)
                {
                    if (jb.Id == jbapb.Projectid && jbapb.Profileid == appbl.Id)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Id"] = jb.Id;
                        dr["ProjectName"] = jb.Title;
                        dr["ProjectDescription"] = jb.Description;
                        dr["Start Date"] = jb.Start;
                        dr["End Date"] = jb.End;
                      

                        string skill = ""; // Initialize the variable
                        foreach (ProjectSkillBL jsa in jsl)
                        {
                            if (jsa.Projectid1 == jb.Id)
                            {
                                skill += jsa.Name + ", "; // Concatenate skill names with a comma and space
                            }
                        }
                        dr["Skills"] = skill.TrimEnd(',', ' '); // Remove the trailing comma and space

                        // Set background color based on status
                        int status = jbapb.Statusid; // Assuming the status is stored in the "Statusid" property
                        dr["Status"] = status;

                        // Set background color based on status
                        if (status == 1)
                        {
                            dr["Status"] = "Pending";
                        }
                        else if (status == 2)
                        {
                            dr["Status"] = "Accepted";
                        }
                        else if (status == 3)
                        {
                            dr["Status"] = "Rejected";
                        }
                        // Add more cases for other statuses if needed

                        dt.Rows.Add(dr);
                        IncreaseRowHeight();
                    }
                }
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            dataGridView1.Columns["Id"].Visible = false;
        }
        private void JobDataShow()
        {
            DataTable dt = new DataTable();

            // Add columns to the DataTable
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Status", typeof(string));
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

            foreach (JobApplicantBL jbapb in jbapl)
            {
                foreach (JobBL jb in jl)
                {
                    if (jb.Id == jbapb.Jobid && jbapb.Profileid == appbl.Id)
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

                        // Set background color based on status
                        int status = jbapb.Statusid; // Assuming the status is stored in the "Statusid" property
                        dr["Status"] = status;

                        // Set background color based on status
                        if (status == 1)
                        {
                            dr["Status"] = "Pending";
                        }
                        else if (status == 2)
                        {
                            dr["Status"] = "Accepted";
                        }
                        else if (status == 3)
                        {
                            dr["Status"] = "Rejected";
                        }
                        // Add more cases for other statuses if needed

                        dt.Rows.Add(dr);
                        IncreaseRowHeight();
                    }
                }
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            dataGridView1.Columns["Id"].Visible = false;
        }

        private void Status_Load(object sender, EventArgs e)
        {


            if (type=="Job")
            {
                JobDataShow();
            }
            else if(type=="Project")
            {
                ProjectDataShow();
            }
            

            AutoSizeDataGridView();
        }
        private void AutoSizeDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the current column is the "Status" column (replace "Status" with your actual column name)
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Status" && e.RowIndex >= 0)
            {
                // Get the status value from the underlying data source
                //int status = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Status"].Value);
                string status = dataGridView1.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                // Set background color based on status
                if (status == "Pending")
                {
                    e.CellStyle.BackColor = Color.Blue;
                    e.CellStyle.ForeColor = Color.White;
                }
                else if (status == "Accepted")
                {
                    e.CellStyle.BackColor = Color.Green;
                    e.CellStyle.ForeColor = Color.White;
                }
                else if (status == "Rejected")
                {
                    e.CellStyle.BackColor = Color.Red;
                    e.CellStyle.ForeColor = Color.White;
                }
                // Add more cases for other statuses if needed

            }
        }
        private void IncreaseRowHeight()
        {
            // Set the height for the row template
            dataGridView1.RowTemplate.Height = 2 * dataGridView1.RowTemplate.Height;

            // Apply the increased height to existing rows
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 2 * row.Height;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_BackgroundColorChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Height = 2 * dataGridView1.Rows[e.RowIndex].Height;
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

        private void txtSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (type == "Job")
            {
                ApplicantDL m = new ApplicantDL();
                ApplicantBL ma = m.GetApplicantbyId(pid);
                if (ma != null)
                {
                    aid = ma.Id;
                    OpenChildForm(new ApplyJob(pid, aid));
                }
            }
            else if(type=="Project")
            {
                ApplicantDL m = new ApplicantDL();
                ApplicantBL ma = m.GetApplicantbyId(pid);
                if (ma != null)
                {
                    aid = ma.Id;
                    if (ma.Isfreelancer)
                    {
                        OpenChildForm(new ApplyProject(pid, aid));
                    }
                  

                }
            }
        }
    }
}