using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
     class ProjectSkillBL
    {
        private int Projectid;
        private string name;

        public int Projectid1 { get => Projectid; set => Projectid = value; }
        public string Name { get => name; set => name = value; }

        public ProjectSkillBL()
        {

        }

        public ProjectSkillBL(int Projectid, string name)
        {
            this.Projectid1 = Projectid;
            this.Name = name;
        }
    }
}
