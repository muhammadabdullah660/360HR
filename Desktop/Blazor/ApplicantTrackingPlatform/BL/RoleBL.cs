using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class RoleBL
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Constructor
        public RoleBL(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
