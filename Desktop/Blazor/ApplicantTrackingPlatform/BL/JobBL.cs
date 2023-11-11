using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class JobBL
    {
        private int id;
        private int companyid;
        private int managerid;
        private string title;
        private string description;

        public int Id { get => id; set => id = value; }
        public int Companyid { get => companyid; set => companyid = value; }
        public int Managerid { get => managerid; set => managerid = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }

        public JobBL(int companyid, int managerid, string title, string description)
        {
            this.Companyid = companyid;
            this.Managerid = managerid;
            this.Title = title;
            this.Description = description;
           
        }
        public JobBL() { }

        public JobBL(int id, int companyid, int managerid, string title, string description):this(companyid,managerid,title,description)
        {
            this.Id = id;
            
        }
    }
}
