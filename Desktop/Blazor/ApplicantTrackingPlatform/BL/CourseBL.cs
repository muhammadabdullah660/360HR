using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class CourseBL
    {
        private int id;
        private string type;
        private string name;
        private DateTime start;
        private int profileid;

        public int Id { get => id; set => id = value; }
        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
        public DateTime Start { get => start; set => start = value; }
        public int Profileid { get => profileid; set => profileid = value; }

        public CourseBL()
        {

        }
        public CourseBL(string type, string name, DateTime start, int profileid)
        {
            this.Type = type;
            this.Name = name;
            this.Start = start;
            this.Profileid = profileid;
        }

        public CourseBL(int id,string type, string name, DateTime start, int profileid):this(type,name,start,profileid)
        {
            this.Id = id;
        }
    }
}
