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
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_DischargeTypeMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(DischargeTypeMasterParams DischargeTypeMasterParams)
        {
            // throw new NotImplementedException();
            var disc = DischargeTypeMasterParams.DischargeTypeMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_DischargeTypeMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

       
    }
}
