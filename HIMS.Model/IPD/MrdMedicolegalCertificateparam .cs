using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HIMS.Model.IPD
{
    public class MrdMedicolegalCertificateparam
    {

        public InsertMrdMedicolegalCertificate InsertMrdMedicolegalCertificate { get; set; }
        public UpdateMrdMedicolegalCertificate UpdateMrdMedicolegalCertificate { get; set; }

       
    }


    public class InsertMrdMedicolegalCertificate
    {
         public DateTime MLCDate { get; set; }         
         public DateTime MLCTime { get; set; }        
         public string CertificateNo { get; set; }       
         public long Op_IP_ID { get; set; }
         public long Op_IP_Type { get; set; }    
         public DateTime AccidentDate { get; set; }      
         public DateTime AccidentTime { get; set; }      
        public string Details_Injuries { get; set; }				
        public string AgeofInjuries { get; set; }
        public string CauseofInjuries { get; set; }
        public long TreatingDoctorId { get; set; }      
        public long TreatingDoctorId1 { get; set; }     
        public long TreatingDoctorId2 { get; set; }      
        public long CreatedBy { get; set; }
        public long DocId { get; set; }


    }
    public class UpdateMrdMedicolegalCertificate
    {
        public long DocId { get; set; }
        public DateTime MLCDate { get; set; }
        public DateTime MLCTime { get; set; }
        public string CertificateNo { get; set; }
        public long Op_IP_ID { get; set; }
        public long Op_IP_Type { get; set; }
        public DateTime AccidentDate { get; set; }
        public DateTime AccidentTime { get; set; }
        public string Details_Injuries { get; set; }
        public string AgeofInjuries { get; set; }
        public string CauseofInjuries { get; set; }
        public long TreatingDoctorId { get; set; }
        public long TreatingDoctorId1 { get; set; }
        public long TreatingDoctorId2 { get; set; }
        public long ModifiedBy    { get; set; }


    }
}