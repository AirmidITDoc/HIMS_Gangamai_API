using HIMS.Common.Utility;
using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Prescription
{
    public class R_CertificateMaster:GenericRepository,I_CertificateMaster
    {
        public R_CertificateMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(CertificateMasterParams certificateMasterParams)
        {
            var disc = certificateMasterParams.UpdateCertificateMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("  ", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(CertificateMasterParams certificateMasterParams)
        {
            var disc = certificateMasterParams.InsertCertificateMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("  ", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
