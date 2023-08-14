using HIMS.Common.Utility;
using HIMS.Model.Master.DepartmenMaster;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.DepartMentMaster
{
   public class R_DischargeTypeMaster :GenericRepository,I_DischargeTypeMaster
    {
        public R_DischargeTypeMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(DischargeTypeMasterParams DischargeTypeMasterParams)
        {
            var disc1 = DischargeTypeMasterParams.DischargeTypeMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_DischargeTypeMaster_1", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(DischargeTypeMasterParams DischargeTypeMasterParams)
        {
            // throw new NotImplementedException();
            var disc = DischargeTypeMasterParams.DischargeTypeMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_DischargeTypeMaster_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

       
    }
}
