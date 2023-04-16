using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class MLCInfoParams
    {

        public InsertMLCInfo InsertMLCInfo { get; set; }
        public UpdateMLCInfo UpdateMLCInfo { get; set; }

    }

          public class InsertMLCInfo
           {

          // public int MLCId { get; set; }
           public int AdmissionId { get; set; }
            public String MLCNo { get; set; }
            public DateTime ReportingDate { get; set; }
            public DateTime ReportingTime { get; set; }
            public String AuthorityName { get; set; }
            public String BuckleNo { get; set; }
            public String PoliceStation { get; set; }
            
        }


    public class UpdateMLCInfo
    {
        public int MLCId { get; set; }
        public int AdmissionId { get; set; }
        public String MLCNo { get; set; }
        public DateTime ReportingDate { get; set; }
        public DateTime ReportingTime { get; set; }
        public String AuthorityName { get; set; }
        public String BuckleNo { get; set; }
        public String PoliceStation { get; set; }

    }
}

