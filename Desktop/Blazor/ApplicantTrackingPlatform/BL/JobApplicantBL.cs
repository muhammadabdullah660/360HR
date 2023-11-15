using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class JobApplicantBL
    {
        private int id;
        private int jobid;
        private int profileid;
        private int statusid;
        private DateTime dateofApply;

        public int Id { get => id; set => id = value; }
        public int Jobid { get => jobid; set => jobid = value; }
        public int Profileid { get => profileid; set => profileid = value; }
        public int Statusid { get => statusid; set => statusid = value; }
        public DateTime DateofApply { get => dateofApply; set => dateofApply = value; }

        public JobApplicantBL()
        {

        }
        public JobApplicantBL(int jobid, int profileid, int statusid, DateTime dateofApply)
        {
            this.Jobid = jobid;
            this.Profileid = profileid;
            this.Statusid = statusid;
            this.DateofApply = dateofApply;
           
        }
        public JobApplicantBL(int id,int jobid, int profileid, int statusid, DateTime dateofApply):this(jobid,profileid,statusid,dateofApply)
        {
            this.Id = id;

        }
    }
}
