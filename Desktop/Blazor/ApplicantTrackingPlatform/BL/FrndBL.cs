using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace ApplicantTrackingPlatform.BL
{
    class FrndBL
    {
        private int id;
        private int senderid;
        private int receiverid;
        private int status;
        private DateTime send;

        public int Id { get => id; set => id = value; }
        public int Senderid { get => senderid; set => senderid = value; }
        public int Receiverid { get => receiverid; set => receiverid = value; }
        public int Status { get => status; set => status = value; }
        public DateTime Send { get => send; set => send = value; }

        public FrndBL()
        {

        }
        public FrndBL(int senderid, int receiverid, int status, DateTime send)
        { 
            this.Senderid = senderid;
            this.Receiverid = receiverid;
            this.Status = status;
            this.Send = send;
           
        }
        public FrndBL(int id,int senderid, int receiverid, int status, DateTime send):this(senderid,receiverid,status,send)
        {
            this.Id = id;

        }
    }
}
