using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_IPBillingwithcredit : GenericRepository, I_IPBillingwithcredit
    {

        public R_IPBillingwithcredit(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public string Insert(IPBillingwithcreditparams IPBillingwithcreditparams)
        {
            //throw new NotImplementedException();

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@BillNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = IPBillingwithcreditparams.InsertBillcreditUpdateBillNo.ToDictionary();
            disc1.Remove("BillNo");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Bill_UpdateWithBillNo_1_New", disc1, outputId1);

            foreach (var a in IPBillingwithcreditparams.BillDetailscreditInsert)
            {
                var disc = a.ToDictionary();
                disc["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("insert_BillDetails_1", disc);
            }

            IPBillingwithcreditparams.Cal_DiscAmount_IPBillcredit.BillNo = (int)Convert.ToInt64(BillNo);
            var disc3 = IPBillingwithcreditparams.Cal_DiscAmount_IPBillcredit.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_OPBill", disc3);

            var AdmissionID = IPBillingwithcreditparams.AdmissionIPBillingcreditUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_AdmissionforIPBilling", AdmissionID);


            IPBillingwithcreditparams.IPBillBalAmountcredit.BillNo = (int)Convert.ToInt64(BillNo);
            var disc2 = IPBillingwithcreditparams.IPBillBalAmountcredit.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_BillBalAmount_1", disc2);

            foreach (var a in IPBillingwithcreditparams.IPAdvanceDetailUpdatecedit)
            {
                var disc = a.ToDictionary();
                //  disc["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("update_AdvanceDetail_1", disc);
            }


            var disc4 = IPBillingwithcreditparams.IPAdvanceHeaderUpdatecredit.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_AdvanceHeader_1", disc4);




            _unitofWork.SaveChanges();
            return BillNo;


        }
    }
}
