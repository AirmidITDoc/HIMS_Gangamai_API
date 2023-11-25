using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Pharmacy
{
    public class R_SalesReturn : GenericRepository, I_SalesReturn
    {
        public R_SalesReturn(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public String InsertSalesReturnCredit(SalesReturnCreditParams salesReturnParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@SalesReturnId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

          
            var disc3 = salesReturnParams.salesReturnHeader.ToDictionary();
            disc3.Remove("SalesReturnId");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_SalesReturnHeader_1", disc3, outputId1);

            foreach (var a in salesReturnParams.SalesReturnDetail)
            {
                var disc5 = a.ToDictionary();
                disc5["SalesReturnId"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("insert_SalesReturnDetails_1", disc5);
            }

            foreach (var a in salesReturnParams.SalesReturn_CurStk_Upt)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_T_CurStk_SalesReturn_Id_1", disc1);
            }

            foreach (var a in salesReturnParams.Update_SalesReturnQty_SalesTbl)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_SalesReturnQty_SalesTbl_1", disc1);
            }

            var vDiscCal = salesReturnParams.Update_SalesRefundAmt_SalesHeader.ToDictionary();
            vDiscCal["SalesReturnId"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("Update_SalesRefundAmt_SalesHeader", vDiscCal);

            var vGSTCal = salesReturnParams.Cal_GSTAmount_SalesReturn.ToDictionary();
            vGSTCal["SalesReturnId"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("Cal_GSTAmount_SalesReturn", vDiscCal);

            var vItemMovement = salesReturnParams.Insert_ItemMovementReport_Cursor.ToDictionary();
           // vItemMovement["SalesReturnId"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("Insert_ItemMovementReport_Cursor", vItemMovement);

            _unitofWork.SaveChanges();
            return BillNo;
        }
        public String InsertSalesReturnPaid(SalesReturnCreditParams salesReturnParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@SalesReturnId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = salesReturnParams.salesReturnHeader.ToDictionary();
            disc3.Remove("SalesReturnId");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_SalesReturnHeader_1", disc3, outputId1);

            foreach (var a in salesReturnParams.SalesReturnDetail)
            {
                var disc5 = a.ToDictionary();
                disc5["SalesReturnId"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("insert_SalesReturnDetails_1", disc5);
            }

            foreach (var a in salesReturnParams.SalesReturn_CurStk_Upt)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_T_CurStk_SalesReturn_Id_1", disc1);
            }

            foreach (var a in salesReturnParams.Update_SalesReturnQty_SalesTbl)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_SalesReturnQty_SalesTbl_1", disc1);
            }

            var vDiscCal = salesReturnParams.Update_SalesRefundAmt_SalesHeader.ToDictionary();
            vDiscCal["SalesReturnId"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("Update_SalesRefundAmt_SalesHeader", vDiscCal);

            var vGSTCal = salesReturnParams.Cal_GSTAmount_SalesReturn.ToDictionary();
            vGSTCal["SalesReturnId"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("Cal_GSTAmount_SalesReturn", vDiscCal);

            var vItemMovement = salesReturnParams.Insert_ItemMovementReport_Cursor.ToDictionary();
            // vItemMovement["SalesReturnId"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("Insert_ItemMovementReport_Cursor", vItemMovement);
            
            var vPayment = salesReturnParams.SalesReturnPayment.ToDictionary();
            vPayment.Remove("PaymentID");
            vPayment["RefundId"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_Pharmacy_New_1", vPayment);

            _unitofWork.SaveChanges();
            return BillNo;
        }
    }
}
