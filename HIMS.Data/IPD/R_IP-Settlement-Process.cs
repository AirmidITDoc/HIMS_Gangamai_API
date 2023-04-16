using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_IP_Settlement_Process:GenericRepository,I_IP_Settlement_Process
    {
        public R_IP_Settlement_Process(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public String Insert(IP_Settlement_Processparams IP_Settlement_Processparams)
        {
            // throw new NotImplementedException();

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = IP_Settlement_Processparams.IPPaymentCreditUpdate.ToDictionary();
            disc1["BillNo"] = IP_Settlement_Processparams.UpdateIpBill.BillNo;
            disc1.Remove("PaymentId");
            var paymentid = ExecNonQueryProcWithOutSaveChanges("insert_Payment_New_1", disc1, outputId1);

            var disc = IP_Settlement_Processparams.UpdateIpBill.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_BillBalAmount_1", disc);


            foreach (var a in IP_Settlement_Processparams.IPsettlementAdvanceDetailUpdate)
            {
                var disc2 = a.ToDictionary();
                //  disc["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("update_AdvanceDetail_1", disc2);
            }


            var disc4 = IP_Settlement_Processparams.IPsettlementAdvanceHeaderUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_AdvanceHeader_1", disc4);




            //commit transaction
            _unitofWork.SaveChanges();
            return paymentid;

        }
    }
}
