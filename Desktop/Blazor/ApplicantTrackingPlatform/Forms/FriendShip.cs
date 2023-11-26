using ApplicantTrackingPlatform.DL;
using ApplicantTrackingPlatform.BL;
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
    public partial class FriendShip : Form
    {
        private int pid;
        private int aid;
        public FriendShip(int pid, int aid)
        {
            InitializeComponent();
            this.pid = pid;
            this.aid = aid;
        }

        private void LoadData()
        {
            List<int> sentRequests = new List<int>();
            List<int> receivedRequests = new List<int>();
            List<string> namesToShow = new List<string>();

            // Get a list of user IDs to whom the current user has sent friend requests
            FrndDL f = new FrndDL();
            List<FrndBL> sentRequestsList = f.GetSentRequests(aid);
            sentRequests.AddRange(sentRequestsList.Select(fr => fr.Receiverid));

            List<FrndBL> receivedRequestsList = f.GetReceivedRequests(aid);
            receivedRequests.AddRange(receivedRequestsList.Select(fr => fr.Senderid));

            // Get a list of all applicants
            ApplicantDL a = new ApplicantDL();
            List<ApplicantBL> applicantsList = a.Applicantlist;

            // Get the current user's details
            PersonDL p = new PersonDL();
            PersonBL currentUser = p.GetPersonById(pid);

            // Add names to the list that meet the conditions
            foreach (ApplicantBL applicant in applicantsList)
            {
                // Check if the applicant is not the current user and not in the sentRequests list
                if (applicant.Id != aid && !sentRequests.Contains(applicant.Id)&& !receivedRequests.Contains(applicant.Id))
                {
                    PersonBL pe= p.GetPersonById(applicant.Personid);
                    // Assuming you want to display the name (Email property) in the ComboBox
                    namesToShow.Add(pe.Email);
                }
            }

            // Bind the list of names to the ComboBox
            comboBox1.DataSource = namesToShow;
        }

        private void gridviewdata()
        {
            DataTable dt = new DataTable();

            // Add columns to the DataTable
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Skills", typeof(string));
            int reid=-1;
            FrndDL f=new FrndDL();
            List<FrndBL> fl=f.GetAllFrnd();
            
            foreach (FrndBL fb in fl)
            {
               
                if (fb.Senderid == aid)
                {
                    DataRow dr = dt.NewRow();

                    ApplicantDL a = new ApplicantDL();
                    foreach (ApplicantBL ap in a.GetAllApplicant())
                    {
                        if (ap.Id == fb.Receiverid)
                        {
                            reid = ap.Personid;
                        }
                    }
                    PersonDL p = new PersonDL();
                    PersonBL pe = p.GetPersonById(reid);
                    dr["Name"] = pe.Firstname + " " + pe.Lastname;
                    dr["Email"] = pe.Email;
                    string skill = "";
                    // Initialize the variable
                    SkillDL s = new SkillDL();
                    foreach (SkillBL jsa in s.GetAllSkill())
                    {
                        if (jsa.Proid == fb.Receiverid)
                        {
                            skill += jsa.Des + ", "; // Concatenate skill names with a comma and space
                        }
                    }
                    dr["Skills"] = skill.TrimEnd(',', ' '); // Remove the trailing comma and space

                    // Set background color based on status
                    int status = fb.Status; // Assuming the status is stored in the "Statusid" property
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
                }
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
        }

        private void flowdata()
        {
           
            FrndDL ed = new FrndDL();
            List<FrndBL> edl = ed.Frndlist;
            flowLayoutPanel1.Controls.Clear();
            foreach (FrndBL eb in edl)
            {
                if (eb.Receiverid == aid)
                {
                    Displayfrnds de = new Displayfrnds();
                    de.Id = eb.Id.ToString();
                    de.Senid = eb.Senderid.ToString();
                    de.Recid = eb.Receiverid.ToString();
                    de.Statusid = eb.Status.ToString();
                    flowLayoutPanel1.Controls.Add(de);

                }

            }
        }

        private void FriendShip_Load(object sender, EventArgs e)
        {

            LoadData();
            gridviewdata();
            flowdata();
            PersonDL p = new PersonDL();
            PersonBL pe = p.GetPersonById(pid);
            label11.Text = pe.Email;
            label10.Text = pe.Firstname + " " + pe.Lastname;
            label9.Text = pe.MobileNumber;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int re = -1;
            if(comboBox1.SelectedIndex!=-1)
            {
                PersonDL personDL = new PersonDL();
                foreach(PersonBL p in personDL.GetAllPersons())
                {
                    if(p.Email==comboBox1.Text)
                    {
                        re = p.Id;
                    }
                }
                ApplicantDL a = new ApplicantDL();
                ApplicantBL ap = a.GetApplicantbyId(re);
                FrndDL f = new FrndDL();
                FrndBL fa=new FrndBL(aid,ap.Id,1,DateTime.Now);
                if(f.InsertFrnd(fa,out string er))
                {
                    MessageBox.Show("Request Sent Successfully!!");
                }
                else
                {
                    MessageBox.Show("Error while Sending");
                }
            }
            LoadData();
            gridviewdata();
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

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
