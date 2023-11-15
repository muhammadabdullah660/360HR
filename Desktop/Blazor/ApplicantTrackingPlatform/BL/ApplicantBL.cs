using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class ApplicantBL
    {
        private int id;
        private int personid;
        private string linkutl;
        private byte[] image;
        private bool isfreelancer;


        public int Id { get => id; set => id = value; }
        public int Personid { get => personid; set => personid = value; }
        public string Linkutl { get => linkutl; set => linkutl = value; }
        public byte[] Image { get => image; set => image = value; }
        public bool Isfreelancer { get => isfreelancer; set => isfreelancer = value; }

        public ApplicantBL()
        {

        }

        public ApplicantBL(int personid, string linkutl, byte[] image,bool isfreelancer)
        {
          
           this.Personid = personid;
            this.Linkutl = linkutl;
            this.Image = image;
            this.Isfreelancer = isfreelancer;
           
        }

        public ApplicantBL(int id,int personid, string linkutl, byte[] image,bool isfreelancer):this(personid,linkutl,image,isfreelancer)
        {

            this.Id = id;

        }
    }
}
