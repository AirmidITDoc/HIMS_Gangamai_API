using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_IPBilling:GenericRepository,I_IPBilling
    {
        public R_IPBilling(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String Insert(IPBillingParams IPBillingParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@BillNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            
            var disc1 = IPBillingParams.InsertBillUpdateBillNo.ToDictionary();
            disc1.Remove("BillNo");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Bill_UpdateWithBillNo_1_New", disc1, outputId1);

            foreach (var a in IPBillingParams.BillDetailsInsert)
            {
                var disc = a.ToDictionary();
                disc["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("insert_BillDetails_1", disc);
            }

            IPBillingParams.Cal_DiscAmount_IPBill.BillNo = (int)Convert.ToInt64(BillNo);
            var disc3 = IPBillingParams.Cal_DiscAmount_IPBill.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_OPBill", disc3);

            var AdmissionID = IPBillingParams.AdmissionIPBillingUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_AdmissionforIPBilling", AdmissionID);

            var disc7 = IPBillingParams.IPInsertPayment.ToDictionary();
            disc7["BillNo"] = (int)Convert.ToInt64(BillNo);
            //disc7.Remove("PaymentId");
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc7);


             IPBillingParams.IPBillBalAmount.BillNo = (int)Convert.ToInt64(BillNo);
             var disc2 = IPBillingParams.IPBillBalAmount.ToDictionary();
             ExecNonQueryProcWithOutSaveChanges("update_BillBalAmount_1", disc2);

            foreach (var a in IPBillingParams.IPAdvanceDetailUpdate)
            {
                var disc = a.ToDictionary();
              //  disc["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("update_AdvanceDetail_1", disc);
            }

                      
            var disc4 = IPBillingParams.IPAdvanceHeaderUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_AdvanceHeader_1", disc4);




            _unitofWork.SaveChanges();
            return BillNo;



           /* var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@BillNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@BillDetailId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId3 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId ",
                Value = 0,
                Direction = ParameterDirection.Output
            };



            foreach (var a in IPBillingParams.InsertPathologyReportHeader)
            {
                var disc1 = a.ToDictionary();
                //disc5["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("insert_PathologyReportHeader_1", disc1);
            }

            foreach (var a in IPBillingParams.InsertRadiologyReportHeader)
            {
                var disc2 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("insert_RadiologyReportHeader_1", disc2);
            }

            var disc3 = IPBillingParams.InsertBillupdatewithbillno.ToDictionary();
            disc3.Remove("BillNo");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Bill_UpdateWithBillNo_1_New", disc3, outputId1);

            foreach (var a in IPBillingParams.OpBillDetailsInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("insert_BillDetails_1", disc5);
            }

            // var disc4 = OPbillingparams.OPoctorShareGroupAdmChargeDoc.ToDictionary();
            // disc4["BillNo"] = (int)Convert.ToInt64(BillNo);
            //disc4.Remove("BillNo");
            //new  ExecNonQueryProcWithOutSaveChanges("ps_OP_Doctor_Share_Group_Adm_ChargeDoc_1", disc4);


            var disc6 = IPBillingParams.OPCalDiscAmountBill.ToDictionary();
            disc6["BillNo"] = (int)Convert.ToInt64(BillNo);
            ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_OPBill", disc6);

            //IPBillingParams.BillDetailsInsert.BillNo = (int)Convert.ToInt64(BillNo);
            var disc7 = IPBillingParams.OPInsertPayment.ToDictionary();
            disc7["BillNo"] = (int)Convert.ToInt64(BillNo);
            //disc7.Remove("PaymentId");
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc7);

            _unitofWork.SaveChanges();
            return true;*/
        }

       
    



    public string ViewIPBillReceipt(int BillNo, string htmlFilePath,string htmlHeaderFilePath)
    {
      


            SqlParameter[] para = new SqlParameter[1];
        
            para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPDFinalBill", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;

            html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetDateColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{Age}}", Bills.GetDateColValue("Age"));
            html = html.Replace("{{GenderName}}", Bills.GetDateColValue("GenderName"));
            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionDate"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeDate"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillDate").ConvertToDateString());
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));


            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount"));
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt"));
            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount"));
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount"));
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount"));
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount"));
            html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount"));
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount"));
            html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount"));
            html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmount"));
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());
            



       
        foreach (DataRow dr in Bills.Rows)
        {
            i++;
            items.Append("<tr><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;\">").Append(i).Append("</td>");
            items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
            items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
            items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
            items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;\">").Append(dr["Qty"].ConvertToDateString()).Append("</td>");
            items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

            T_NetAmount += dr["ChargesTotalAmt"].ConvertToDouble();
        }
        html = html.Replace("{{Items}}", items.ToString());
      
        html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


        return html;

    }

}
}
