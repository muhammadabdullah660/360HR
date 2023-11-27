using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.BL
{
    class InterviewFeedbackBL
    {
        private int id;
        private int jid;
        private DateTime sentDate;
        private string message;

        public int Id { get => id; set => id = value; }
        public int Jid { get => jid; set => jid = value; }
        public DateTime SentDate { get => sentDate; set => sentDate = value; }
        public string Message { get => message; set => message = value; }

        public InterviewFeedbackBL()
        {

        }
        public InterviewFeedbackBL(int jid, DateTime sentDate, string message)
        {
            this.Jid = jid;
            this.SentDate = sentDate;
            this.Message = message;
        }

        public InterviewFeedbackBL(int id,int jid, DateTime sentDate, string message):this(jid,sentDate,message)
        {
            this.Id = id;
        }
    }
}
