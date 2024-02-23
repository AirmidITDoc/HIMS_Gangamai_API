using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Data.Inventory;
using HIMS.Common.Utility;
using System.Data;
using System.Data.SqlClient;
using HIMS.Model.Inventory;
using System.IO;

namespace HIMS.Data.Inventory
{
    public class R_Indent : GenericRepository, I_Indent
    {
        public R_Indent(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public String Insert(IndentParams indentParams)
        {

            var vIndentId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@IndentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = indentParams.InsertIndent.ToDictionary();
            disc3.Remove("IndentId");
            var IndentId = ExecNonQueryProcWithOutSaveChanges("m_insert_IndentHeader_1", disc3, vIndentId);

            foreach (var a in indentParams.InsertIndentDetail)
            {
                var disc5 = a.ToDictionary();
                disc5["IndentId"] = IndentId;
                ExecNonQueryProcWithOutSaveChanges("m_insert_IndentDetails", disc5);
             
            }

            _unitofWork.SaveChanges();
            return IndentId;
        }

        public bool Update(IndentParams indentParams)
        {

            var vupdateIndent = indentParams.UpdateIndent.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_IndentHeader", vupdateIndent);

            var vdeleteParams = indentParams.DeleteIndent.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Delete_IndentDetails", vdeleteParams);

            foreach (var a in indentParams.InsertIndentDetail)
            {
                var disc5 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_insert_IndentDetails", disc5);

            }

            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewCurrentStock(string ItemName, int StoreId, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@ItemName", ItemName) { DbType = DbType.String };
            para[1] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("Retrieve_Storewise_CurrentStock", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

           

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;border-bottom: 1px solid black;text-align:center\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;height:10px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["ReceivedQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;height:10px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["IssueQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["BalanceQty"].ConvertToString()).Append("</td></tr>");

             

            }

            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{StoreName}}", Bills.GetColValue("StoreName"));

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            return html;
        }

        public string ViewDaywisestock(DateTime LedgerDate, int StoreId, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@LedgerDate", LedgerDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptStockReportDayWise", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

          
            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;border-bottom: 1px solid black;text-align:center\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle;border-bottom: 1px solid black;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;height:10px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;height:10px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["ReceivedQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["IssueQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["BalanceQty"].ConvertToString()).Append("</td>");
               items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["UnitMRP"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

            
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{LedgerDate}}", LedgerDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
          

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            return html;
        }

        public string ViewItemwiseStock(DateTime FromDate, DateTime todate, int StoreId, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@todate", todate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rpt_ItemWiseSalesReport", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

           
            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;border-bottom: 1px solid black;text-align:center\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;height:10px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["ConversionFactor"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle;border-bottom: 1px solid black;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;height:10px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["Received_Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["Sales_Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;border-bottom: 1px solid black;\">").Append(dr["Current_BalQty"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{todate}}", todate.ToString("dd/MM/yy"));
            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            return html;
        }
    }
}
