using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.CustomerInformation;
using HIMS.Model.Opd;

namespace HIMS.Data.Opd
{
   public interface I_OPDRegistration
    {
        string Insert(OPDRegistrationParams OPDRegistrationParams);
        bool Update(OPDRegistrationParams OPDRegistrationParams);
        public bool TConsentInformationSave(TConsentInformationparams TConsentInformationparams);
        public bool TConsentInformationUpdate(TConsentInformationparams TConsentInformationparams);
        public bool TCertificateInformationSave(TCertificateInformationparams TCertificateInformationparams);
        public bool TCertificateInformationUpdate(TCertificateInformationparams TCertificateInformationparams);
        public string ViewCertificateInformationPrint(int CertificateId,  string htmlFilePath, string HeaderName);

    }
}
