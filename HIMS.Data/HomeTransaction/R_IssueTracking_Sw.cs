using HIMS.Common.Utility;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
   public class R_IssueTracking_Sw :GenericRepository ,I_IssueTracking_Sw
    { 
        public R_IssueTracking_Sw(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

         public bool Save(IssueTracking_SwParams IssueTracking_SwParams)
        {
            //throw new NotImplementedException();
            var disc1 = IssueTracking_SwParams.IssueTracking_SwInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Insert_T_IssueTracking_Sw", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Update(IssueTracking_SwParams IssueTracking_SwParams)
        {
            
            var disc1 = IssueTracking_SwParams.IssueTracking_SwUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_IssueTracking_Sw", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        
    }
}
