using HIMS.Model.CustomerInformation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class TCertificateInformationparams
    {
        public SaveTCertificateInformationparams SaveTCertificateInformationparams { get; set; }
        public UpdateTCertificateInformationparams UpdateTCertificateInformationparams { get; set; }

    }

    public class SaveTCertificateInformationparams
    {
        public long CertificateId { get; set; }
        public DateTime CertificateDate { get; set; }

        public DateTime CertificateTime { get; set; }
        public long VisitId { get; set; }
        public long CertificateTempId { get; set; }
        public string CertificateName { get; set; }
        public string CertificateText { get; set; }

        public long CreatedBy { get; set; }



    }
    public class UpdateTCertificateInformationparams
    {
        public long CertificateId { get; set; }
        public DateTime CertificateDate { get; set; }

        public DateTime CertificateTime { get; set; }
        public long VisitId { get; set; }
        public long CertificateTempId { get; set; }
        public string CertificateName { get; set; }
        public string CertificateText { get; set; }

        public long ModifiedBy { get; set; }

    }



}