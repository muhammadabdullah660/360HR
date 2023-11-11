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
    public partial class ViewProject : Form
    {
        private int mid;
        private int pid;
        private Form activeForm = null;
        public ViewProject(int mid, int pid)
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
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("Skills", typeof(string));

          
            ProjectDL j = new ProjectDL();
            List<ProjectBL> jl = j.Projectlist;
            ProjectSkillDL js = new ProjectSkillDL();
            List<ProjectSkillBL> jsl = js.ProjectSkilllist;

            foreach (ProjectBL jb in jl)
            {
                if (jb.Managerid == mid)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = jb.Id;

                    dr["Name"] = jb.Title;
                    dr["Description"] = jb.Description;
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
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ViewProject_Load(object sender, EventArgs e)
        {
            DataShow();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AddProject(pid, mid, -1));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int jobId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                ProjectDL j = new ProjectDL();
                string err = "";
                if (j.DeleteProject(jobId, out err))
                {
                    MessageBox.Show("Delete Successfully");
                    DataShow();
                }
                else
                {
                    MessageBox.Show(err);
                }


            }
            else if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                MessageBox.Show("Edit");
                OpenChildForm(new AddProject(pid, mid, jobId));
            }
        }
    }
}
