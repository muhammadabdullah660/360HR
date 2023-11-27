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
    public partial class AcceptProject : Form
    {
        private int pid;
        private int mid;
        public AcceptProject(int pid, int mid)
        {
            InitializeComponent();
            this.pid = pid;
            this.mid = mid;
        }
        public void LoadData()
        {
            DataTable dt = new DataTable();

            // Add columns to the DataTable
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("ApplicantName", typeof(string));
            dt.Columns.Add("ApplicantEmail", typeof(string));
            dt.Columns.Add("ProjectName", typeof(string));
            dt.Columns.Add("ProjectDescription", typeof(string));
            dt.Columns.Add("Rate", typeof(string));
            dt.Columns.Add("ProjectSkills", typeof(string));
            dt.Columns.Add("ApplicantSkills", typeof(string));

            ProjectDL j = new ProjectDL();
            ApplicantDL ad = new ApplicantDL();
            ProjectSkillDL js = new ProjectSkillDL();
            SkillDL ask = new SkillDL();
            ProjectApplicantDL jobapp = new ProjectApplicantDL();
            ManagerDL m = new ManagerDL();
            foreach (ProjectBL jb in j.GetAllProject())
            {

                foreach (ProjectApplicantBL jba in jobapp.GetAllProjectApplicant())
                {

                    if (jb.Recid == mid && jb.Id == jba.Projectid && jba.Statusid == 1)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Id"] = jba.Id;
                        dr["ProjectName"] = jb.Title;
                        dr["ProjectDescription"] = jb.Description;
                        dr["Rate"] = jba.Rate;
                        
                        foreach (ApplicantBL ma in ad.GetAllApplicant())
                        {
                            if (ma.Id == jba.Profileid)
                            {
                                PersonDL p = new PersonDL();
                                PersonBL pe = p.GetPersonById(ma.Personid);
                                dr["ApplicantName"] = pe.Firstname + " " + pe.Lastname;
                                dr["ApplicantEmail"] = pe.Email;
                            }
                        }
                        string skill = ""; // Initialize the variable
                        foreach (ProjectSkillBL jsa in js.GetAllProjectSkill())
                        {
                            if (jsa.Projectid1 == jb.Id)
                            {
                                skill += jsa.Name + ", "; // Concatenate skill names with a comma and space
                            }
                        }
                        dr["ProjectSkills"] = skill.TrimEnd(',', ' '); // Remove the trailing comma and space


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
        private void AcceptProject_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int jobId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                ProjectApplicantDL pr = new ProjectApplicantDL();

                // Update status to 2
                if (pr.UpdateProjectApplicant(jobId, 2, out string err))
                {
                    MessageBox.Show("Accepted");
                    ProjectApplicantBL prp= pr.GetProjectApplicantById(jobId);
                    // If the update to status 2 was successful, update all applicants with status 1 to status 3
                    foreach (ProjectApplicantBL prb in pr.GetAllProjectApplicant())
                    {
                        if (prb.Projectid == prp.Projectid && prb.Statusid == 1 && prb.Id!=jobId)
                        {
                            if (pr.UpdateProjectApplicant(prb.Id, 3, out string erre))
                            {
                                // Additional logic if needed
                            }
                            else
                            {
                                // Handle the case where the update to status 3 fails
                                MessageBox.Show($"Failed to update to status 3: {erre}");
                            }
                        }
                    }
                }
                else
                {
                    // Handle the case where the update to status 2 fails
                    MessageBox.Show($"Failed to update to status 2: {err}");
                }
            }
            LoadData();

        }
    }
}
