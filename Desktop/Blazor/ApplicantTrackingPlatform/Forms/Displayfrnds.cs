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
    public partial class Displayfrnds : UserControl
    {
        private int pid;
        public Displayfrnds()
        {
            InitializeComponent();
        }
        #region Properties
        private string id;
        private string senid;
        private string recid;
        private string statusid;

        [Category("Custom props")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [Category("Custom props")]
        public string Senid
        {
            get { return senid; }
            set { senid = value;  }
        }
        [Category("Custom props")]
        public string Recid
        {
            get { return recid; }
            set { recid = value;}
        }
        [Category("Custom props")]
        public string Statusid
        {
            get { return statusid; }
            set { statusid = value;}
        }


        #endregion
        private void Displayfrnds_Load(object sender, EventArgs e)
        {
            ApplicantDL a = new ApplicantDL();
            foreach (ApplicantBL ab in a.GetAllApplicant())
            {
                if (ab.Id == int.Parse(Senid))
                {
                    this.pid = ab.Personid;
                }
            }
            PersonDL p = new PersonDL();
            PersonBL pe = p.GetPersonById(pid);
            if (int.Parse(statusid)==1)
            {
                label1.Text = pe.Firstname + " " + pe.Lastname + " wants to add you as a friend from " + pe.Email + " this email id ";

            }
            if (int.Parse(statusid) == 2 )
            {
                label1.Text = "You are now friends with "+pe.Firstname + " " + " and email id is " +  pe.Email +  " you accep this request " ;
                button1.Visible = false;
                button2.Visible = false;

            }
            if(int.Parse(statusid) == 3)
            {
                label1.Text = "You are reject this friend request came from " + pe.Firstname + " " + " and email id is " + pe.Email;
                button1.Visible = false;
                button2.Visible = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplicantDL a = new ApplicantDL();
            foreach (ApplicantBL ab in a.GetAllApplicant())
            {
                if (ab.Id == int.Parse(Recid))
                {
                    this.pid = ab.Personid;
                }
            }
            PersonDL p = new PersonDL();
            PersonBL pe = p.GetPersonById(pid);
            FrndDL f = new FrndDL();
            if(f.UpdatedFrnd(int.Parse(Id),2,out string er))
            {
                MessageBox.Show("Accepted!!");
            }
            else
            {
                MessageBox.Show("Error!!");
            }
            label1.Text = "You are now friends with " + pe.Firstname + " " + " and email id is " + pe.Email + " you accep this request ";
            button1.Visible = false;
            button2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ApplicantDL a = new ApplicantDL();
            foreach (ApplicantBL ab in a.GetAllApplicant())
            {
                if (ab.Id == int.Parse(Recid))
                {
                    this.pid = ab.Personid;
                }
            }
            PersonDL p = new PersonDL();
            PersonBL pe = p.GetPersonById(pid);
            FrndDL f = new FrndDL();
            if (f.UpdatedFrnd(int.Parse(Id), 3, out string er))
            {
                MessageBox.Show("Rejected!!");
            }
            else
            {
                MessageBox.Show("Error!!");
            }
            label1.Text = "You are reject this friend request came from " + pe.Firstname + " " + " and email id is "  + pe.Email;
            button1.Visible = false;
            button2.Visible = false;
        }
    }
}
