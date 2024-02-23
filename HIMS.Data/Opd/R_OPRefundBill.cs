using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Opd
{ 
    public class R_OPRefundBill : GenericRepository, I_OPRefundBill
    {
        public R_OPRefundBill(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String Insert(OPRefundBillParams OPRefundBillParams)
        {
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RefundId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = OPRefundBillParams.InsertRefund.ToDictionary();
            disc1.Remove("RefundId");
            var RefundId = ExecNonQueryProcWithOutSaveChanges("insert_Refund_1", disc1, outputId1);


            foreach (var a in OPRefundBillParams.InsertOPRefundDetails)
            {
                var disc2 = a.ToDictionary();
                disc2["RefundID"] = RefundId;
                disc2["RefundAmount"] = OPRefundBillParams.InsertRefund.RefundAmount;
                ExecNonQueryProcWithOutSaveChanges("insert_T_RefundDetails_1", disc2);
            }

            foreach (var a in OPRefundBillParams.Update_AddCharges_RefundAmount)
            {

                var disc3 = a.ToDictionary();
                disc3["RefundAmount"] = OPRefundBillParams.InsertRefund.RefundAmount;
                ExecNonQueryProcWithOutSaveChanges("Update_AddCharges_RefundAmt", disc3);
            }

          
           /* var disc4 = OPRefundBillParams.OP_DoctorShare_GroupWise_RefundOfBill.ToDictionary();
            disc4["RefundId"] = RefundId;
            ExecNonQueryProcWithOutSaveChanges("OP_DoctorShare_GroupWise_RefundOfBill", disc4);

            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };*/

            var disc5 = OPRefundBillParams.InsertOPPayment.ToDictionary();
           // disc5.Remove("PaymentId");
            disc5["RefundId"] = RefundId;
            disc5["BillNo"] = OPRefundBillParams.InsertRefund.BillId;
            disc5["AdvanceId"] = OPRefundBillParams.InsertRefund.AdvanceId;
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc5);

            _unitofWork.SaveChanges();      
            return RefundId;
        }

        public string ViewOPRefundofBillReceipt(int RefundId, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@RefundId", RefundId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptOPRefundofBillPrint", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;



            //html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());
            //html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{Remark}}", Bills.GetColValue("Remark"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RefundNo}}", Bills.GetColValue("RefundNo"));
            html = html.Replace("{{Addedby}}", Bills.GetColValue("Addedby"));

            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AdmissinDate}}", Bills.GetColValue("AdmissinDate"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{NetPayableAmt}}", Bills.GetColValue("NetPayableAmt"));
            html = html.Replace("{{RefundAmount}}", Bills.GetColValue("RefundAmount"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{MobileNo}}", Bills.GetColValue("MobileNo"));

            html = html.Replace("{{RefundDate}}", Bills.GetColValue("RefundDate").ConvertToDateString());
            html = html.Replace("{{RefundTime}}", Bills.GetColValue("RefundTime").ConvertToDateString("dd/mm/yyyy hh:mm"));
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString());
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            html = html.Replace("{{PBillNo}}", Bills.GetColValue("PBillNo").ConvertToString());
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillDate").ConvertToDateString());
            html = html.Replace("{{NetPayableAmt}}", Bills.GetColValue("NetPayableAmt"));
            

            return html;
        }
    }
}
