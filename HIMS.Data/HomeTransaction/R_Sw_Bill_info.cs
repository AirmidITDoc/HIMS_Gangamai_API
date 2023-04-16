using HIMS.Common.Utility;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
    public class R_Sw_Bill_info :GenericRepository,I_Sw_Bill_info
    {
        public R_Sw_Bill_info(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(Sw_Bill_infoParams Sw_Bill_infoParams)
        {
            //Update VendorMaster
            var disc = Sw_Bill_infoParams.Sw_Bill_infoUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_Sw_Bill_info", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
        

        public bool Save(Sw_Bill_infoParams Sw_Bill_infoParams)
        {

            var disc = Sw_Bill_infoParams.Sw_Bill_infoInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Insert_T_Sw_Bill_info", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

      /*  public bool Save(Sw_Bill_infoParams Sw_Bill_infoParams)
        {
            throw new NotImplementedException();
        }

        public bool Update(Sw_Bill_infoParams Sw_Bill_infoParams)
        {
            throw new NotImplementedException();
        }*/
    }
}
