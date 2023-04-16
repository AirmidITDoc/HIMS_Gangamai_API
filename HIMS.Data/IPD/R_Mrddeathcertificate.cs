using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_Mrddeathcertificate:GenericRepository,I_Mrddeathcertificate
    {
        public R_Mrddeathcertificate(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Insert(Mrddeathcertificateparam Mrddeathcertificateparam)
        {
            //throw new NotImplementedException();

            var disc1 = Mrddeathcertificateparam.CertificateDelete.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Delete_T_DeathCertificate_1", disc1);



            var disc = Mrddeathcertificateparam.CertificateInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_DeathCertificate_1", disc);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
