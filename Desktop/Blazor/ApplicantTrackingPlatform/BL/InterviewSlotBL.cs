using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class InterviewSlotBL
    {
        private int jobid;
        private string slot;
        public int Jobid { get => jobid; set => jobid = value; }
        public string Slot { get => slot; set => slot = value; }

        public InterviewSlotBL()
        {

        }

        public InterviewSlotBL(int jobid,string slot)
        {
            this.Jobid = jobid;
            this.Slot = slot;
        }
    }
}
