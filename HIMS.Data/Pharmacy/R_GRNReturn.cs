using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public class R_GRNReturn :GenericRepository , I_GRNReturn
    {
           public R_GRNReturn(IUnitofWork unitofWork) : base(unitofWork)
    {

    }

        public string InsertGRNReturn(GRNReturnParam GRNReturnParam)
        {
            //  throw new NotImplementedException();


            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@GRNReturnId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = GRNReturnParam.GRNReturnSave.ToDictionary();
            disc3.Remove("GRNReturnId");
            var GrnReturnId = ExecNonQueryProcWithOutSaveChanges("m_insert_GRNReturnH_GrnReturnNo_1", disc3, outputId2);

            foreach (var a in GRNReturnParam.GRNReturnDetailSave)
            {
                var disc5 = a.ToDictionary();
                //   disc5.Remove("GRNDetID");
                disc5["GrnReturnId"] = GrnReturnId;
                var GrnDetID = ExecNonQueryProcWithOutSaveChanges("m_insert_GRNReturnDetails_1", disc5);
            }
            foreach (var a in GRNReturnParam.GRNReturnUpdateCurrentStock)
            {
                var disc6 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_Update_T_CurrentStock_GRNReturn_1", disc6);
            }


            foreach (var a in GRNReturnParam.GRNReturnUpateReturnQty)
            {
                var disc7 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_Update_GrnReturnQty_GrnTbl_1", disc7);
            }
           
            _unitofWork.SaveChanges();
            return GrnReturnId;
        }

        public bool VerifyGRNReturn(GRNReturnParam GRNReturnParam)
        {
            var vGRNVerify = GRNReturnParam.UpdateGRNReturnVerifyStatus.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_GRNReturn_Verify_Status_1", vGRNVerify);

            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewGRNReturnReport(int GRNReturnId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@GRNReturnId", GRNReturnId) { DbType = DbType.String };

            var Bills = GetDataTableProc("rptGRNReturnPrint", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            Boolean chkdiscflag = false;
            Boolean chkponoflag = false;

            double T_TotalAmount = 0, T_TotalVatAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr  style=\"font-size:15px;border: 1px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;color: #101828 \"><td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Conversion"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchExpiryDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LandedRate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ReturnQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LandedTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatPercentage"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalDiscAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                T_TotalVatAmount += dr["VatAmount"].ConvertToDouble();
                T_TotalDiscAmount += dr["TotalDiscAmount"].ConvertToDouble();
                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                //T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();
                //T_TotalCGST += dr["CGSTAmt"].ConvertToDouble();
                //T_TotalSGST += dr["SGSTAmt"].ConvertToDouble();
                //T_TotalIGST += dr["IGSTAmt"].ConvertToDouble();


            }
            
           //| currency:'INR':'symbol-narrow':'0.2'
           html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());
            html = html.Replace("{{TotalDiscAmount}}", Bills.GetColValue("TotalDiscAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalNETAmount}}", Bills.GetColValue("GrnReturnAmount").ConvertToDouble().To2DecimalPlace());


            html = html.Replace("{{TotCGSTAmt}}", Bills.GetColValue("TotCGSTAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{GrandTotalAount}}", Bills.GetColValue("TotalAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalAmount}}", Bills.GetColValue("TotalAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{HandlingCharges}}", Bills.GetColValue("HandlingCharges").ConvertToDouble().To2DecimalPlace());

            html = html.Replace("{{TransportChanges}}", Bills.GetColValue("TransportChanges").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{DiscAmount}}", Bills.GetColValue("DiscAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotSGSTAmt}}", Bills.GetColValue("TotSGSTAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalDiscAmount}}", Bills.GetColValue("TotalDiscAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{OtherCharge}}", Bills.GetColValue("OtherCharge").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{CreditNote}}", Bills.GetColValue("CreditNote").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AddedByName}}", Bills.GetColValue("AddedByName").ConvertToString());
            html = html.Replace("{{DebitNote}}", Bills.GetColValue("DebitNote").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Remark}}", Bills.GetColValue("Remark").ConvertToString());
            html = html.Replace("{{TotalVATAmount}}", Bills.GetColValue("TotalVATAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{NetPayble}}", Bills.GetColValue("GrnReturnAmount").ConvertToDouble().To2DecimalPlace());

            html = html.Replace("{{T_TotalCGST}}", T_TotalCGST.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_TotalSGST}}", T_TotalSGST.ConvertToDouble().To2DecimalPlace());

            html = html.Replace("{{GRNDate}}", Bills.GetColValue("GRNDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{GRNTime}}", Bills.GetColValue("GRNReturnTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{PurchaseTime}}", Bills.GetColValue("PurchaseTime").ConvertToDateString("dd/MM/yyyy"));

            html = html.Replace("{{InvDate}}", Bills.GetColValue("InvDate").ConvertToDateString("dd/MM/yyyy"));

            html = html.Replace("{{GateEntryNo}}", Bills.GetColValue("GateEntryNo"));

            html = html.Replace("{{GrnNumber}}", Bills.GetColValue("GRNReturnNo"));
            html = html.Replace("{{EwayBillDate}}", Bills.GetColValue("EwayBillDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{GSTNo}}", Bills.GetColValue("GSTNo").ConvertToString());
            html = html.Replace("{{EwayBillNo}}", Bills.GetColValue("EwayBillNo"));


            html = html.Replace("{{SupplierName}}", Bills.GetColValue("SupplierName").ConvertToString());
            html = html.Replace("{{PONo}}", Bills.GetColValue("PONo").ConvertToString());
            html = html.Replace("{{InvoiceNo}}", Bills.GetColValue("InvoiceNo").ConvertToString());
            html = html.Replace("{{StoreName}}", Bills.GetColValue("StoreName"));
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            
            html = html.Replace("{{Email}}", Bills.GetColValue("Email").ConvertToString());
            html = html.Replace("{{GSTNo}}", Bills.GetColValue("GSTNo").ConvertToString());
            html = html.Replace("{{Mobile}}", Bills.GetColValue("Mobile"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{VatAmount}}", Bills.GetColValue("VatAmount").ConvertToString());
            html = html.Replace("{{Mobile}}", Bills.GetColValue("Mobile"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PONo}}", Bills.GetColValue("PONo"));

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            string finalamt = NumberToWords(Bills.GetColValue("GrnReturnAmount").ConvertToDouble().To2DecimalPlace().ToInt());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("T_TotalDiscAmount").ConvertToDouble() > 0 ? "block" : "none");


            html = html.Replace("{{chkponoflag}}", Bills.GetColValue("PONo").ConvertToDouble() != 0 ? "table-row " : "none");


            return html;

        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

    }
}
