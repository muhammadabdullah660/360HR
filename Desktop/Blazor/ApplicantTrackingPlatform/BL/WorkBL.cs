using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
     class WorkBL
    {
        private int id;
        private int companyid;
        private string role;
        private float duration;
        private DateTime start;
        private int profileid;

        public int Id { get => id; set => id = value; }
        public int Companyid { get => companyid; set => companyid = value; }
        public string Role { get => role; set => role = value; }
        public float Duration { get => duration; set => duration = value; }
        public DateTime Start { get => start; set => start = value; }
        public int Profileid { get => profileid; set => profileid = value; }

        public WorkBL()
        {

        }
        public WorkBL(int companyid, string role, float duration, DateTime start, int profileid)
        {
            this.Companyid = companyid;
            this.Role = role;
            this.Duration = duration;
            this.Start = start;
            this.Profileid = profileid;
        }
        public WorkBL(int id, int companyid, string role, float duration, DateTime start, int profileid) : this(companyid, role, duration, start, profileid)
        {
            this.Id = id;
        }
    }
}
