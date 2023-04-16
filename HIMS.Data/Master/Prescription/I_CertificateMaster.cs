using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Prescription
{
    public interface I_CertificateMaster
    {
        public bool Insert(CertificateMasterParams certificateMasterParams);
        public bool Update(CertificateMasterParams certificateMasterParams);

    }
}
