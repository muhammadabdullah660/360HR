using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class ProjectApplicantBL
    {
        private int id;
        private int profileid;
        private int projectid;
        private int statusid;
        private float rate;

        public int Id { get => id; set => id = value; }
        public int Profileid { get => profileid; set => profileid = value; }
        public int Projectid { get => projectid; set => projectid = value; }
        public int Statusid { get => statusid; set => statusid = value; }
        public float Rate { get => rate; set => rate = value; }

        public ProjectApplicantBL()
        {

        }

        public ProjectApplicantBL( int profileid, int projectid, int statusid, float rate)
        {
            this.Profileid = profileid;
            this.Projectid = projectid;
            this.Statusid = statusid;
            this.Rate = rate;
           
        }
        public ProjectApplicantBL(int id,int profileid, int projectid, int statusid, float rate):this(profileid,projectid,statusid,rate)
        {
            this.Id = id;
        }
    }
}
