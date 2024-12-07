using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Model.Administration
{
    public class Parameter
    {
        public   List<Param> PatientFeedbackParams { get; set; }
      
    }
    public class Param
    {
      
        public long OP_IP_ID { get; set; }
       
        public long OP_IP_Type { get; set; }
        public long DepartmentId { get; set; }

        public long FeedbackQuestionId { get; set; }
        public long FeedbackRating { get; set; }

        public string FeedbackComments { get; set; }
        public long CreatedBy { get; set; }

       

    }

   
}
