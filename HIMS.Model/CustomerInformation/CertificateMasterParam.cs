using HIMS.Model.CustomerInformation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class CertificateMasterParam
    {
        public SaveCertificateMasterParam SaveCertificateMasterParam { get; set; }
        public UpdateCertificateMasterParam UpdateCertificateMasterParam { get; set; }
        public CancelCertificateMasterParam CancelCertificateMasterParam { get; set; }
    }

    public class SaveCertificateMasterParam
    {
        public int CertificateId { get; set; }
        public string CertificateName { get; set; }
        public string CertificateDesc { get; set; }
        public long IsActive { get; set; }
      
        public long CreatedBy { get; set; }
       

       

    }
    public class UpdateCertificateMasterParam
    {
        public long CertificateId { get; set; }
        public string CertificateName { get; set; }
        public string CertificateDesc { get; set; }
        public long IsActive { get; set; }
       
        public long ModifiedBy { get; set; }
       

    }
    public class CancelCertificateMasterParam
    {
        public long CertificateId { get; set; }
       
        public long IsActive { get; set; }

    


    }
}

    