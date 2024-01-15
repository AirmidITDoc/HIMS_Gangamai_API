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
    public class R_InsertIPDraft :GenericRepository, I_InsertIPDraft
    {
        public R_InsertIPDraft(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

      
        public String Insert(InsertIPDraftParams InsertIPDraftParams)
        {
            //throw new NotImplementedException();

            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DRBNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc = InsertIPDraftParams.IPIntremdraftbillInsert.ToDictionary();
            disc.Remove("DRBNo");
            var DRNo = ExecNonQueryProcWithOutSaveChanges("insert_DRBill_1", disc, outputId);


            foreach (var a in InsertIPDraftParams.InterimBillDetailsInsert)
            {
                var disc1 = a.ToDictionary();
                disc1["DRNo"] = DRNo;
                ExecNonQueryProcWithOutSaveChanges("insert_T_DRBillDet_1", disc1);
            }


            _unitofWork.SaveChanges();
            return DRNo;
        }

        public string ViewIPDraftBillReceipt(int AdmissionID, string htmlFilePath, string htmlHeaderFilePath)
        {
            //throw new NotImplementedException();


            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };

            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;


            var Bills = GetDataTableProc("rptIPDDraftBillPrintSummary", para);

            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo").ConvertToString());
            html = html.Replace("{{IPDNo}}", Bills.GetDateColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{Age}}", Bills.GetDateColValue("AgeYear"));
            html = html.Replace("{{GenderName}}", Bills.GetDateColValue("GenderName"));
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString());
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeDate"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));


            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{NetPayableAmt}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());
            //     html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount"));
            // html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt"));
            //html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount"));
            //html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount"));
            //html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount"));
            //html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount"));
            //html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ConcessionAmt}}", Bills.GetColValue("ConcessionAmt"));



            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                items.Append("<tr><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");

                T_NetAmount += dr["NetAmount"].ConvertToDouble();
            }
            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


            return html;
        }
    }
}
