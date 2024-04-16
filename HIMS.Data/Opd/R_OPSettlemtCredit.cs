using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Opd
{
   public class R_OPSettlemtCredit :GenericRepository,I_OPSettlemtCredit
    {
        public R_OPSettlemtCredit(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }


        public String Insert(OPSettlementCreditParam OPSettlementCreditParam)
        {
            // throw new NotImplementedException();

            var PaymentId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var disc = OPSettlementCreditParam.UpdateBill.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_BillBalAmount_1", disc);

            var disc1 = OPSettlementCreditParam.PaymentCreditUpdate.ToDictionary();
            disc1["BillNo"] = OPSettlementCreditParam.UpdateBill.BillNo;
            disc1.Remove("PaymentId");
            var Id =ExecNonQueryProcWithOutSaveChanges("m_insert_Payment_OPIP_1", disc1, PaymentId);

           
            //commit transaction
            _unitofWork.SaveChanges();
            return Id;

        }
    }


}
