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
            ExecNonQueryProcWithOutSaveChanges("update_M_PrefixMaster_1", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(PrefixMasterParams PrefixMasterParams)
        {
           // throw new NotImplementedException();
            var disc = PrefixMasterParams.PrefixMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_M_PrefixMaster_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
    }
}
