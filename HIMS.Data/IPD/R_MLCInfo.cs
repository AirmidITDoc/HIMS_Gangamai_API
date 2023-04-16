using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_MLCInfo :GenericRepository,I_MLCInfo
    {
        public R_MLCInfo(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }


        public bool Insert(MLCInfoParams MLCInfoParams)
        {
            //throw new NotImplementedException();

            var dic = MLCInfoParams.InsertMLCInfo.ToDictionary();
         // dic.Remove("MLCId");
            ExecNonQueryProcWithOutSaveChanges("insert_MLCInfo_1", dic);

                     
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Update(MLCInfoParams MLCInfoParams)
        {
            var disc1 = MLCInfoParams.UpdateMLCInfo.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_MLCInfo_1", disc1);

            _unitofWork.SaveChanges();
            return true;
        }

    }
}
