using HIMS.Common.Utility;
using HIMS.Model;
using HIMS.Model.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data
{
  public  class R_PayTranModeMaster :GenericRepository,I_PayTranModeMaster
    {
        public R_PayTranModeMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

      
      
        public bool Update(PayTranModeMasterParams PayTranModeMasterParamsHome)
        {
           // throw new NotImplementedException();

            // throw new NotImplementedException();

            var disc1 = PayTranModeMasterParamsHome.PayTranModeMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_M_PayTranModeMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(PayTranModeMasterParams PayTranModeMasterParamsHome)
        {
          //  throw new NotImplementedException();



            var disc = PayTranModeMasterParamsHome.PayTranModeMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("Insert_M_PayTranModeMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
    }
}

