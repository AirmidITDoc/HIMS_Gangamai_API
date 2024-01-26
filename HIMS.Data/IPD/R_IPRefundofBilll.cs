using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace HIMS.Data.IPD
{
    public class R_IPRefundofBilll:GenericRepository,I_IPRefundofBilll
    {
        public R_IPRefundofBilll(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public String Insert(IPRefundofBilllparams IPRefundofBilllparams)
        {
            //throw new NotImplementedException();
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RefundId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var dic = IPRefundofBilllparams.InsertIPRefundofNew.ToDictionary();
            dic.Remove("RefundId");
            var RefundId = ExecNonQueryProcWithOutSaveChanges("insert_Refund_1", dic, outputId);


            foreach (var a in IPRefundofBilllparams.InsertRefundDetails)
            {
                var disc2 = a.ToDictionary();
                disc2["RefundID"] = RefundId;
                disc2["RefundAmount"] = IPRefundofBilllparams.InsertIPRefundofNew.RefundAmount;
                ExecNonQueryProcWithOutSaveChanges("insert_T_RefundDetails_1", disc2);
            }

             foreach (var a in IPRefundofBilllparams.UpdateAddChargesDetails)
            {

                var disc3 = a.ToDictionary();
                disc3["RefundAmount"] = IPRefundofBilllparams.InsertIPRefundofNew.RefundAmount;
                ExecNonQueryProcWithOutSaveChanges("Update_AddCharges_RefundAmt", disc3);
            }

           /* IPRefundofBilllparams.IPDocShareGroupAdmRefundofBillDoc.RefundId = Convert.ToInt32(RefundId);
            var disc4 = IPRefundofBilllparams.IPDocShareGroupAdmRefundofBillDoc.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_IP_DocShare_Group_Adm_RefundofBill_Doc_1", disc4);*/

            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc5 = IPRefundofBilllparams.IPDInsertPayment.ToDictionary();
           // disc5.Remove("PaymentId");
            disc5["RefundId"] = RefundId;
            disc5["BillNo"] = IPRefundofBilllparams.InsertIPRefundofNew.BillId;
            disc5["AdvanceId"] = IPRefundofBilllparams.InsertIPRefundofNew.AdvanceId;
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc5);

            _unitofWork.SaveChanges();
            return RefundId;

        }


        //exec rptIPRefundofBillPrint 10268 
        string I_IPRefundofBilll.ViewIPRefundofBillReceipt(int RefundId, string htmlFilePath, string htmlHeaderFilePath)
        {
           

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@RefundId", RefundId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPRefundofBillPrint", para);
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
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString());
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


            return html;
        }
    }
}

