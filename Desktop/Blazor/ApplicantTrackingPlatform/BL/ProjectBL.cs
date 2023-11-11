using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class ProjectBL
    {
        private int id;
        private string title;
        private string description;
        private DateTime start;
        private DateTime end;
        private int managerid;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public DateTime Start { get => start; set => start = value; }
        public DateTime End { get => end; set => end = value; }
        public int Managerid { get => managerid; set => managerid = value; }

        public ProjectBL()
        {

        }

        public ProjectBL(string title, string description, DateTime start, DateTime end, int managerid)
        {
            this.Title = title;
            this.Description = description;
            this.Start = start;
            this.End = end;
            this.Managerid = managerid;
        }
        public ProjectBL(int id,string title, string description, DateTime start, DateTime end, int managerid):this(title,description,start,end,managerid)
        {
            this.Id = id;
        }
    }
}
