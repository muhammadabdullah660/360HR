using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class AchivementBL
    {
        private int proid;
        private string des;

        public int Proid { get => proid; set => proid = value; }
        public string Des { get => des; set => des = value; }

        public AchivementBL()
        {

        }
        public AchivementBL(int proid, string des)
        {
            this.Proid = proid;
            this.Des = des;
        }
    }

}
