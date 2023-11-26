using ApplicantTrackingPlatform.BL;
using ApplicantTrackingPlatform.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class ApplyProject : Form
    {
        private int pid;
        private int aid;
        private Form activeForm = null;
        public ApplyProject(int pid, int aid)
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
        private void DataShow()
        {
            DataTable dt = new DataTable();

            // Add columns to the DataTable
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("ProjectName", typeof(string));
            dt.Columns.Add("ProjectDescription", typeof(string));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("Skills", typeof(string));
            ApplicantDL app = new ApplicantDL();
            ApplicantBL appbl = app.GetApplicantbyId(pid);
            
            ProjectDL j = new ProjectDL();
            List<ProjectBL> jl = j.Projectlist;
            ProjectSkillDL js = new ProjectSkillDL();
            List<ProjectSkillBL> jsl = js.ProjectSkilllist;
            ProjectApplicantDL jbap = new ProjectApplicantDL();
            List<ProjectApplicantBL> jbapl = jbap.ProjectApplicantlist;

            foreach (ProjectBL jb in jl)
            {

                if (!jbapl.Any(jbapd => jb.Id == jbapd.Projectid && jbapd.Profileid == appbl.Id))
                {

                    DataRow dr = dt.NewRow();
                    dr["Id"] = jb.Id;

                    dr["ProjectName"] = jb.Title;
                    dr["ProjectDescription"] = jb.Description;
                    dr["StartDate"] = jb.Start;
                    dr["EndDate"] = jb.End;

                   

                    string skill = ""; // Initialize the variable
                    foreach (ProjectSkillBL jsa in jsl)
                    {
                        if (jsa.Projectid1 == jb.Id)
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
        private void ApplyProject_Load(object sender, EventArgs e)
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
                OpenChildForm(new AddApplyProject(pid,aid,jobId));



            }
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

            ApplicantDL m = new ApplicantDL();
            ApplicantBL ma = m.GetApplicantbyId(pid);
            if (ma != null)
            {
                aid = ma.Id;
                OpenChildForm(new Status(pid, aid,"Project"));
            }
           
        }
    }
}
