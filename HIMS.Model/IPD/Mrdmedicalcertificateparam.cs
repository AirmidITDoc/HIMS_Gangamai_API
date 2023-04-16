using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
  public class Mrdmedicalcertificateparam
    {

        public InsertMrdmedicalcertificate InsertMrdmedicalcertificate { get; set; }

        public UpdateMrdmedicalcertificate UpdateMrdmedicalcertificate { get; set; }

    }

    public class InsertMrdmedicalcertificate
    {

         public int AdmissionId { get; set; }
        public String MLCNo { get; set; }
        public DateTime ReportingDate { get; set; }
        public DateTime ReportingTime { get; set; }
        public String AuthorityName { get; set; }
        public String BuckleNo { get; set; }
        public String PoliceStation { get; set; }

    }


    public class UpdateMrdmedicalcertificate
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
