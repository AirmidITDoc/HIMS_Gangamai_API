using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_Mrdmedicalcertificate:GenericRepository,I_Mrdmedicalcertificate
    {
        public R_Mrdmedicalcertificate(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }


      
        public bool Insert(Mrdmedicalcertificateparam Mrdmedicalcertificateparam)
        {
            //  throw new NotImplementedException();

            var dic = Mrdmedicalcertificateparam.InsertMrdmedicalcertificate.ToDictionary();
            dic.Remove("MLCId");
            ExecNonQueryProcWithOutSaveChanges("insert_MLCInfo_1", dic);


            _unitofWork.SaveChanges();
            return true;
        }

     
        public bool Update(Mrdmedicalcertificateparam Mrdmedicalcertificateparam)
        {
            // throw new NotImplementedException();

            var disc1 = Mrdmedicalcertificateparam.UpdateMrdmedicalcertificate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_MLCInfo_1", disc1);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
