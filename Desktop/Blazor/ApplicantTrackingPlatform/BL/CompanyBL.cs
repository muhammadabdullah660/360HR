using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class CompanyBL
    {
        private int id;
        private string name;
        private string description;
        private string contact;
        private int addressid;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Contact { get => contact; set => contact = value; }
        public int Addressid { get => addressid; set => addressid = value; }
        public int Id { get => id; set => id = value; }

        public CompanyBL(string name, string description, string contact, int addressid)
        {
            this.Name = name;
            this.Description = description;
            this.Contact = contact;
            this.Addressid = addressid;
        }

        public CompanyBL() { }
        public CompanyBL(int id, string name, string description, string contact, int addressid) : this(name, description, contact, addressid)
        {
            this.Id = id;

        }
    }
}
