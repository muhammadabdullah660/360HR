using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class EducationBL
    {
        private int id;
        private string degree;
        private string institute;
        private DateTime start;
        private DateTime end;
        private string grade;
        private int profileid;

        public string Degree { get => degree; set => degree = value; }
        public string Institute { get => institute; set => institute = value; }
        public DateTime Start { get => start; set => start = value; }
        public DateTime End { get => end; set => end = value; }
        public string Grade { get => grade; set => grade = value; }
        public int Profileid { get => profileid; set => profileid = value; }
        public int Id { get => id; set => id = value; }

        public EducationBL()
        {

        }
        public EducationBL(string degree, string institute, DateTime start, DateTime end, string grade,int profileid)
        {
            this.Degree = degree;
            this.Institute = institute;
            this.Start = start;
            this.End = end;
            this.Grade = grade;
            this.Profileid =profileid;
          
        }
        public EducationBL(int id,string degree, string institute, DateTime start, DateTime end, string grade, int profileid):this(degree,institute,start,end,grade,profileid)
        {
            this.Id = id;
        }
    }
}
