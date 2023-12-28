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



        public string ViewIPInterimBillReceipt(int BillNo, string htmlFilePath, string HeaderName)
        {
            // throw new NotImplementedException();

            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                { "BillNo", BillNo}
            };
            var Bills = GetDataTableProc("rptIPDFinalBill", dictionary);
            string html = File.ReadAllText(htmlFilePath);// templates.Rows[0]["TempDesign"].ToString();
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
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillDate"));
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



            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;
            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                items.Append("<tr><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">-</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["docname"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">-</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["Qty"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

                T_NetAmount += dr["NetAmount"].ConvertToDouble();
            }
            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


            return html;
        }
    }
}
