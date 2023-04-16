using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_IPInterimBill:GenericRepository,I_IPInterimBill
    {
        public R_IPInterimBill(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String Insert(IPInterimBillParams IPInterimBillParams)
        {
            var ChargesId = IPInterimBillParams.InterimBillChargesUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_InterimBillCharges_1", ChargesId);

           
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@BillNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = IPInterimBillParams.InsertBillUpdateBillNo1.ToDictionary();
            disc1.Remove("BillNo");
            var BillNo1 = ExecNonQueryProcWithOutSaveChanges("insert_Bill_UpdateWithBillNo_1_New", disc1, outputId1);

            foreach (var a in IPInterimBillParams.BillDetailsInsert1)
            {
                var disc = a.ToDictionary();
                disc["BillNo"] = BillNo1;
                ExecNonQueryProcWithOutSaveChanges("insert_BillDetails_1", disc);
            }

            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc2 = IPInterimBillParams.IPIntremPaymentInsert.ToDictionary();
            disc2["BillNo"]=BillNo1;
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc2);

          

            _unitofWork.SaveChanges();
            return BillNo1;
        }


    }
}
