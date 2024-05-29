using HIMS.Common.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Inventory
{
   public class R_Itemmovement:GenericRepository,I_Itemmovement
    {
        public R_Itemmovement(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string ViewItemmovement(DateTime FromDate, DateTime ToDate, int ItemId, int FromStoreID, int ToStoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[5];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@ItemId", ItemId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@FromStoreID", FromStoreID) { DbType = DbType.Int64 };
            para[4] = new SqlParameter("@ToStoreId", ToStoreId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptItemMovementReport", para);

            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalAmount = 0, T_TotalVatAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TransactionTime"].ConvertToDateString("dd/MM/yyyy hh:mm tt")).Append("</td>");
               // items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["MovementNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["FromStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ToStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TransactionType"].ConvertToString()).Append("</td>");


                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ReceiptQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["IssueQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalQty"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["CGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                //items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["SGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td></tr>");




            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalDiscAmount}}", T_TotalDiscAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());
            //html = html.Replace("{{TotalCGST}}", T_TotalCGST.To2DecimalPlace());
            //html = html.Replace("{{TotalSGST}}", T_TotalSGST.To2DecimalPlace());
            //html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());

            return html;
        }
    }
}
