using HIMS.Model.Administration;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_Mrdmedicalcertificate
    {
        public string InsertPatICDCode(PatICDCodeParam PatICDCodeParam);
        public bool UpdatePatICDCode(PatICDCodeParams PatICDCodeParam);
        public bool Insert(Mrdmedicalcertificateparam Mrdmedicalcertificateparam);
       public bool Update(Mrdmedicalcertificateparam Mrdmedicalcertificateparam);
        public bool SaveMICDCodingMaster(MICDCodingMasterParam MICDCodingMasterParam);
        public bool UpdateMICDCodingMaster(MICDCodingMasterParam MICDCodingMasterParam);
        public bool InsertMrdMedicolegalCertificate(MrdMedicolegalCertificateparam MrdMedicolegalCertificateparam);
        public bool UpdateMrdMedicolegalCertificate(MrdMedicolegalCertificateparam MrdMedicolegalCertificateparam);
        public bool SaveMICDCdeheadMaster(MICDCdeheadMasterParam MICDCdeheadMasterParam);
        public bool UpdateMICDCdeheadMaster(MICDCdeheadMasterParam MICDCdeheadMasterParam);
    }
}

