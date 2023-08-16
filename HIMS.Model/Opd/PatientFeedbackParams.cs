using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
    public class PatientFeedbackParams
    {
        public List<PatientFeedbackInsert> PatientFeedbackInsert { get; set; }
    }

    public class PatientFeedbackInsert
    {
        public long PatientFeedbackId { get; set; }
        public long OP_IP_ID { get; set; }
        public long OP_IP_Type { get; set; }
        public string FeedbackCategory { get; set; }
        public string FeedbackRating { get; set; }
        public string FeedbackComments { get; set; }
        public long AddedBy { get; set; }
    }

}
