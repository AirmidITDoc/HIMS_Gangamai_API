using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class Mrddeathcertificateparam
    {

        public CertificateDelete CertificateDelete { get; set; }
        public CertificateInsert CertificateInsert { get; set; }

       
    }


    public class CertificateDelete
    {
        public int CertificateId { get; set; }
    }
    public class CertificateInsert
    {
        public int CertificateNo { get; set; }
        public int OPD_IPD_Id { get; set; }
        public DateTime CertificateDate { get; set; }
        public DateTime CertificateTime { get; set; }
        public bool OPD_IPD_Type { get; set; }

        public DateTime DateofDeath { get; set; }
        public DateTime TimeofDeath { get; set; }
        public String CauseofDeath { get; set; }
        public String PlaceOfDeath { get; set; }

        public String ResponsiblePersonName { get; set; }
        public String SMCNo { get; set; }
        public String Diagnsis { get; set; }
        public int AddedBy { get; set; }


    }
}