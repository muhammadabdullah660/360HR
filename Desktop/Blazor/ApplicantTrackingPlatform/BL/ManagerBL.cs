using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class ManagerBL
    {
        private int id;
        private int personid;
        private int companyid;
        private bool isManager;

        public int Id { get => id; set => id = value; }
        public int Personid { get => personid; set => personid = value; }
        public int Companyid { get => companyid; set => companyid = value; }
        public bool IsManager { get => isManager; set => isManager = value; }

        public ManagerBL(int personid, int companyid,bool isManager)
        {
          
            this.Personid = personid;
            this.Companyid = companyid;
            this.IsManager = isManager;
      
        }
        public ManagerBL() { }
        public ManagerBL(int id,int personid, int companyid,bool isManager):this(personid,companyid,isManager)
        {
            this.Id = id;
        }
    }
}
