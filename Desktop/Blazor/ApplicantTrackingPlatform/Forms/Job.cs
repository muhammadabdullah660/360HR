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
    public partial class Job : Form
    {
        List<AddressBL> li = new List<AddressBL>();
        public Job()
        {
            InitializeComponent();
        }

        private void Job_Load(object sender, EventArgs e)
        {
            AddressDL a = new AddressDL();
            li = a.LoadAddresses();
            foreach (var ad in li)
            {
                DisplayJob d = new DisplayJob();
                d.Name1 = ad.StreetNo;
                d.Description = ad.State;
                d.Country = ad.Country;



                flowLayoutPanel1.Controls.Add(d);



            }
        }
    }
}
