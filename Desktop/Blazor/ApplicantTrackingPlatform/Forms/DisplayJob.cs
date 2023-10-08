using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicantTrackingPlatform.DL;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class DisplayJob : UserControl
    {
        public DisplayJob()
        {
            InitializeComponent();
        }
        #region Properties
        private string name;
        private string description;
        private string country;
        [Category("Custom props")]
        public string Name1
        {
            get { return name; }
            set { name = value; lbltitle.Text = value; }
        }
        [Category("Custom props")]
        public string Description 
        { get { return description; }
            set { description = value; txtdes.Text = value; }        }
        [Category("Custom props")]
        public string Country 
        {
            get {return  country; }
            set { country = value; lblcom.Text = value; }
        }
        #endregion


    }
}
