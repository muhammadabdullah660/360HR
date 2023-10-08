using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class AddressBL
    {
        private int id;
        private string country;
        private string state;
        private string streetNo;

        // Constructor
        public AddressBL()
        {
            // Default constructor
        }

        public AddressBL(string country, string state, string streetNo)
        {
            this.Country = country;
            this.State = state;
            this.StreetNo=streetNo;
        }
        public AddressBL(int id, string country, string state, string streetNo) : this(country, state, streetNo)
        {
            this.Id = id;
        }

        public int Id { get => id; set => id = value; }
        public string Country { get => country; set => country = value; }
        public string State { get => state; set => state = value; }
        public string StreetNo { get => streetNo; set => streetNo = value; }
    }
}
