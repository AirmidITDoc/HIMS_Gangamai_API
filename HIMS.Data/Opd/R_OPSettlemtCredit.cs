using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd
{
   public class R_OPSettlemtCredit :GenericRepository,I_OPSettlemtCredit
    {
        public R_OPSettlemtCredit(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }


        public bool Insert(OPSettlementCreditParam OPSettlementCreditParam)
        {
            // throw new NotImplementedException();
            var disc = OPSettlementCreditParam.UpdateBill.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_BillBalAmount_1", disc);

            var disc1 = OPSettlementCreditParam.PaymentCreditUpdate.ToDictionary();
            disc1["BillNo"] = OPSettlementCreditParam.UpdateBill.BillNo;
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_New_1", disc1);

           
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
    }


}
