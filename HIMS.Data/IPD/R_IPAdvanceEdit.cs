using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_IPAdvanceEdit :GenericRepository,I_IPAdvanceEdit
    {
        public R_IPAdvanceEdit(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(IPAdvaceEditparam IPAdvaceEditparam)
        {
            // throw new NotImplementedException();

            foreach (var a in IPAdvaceEditparam.UpdateAdvancedet)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_Advance_det", disc);
            }

            foreach (var a in IPAdvaceEditparam.UpdatePayAmountAdvance)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_PayAmount_Advance", disc);
            }

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
