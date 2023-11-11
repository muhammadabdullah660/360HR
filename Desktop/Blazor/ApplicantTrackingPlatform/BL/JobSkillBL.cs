using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class JobSkillBL
    {
        private int Jobid;
        private string name;

        public int Jobid1 { get => Jobid; set => Jobid = value; }
        public string Name { get => name; set => name = value; }

        public JobSkillBL()
        {

        }

        public JobSkillBL(int jobid, string name)
        {
            this.Jobid1 = jobid;
            this.Name = name;
        }
    }
}
