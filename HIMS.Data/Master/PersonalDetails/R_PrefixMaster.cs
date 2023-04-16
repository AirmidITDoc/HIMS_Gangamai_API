using HIMS.Common.Utility;
using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data.Master.PersonalDetails
{
   public class R_PrefixMaster :GenericRepository,I_PrefixMaster
    {
        public R_PrefixMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(PrefixMasterParams PrefixMasterParams)
        {
            var disc1 = PrefixMasterParams.PrefixMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_PrefixMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(PrefixMasterParams PrefixMasterParams)
        {
           // throw new NotImplementedException();
            var disc = PrefixMasterParams.PrefixMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_PrefixMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
    }
}
