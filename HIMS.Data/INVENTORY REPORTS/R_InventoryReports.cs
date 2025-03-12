using HIMS.Common.Utility;
using HIMS.Data.MISReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using HIMS.Common.Utility;
using HIMS.Data.InventoryReports;

namespace HIMS.Data.Opd
{
    public class R_InventoryReport : GenericRepository, I_InventoryReport
    {


        public R_InventoryReport(IUnitofWork unitofWork) : base(unitofWork)
        {

        }


        public string ViewItemList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {


            SqlParameter[] para = new SqlParameter[0];
            //para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptItemList", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0;
            string previousLabel = "";




            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["ItemTypeName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"8\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["ItemTypeName"].ConvertToString())
                {
                    j = 1;

                    //items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                    //   .Append(Dcount.ToString()).Append("</td></tr>");

                    //Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"8\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["ItemTypeName"].ConvertToString()).Append("</td></tr>");

                }

                //Dcount = Dcount + 1;
                T_Count = T_Count + 1;
                previousLabel = dr["ItemTypeName"].ConvertToString();

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                //items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemTypeName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CurrentSell"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PurchaseRate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PurchaseUOM"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["StockUOM"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["UnitConv"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemCategoryName"].ConvertToString()).Append("</td></tr>");
            
                //T_Count += dr["PatientName"].ConvertToDouble();

                //if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                //{

                //    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                //         .Append(Dcount.ToString()).Append("</td></tr>");


                //}

            }

            html = html.Replace("{{T_Count}}", T_Count.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }


        public string ViewSupplierList(String SupplierName, int StoreID, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@SupplierName", SupplierName) { DbType = DbType.String };
            para[1] = new SqlParameter("@StoreID", StoreID) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("Rtrv_SupplierMasterList_by_Name", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
         


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"text-align: left;font-size:20px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; font-size:20px;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Address"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center;font-size:20px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Mobile"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; font-size:20px;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Email"].ConvertToString()).Append("</td></tr>");






            }


            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

          

            return html;

        
    }
        public string ViewIndentReport(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@FromStoreId", FromStoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@ToStoreId", ToStoreId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptIndentDetails", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
         


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IndentNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ToStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td></tr>");



              


            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));



            return html;

        }
        public string ViewMonthlyPurchaseGRNReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptMonthlyGRNPurchase", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"text-align: left;font-size:20px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center;font-size:20px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Cnt"].ConvertToString()).Append("</td>");
               
                items.Append("<td style=\"text-align: right;font-size:20px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td></tr>");



                T_NetAmount += dr["NetAmount"].ConvertToDouble();


            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;

        }

        public string ViewGRNReport(DateTime FromDate, DateTime ToDate, int StoreId, int SupplierID, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();



            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@SupplierID", SupplierID) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptGrnlistDetails", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_ItemNetAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrnNumber"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ReceiveQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["FreeQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align: right;\">").Append(dr["MRP"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align: right;\">").Append(dr["Rate"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align: right;\">").Append(dr["ItemTotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConversionFactor"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatPercentage"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align: right;\">").Append(dr["VatAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiscPercentage"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align: right;\">").Append(dr["DiscAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align: right;\">").Append(dr["ItemNetAmount"].ConvertToDouble()).Append("</td></tr>");

                T_ItemNetAmount += dr["ItemNetAmount"].ConvertToDouble();




            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_ItemNetAmount}}", T_ItemNetAmount.To2DecimalPlace());

            return html;

        }
        public string ViewGRNReportSummary(DateTime FromDate, DateTime ToDate, int StoreId, int SupplierID, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@SupplierID", SupplierID) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("m_rptGrnlistSummary", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_ItemNetAmount = 0, T_TotalAmount = 0, T_GSTAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
           
                items.Append("<td style=\"text-align: center;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrnNumber"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["InvoiceNo"].ConvertToString()).Append("</td>");
               
                items.Append("<td style=\"text-align: right;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
          
                items.Append("<td style=\"text-align: right;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GSTAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["UserName"].ConvertToString()).Append("</td></tr>");

                T_ItemNetAmount += dr["NetAmount"].ConvertToDouble();
                T_GSTAmount += dr["GSTAmount"].ConvertToDouble();
                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();



            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_ItemNetAmount}}", T_ItemNetAmount.To2DecimalPlace());
            html = html.Replace("{{T_GSTAmount}}", T_GSTAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalAmount}}", T_TotalAmount.To2DecimalPlace());

            return html;

        }

        public string ViewGRNReportNΑΒΗ(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("RptGrnNABH", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"text-align: center;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNDate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ReceiveQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["FreeQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConversionFactor"].ConvertToString()).Append("</td></tr>");
             






            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));



            return html;

        }

        public string ViewGRNReturnReport(DateTime ToDate, DateTime FromDate, int StoreId, int SupplierID, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@Fromdate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@Todate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@SupplierID", SupplierID) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptGrnReturnlistDetails", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNReturnDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNReturnNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchExpiryDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td></tr>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ReturnQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatPercentage"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LandedRate"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td></tr>");


            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));



            return html;

        }

        public string ViewIssueToDepartmentMonthlySummary(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@Fromdate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@Todate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@FromStoreId", FromStoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@ToStoreId", ToStoreId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptIssueToDepartmentSummary", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

              
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["FromStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ToStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
            
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalVatAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td></tr>");

                T_NetAmount += dr["NetAmount"].ConvertToDouble();
            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;

        }

        public string ViewGRNWiseProductQtyReport(DateTime FromDate, DateTime ToDate, int SupplierId, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@SupplierId", SupplierId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptGRNWiseProductQtyDet", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_ReceiveQty = 0 , T_FreeQty = 0, T_TotalQty = 0, T_MRP = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: left; font-size:20px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center;font-size:20px;  border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center;font-size:20px;  border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrnNumber"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: left; font-size:20px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center;font-size:20px;  border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ReceiveQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center;font-size:20px;  border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["FreeQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center;font-size:20px;  border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right;font-size:20px;  border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MRP"].ConvertToDouble()).Append("</td></tr>");
                T_ReceiveQty += dr["ReceiveQty"].ConvertToDouble();
                T_FreeQty += dr["FreeQty"].ConvertToDouble();
                T_TotalQty += dr["TotalQty"].ConvertToDouble();
                T_MRP += dr["MRP"].ConvertToDouble();
            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_ReceiveQty}}", T_ReceiveQty.To2DecimalPlace());
            html = html.Replace("{{T_FreeQty}}", T_FreeQty.To2DecimalPlace());
            html = html.Replace("{{T_TotalQty}}", T_TotalQty.To2DecimalPlace());
            html = html.Replace("{{T_MRP}}", T_MRP.To2DecimalPlace());

            return html;

        }


        public string ViewGRNPurchaseReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
         
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptGrnPurchaseReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0, LastPrice = 0, Rate = 0, TotalAmount = 0, CGSTAmt = 0, SGSTAmt = 0, IGSTAmt = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrnNumber"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");

          
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ReceiveQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["FreeQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LastDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LastPrice"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Rate"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td>");
                
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td></tr>");
                LastPrice += dr["LastPrice"].ConvertToDouble();
                Rate += dr["Rate"].ConvertToDouble();
                TotalAmount += dr["TotalAmount"].ConvertToDouble();
                CGSTAmt += dr["CGSTAmt"].ConvertToDouble();
                SGSTAmt += dr["SGSTAmt"].ConvertToDouble();
                IGSTAmt += dr["IGSTAmt"].ConvertToDouble();
                T_NetAmount += dr["NetAmount"].ConvertToDouble();
            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{IGSTAmt}}", IGSTAmt.To2DecimalPlace());
            html = html.Replace("{{SGSTAmt}}", SGSTAmt.To2DecimalPlace());
            html = html.Replace("{{CGSTAmt}}", CGSTAmt.To2DecimalPlace());
            html = html.Replace("{{TotalAmount}}", TotalAmount.To2DecimalPlace());
            html = html.Replace("{{Rate}}", Rate.To2DecimalPlace());
            html = html.Replace("{{LastPrice}}", LastPrice.To2DecimalPlace());
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;

        }



        public string ViewSupplierWiseGRNList(int StoreId, int SupplierID, DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@SupplierID", SupplierID) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[3] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptSupplierWiseGrnList", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            string previousLabel = "";
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0, T_NetAmount = 0, NetAmount = 0, T_DiscAmount = 0, T_MRP = 0, T_Rate = 0, T_TotalAmount = 0, T_VatAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["SupplierName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["SupplierName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='13' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Supplier Wise Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(NetAmount.To2DecimalPlace()).Append("</td></tr>");

                    NetAmount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["SupplierName"].ConvertToString()).Append("</td></tr>");

                }
                NetAmount += dr["NetAmount"].ConvertToDouble();
                NetAmount = NetAmount ;
                T_Count = T_Count + 1;
                previousLabel = dr["SupplierName"].ConvertToString();

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrnNumber"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["InvoiceNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MRP"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Rate"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatPercentage"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiscAmount"].ConvertToDouble()).Append("</td>");
             
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td></tr>");

                T_MRP += dr["MRP"].ConvertToDouble();

                T_Rate += dr["Rate"].ConvertToDouble();

                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();

                T_VatAmount += dr["VatAmount"].ConvertToDouble();

                T_DiscAmount += dr["DiscAmount"].ConvertToDouble();

                T_NetAmount += dr["NetAmount"].ConvertToDouble();

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='13' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Supplier Wise Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(NetAmount.To2DecimalPlace()).Append("</td></tr>");


                }

            }
            //html = html.Replace("{{T_MRP}}", Bills.GetColValue("T_MRP").ConvertToDouble().ToString("0.00"));
            //html = html.Replace("{{chkdmrpflag}}", Bills.GetColValue("T_MRP").ConvertToDouble() > 0 ? "table-row" : "none");




            //html = html.Replace("{{T_Rate}}", Bills.GetColValue("T_Rate").ConvertToDouble().ToString("0.00"));
            //html = html.Replace("{{chkdrateflag}}", Bills.GetColValue("T_Rate").ConvertToDouble() > 0 ? "table-row" : "none");


            //html = html.Replace("{{chkdtotalamountflag}}", Bills.GetColValue("T_TotalAmount").ConvertToDouble() > 0 ? "table-row" : "none");
            //html = html.Replace("{{T_TotalAmount}}", Bills.GetColValue("T_TotalAmount").ConvertToDouble().ToString("0.00"));


            //html = html.Replace("{{chkdvatamountflag}}", Bills.GetColValue("T_VatAmount").ConvertToDouble() > 0 ? "table-row" : "none");
            //html = html.Replace("{{T_VatAmount}}", Bills.GetColValue("T_VatAmount").ConvertToDouble().ToString("0.00"));


            //html = html.Replace("{{chkddiscamountflag}}", Bills.GetColValue("T_DiscAmount").ConvertToDouble() > 0 ? "table-row" : "none");
            //html = html.Replace("{{T_DiscAmount}}", Bills.GetColValue("T_DiscAmount").ConvertToDouble().ToString("0.00"));

            //html = html.Replace("{{chkdnetamountflag}}", Bills.GetColValue("T_NetAmount").ConvertToDouble() > 0 ? "table-row" : "none");
            //html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("T_NetAmount").ConvertToDouble().ToString("0.00"));


            html = html.Replace("{{T_MRP}}", T_MRP.To2DecimalPlace());
            html = html.Replace("{{T_Rate}}", T_Rate.To2DecimalPlace());
            html = html.Replace("{{T_TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{T_VatAmount}}", T_VatAmount.To2DecimalPlace());
            html = html.Replace("{{T_DiscAmount}}", T_DiscAmount.To2DecimalPlace());

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewIssueToDepartment(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId, int ItemId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[5];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@FromStoreId", FromStoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@ToStoreId", ToStoreId) { DbType = DbType.Int64 };
            para[4] = new SqlParameter("@ItemId", ItemId) { DbType = DbType.Int64};

            var Bills = GetDataTableProc("rptIssueToDepartmentDetails", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["FromStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ToStoreName"].ConvertToString()).Append("</td>");
             
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IssueNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IssueDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IssueQty"].ConvertToString()).Append("</td>");
       
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PerUnitLandedRate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatPercentage"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LandedTotalAmount"].ConvertToDouble()).Append("</td></tr>");
                T_NetAmount += dr["LandedTotalAmount"].ConvertToDouble();
            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewIssueToDepartmentItemWise(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId, int ItemId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[5];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@FromStoreId", FromStoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@ToStoreId", ToStoreId) { DbType = DbType.Int64 };
            para[4] = new SqlParameter("@ItemId", ToStoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIssueToDepartmentDetails", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double NetAmount = 0, Dcount = 0, T_NetAmount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["ItemName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["ItemName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Supplier Wise Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(NetAmount.ToString()).Append("</td></tr>");

                    NetAmount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["ItemName"].ConvertToString()).Append("</td></tr>");

                }

                //Dcount = Dcount +;
                //T_Count = T_Count + 1;
                previousLabel = dr["ItemName"].ConvertToString();

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["FromStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ToStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IssueNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IssueDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
              
              

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IssueQty"].ConvertToDouble()).Append("</td></tr>");

                T_NetAmount += dr["IssueQty"].ConvertToDouble();
                NetAmount += dr["IssueQty"].ConvertToDouble();

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">  Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(NetAmount.ToString()).Append("</td></tr>");


                }

            }

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewReturnFromDepartment(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@FromStoreId", FromStoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@ToStoreId", ToStoreId) { DbType = DbType.Int64 };
           

            var Bills = GetDataTableProc("rptReturnFromDepartmentDetails", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["FromStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ToStore"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ReturnNo"].ConvertToString()).Append("</td>");
                
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ReturnQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["UnitLandedRate"].ConvertToString()).Append("</td>");


                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalLandedRate"].ConvertToDouble()).Append("</td></tr>");
                T_NetAmount += dr["TotalLandedRate"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewPurchaseOrder(DateTime FromDate, DateTime ToDate, int SupplierID, int ToStoreId, string htmlFilePath, string htmlHeader)
        {

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@SupplierID", SupplierID) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@ToStoreId", ToStoreId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptPurchaseDateWise", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PurchaseNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PurchaseDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrnNumber"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["InvoiceNo"].ConvertToString()).Append("</td>");
              
               
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td></tr>");
                T_NetAmount += dr["TotalAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewMaterialConsumptionMonthlySummary(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptMaterialConsumptionDateWise", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");



                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");


                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MRPTotalAmount"].ConvertToDouble()).Append("</td></tr>");
                T_NetAmount += dr["MRPTotalAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewMaterialConsumption(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
         
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptMaterialConsumptionDet", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
              
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
           
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PerUnitLandedRate"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PerUnitPurchaseRate"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Remark"].ConvertToString()).Append("</td></tr>");
                T_NetAmount += dr["PerUnitPurchaseRate"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewItemExpiryReport(int ExpMonth, int ExpYear, int StoreID, DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@ExpMonth", ExpMonth) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@ExpYear", ExpYear) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@StoreID", StoreID) { DbType = DbType.Int64 };
         

           


            var Bills = GetDataTableProc("RptItemExpReportMonthWise", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ExpYear"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
              

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceQty"].ConvertToString()).Append("</td></tr>");
                T_NetAmount += dr["BalanceQty"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewCurrentStockReport(int StoreId,int IsNarcotic, int ish1Drug, int isScheduleH,int IsHighRisk,int IsScheduleX, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[6];
           
         
            para[0] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@IsNarcotic", IsNarcotic) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@ish1Drug", ish1Drug) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@isScheduleH", isScheduleH) { DbType = DbType.Int64 };
            para[4] = new SqlParameter("@IsHighRisk", IsHighRisk) { DbType = DbType.Int64 };
            para[5] = new SqlParameter("@IsScheduleX", IsScheduleX) { DbType = DbType.Int64 };



            var Bills = GetDataTableProc("rptCurrentStockStoreWise", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy  hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


             

                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PurUnitRateWF"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalPur"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["UnitMRP"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalMRP"].ConvertToDouble()).Append("</td></tr>");
                T_NetAmount += dr["TotalMRP"].ConvertToDouble();
            }
            html = html.Replace("{{PurUnitRateWF}}", Bills.GetColValue("PurUnitRateWF").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{chkdunitrateflag}}", Bills.GetColValue("PurUnitRateWF").ConvertToDouble() > 0 ? "table-row" : "none");


         

            html = html.Replace("{{UnitMRP}}", Bills.GetColValue("UnitMRP").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{chkdunitmrpflag}}", Bills.GetColValue("UnitMRP").ConvertToDouble() > 0 ? "table-row" : "none");

          
            html = html.Replace("{{chkdtotalmrpflag}}", Bills.GetColValue("TotalMRP").ConvertToDouble() > 0 ? "table-row" : "none");
            html = html.Replace("{{TotalMRP}}", Bills.GetColValue("TotalMRP").ConvertToDouble().ToString("0.00"));

        
            html = html.Replace("{{chkdtotalpurflag}}", Bills.GetColValue("TotalPur").ConvertToDouble() > 0 ? "table-row" : "none");
            html = html.Replace("{{TotalPur}}", Bills.GetColValue("TotalPur").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewCurrentStockCategoryWise(int StoreId, int IsNarcotic, int ish1Drug, int isScheduleH, int IsHighRisk, int IsScheduleX, int ItemCategaryId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[7];


            para[0] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@IsNarcotic", IsNarcotic) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@ish1Drug", ish1Drug) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@isScheduleH", isScheduleH) { DbType = DbType.Int64 };
            para[4] = new SqlParameter("@IsHighRisk", IsHighRisk) { DbType = DbType.Int64 };
            para[5] = new SqlParameter("@IsScheduleX", IsScheduleX) { DbType = DbType.Int64 };
            para[6] = new SqlParameter("@ItemCategaryId", ItemCategaryId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("m_rptCurrentStockCategorywise", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy  hh:mm tt"));

            //StringBuilder items = new StringBuilder("");
            //int i = 0, j = 0;
            //double T_NetAmount = 0;

            //string previousLabel = "";

            //foreach (DataRow dr in Bills.Rows)
            //{
            //    i++; j++;
            //    if (i == 1)
            //    {
            //        String Label;
            //        Label = dr["ItemCategoryName"].ConvertToString();
            //        items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
            //    }
            //    if (previousLabel != "" && previousLabel != dr["ItemCategoryName"].ConvertToString())
            //    {
            //        j = 1;

            //        //items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
            //        //   .Append(Dcount.ToString()).Append("</td></tr>");

            //        //Dcount = 0;
            //        items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["ItemCategoryName"].ConvertToString()).Append("</td></tr>");

            //    }

            //    //Dcount = Dcount + 1;
            //    //T_Count = T_Count + 1;
            //    previousLabel = dr["ItemCategoryName"].ConvertToString();


            //    items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");




            //    items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
            //    items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceQty"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PurUnitRateWF"].ConvertToDouble()).Append("</td>");
            //    items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalPur"].ConvertToDouble()).Append("</td>");
            //    items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["UnitMRP"].ConvertToDouble()).Append("</td>");
            //    items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalMRP"].ConvertToDouble()).Append("</td></tr>");
            //    T_NetAmount += dr["TotalMRP"].ConvertToDouble();
            //}
            //html = html.Replace("{{PurUnitRateWF}}", Bills.GetColValue("PurUnitRateWF").ConvertToDouble().ToString("0.00"));
            //html = html.Replace("{{chkdunitrateflag}}", Bills.GetColValue("PurUnitRateWF").ConvertToDouble() > 0 ? "table-row" : "none");




            //html = html.Replace("{{UnitMRP}}", Bills.GetColValue("UnitMRP").ConvertToDouble().ToString("0.00"));
            //html = html.Replace("{{chkdunitmrpflag}}", Bills.GetColValue("UnitMRP").ConvertToDouble() > 0 ? "table-row" : "none");


            //html = html.Replace("{{chkdtotalmrpflag}}", Bills.GetColValue("TotalMRP").ConvertToDouble() > 0 ? "table-row" : "none");
            //html = html.Replace("{{TotalMRP}}", Bills.GetColValue("TotalMRP").ConvertToDouble().ToString("0.00"));


            //html = html.Replace("{{chkdtotalpurflag}}", Bills.GetColValue("TotalPur").ConvertToDouble() > 0 ? "table-row" : "none");
            //html = html.Replace("{{TotalPur}}", Bills.GetColValue("TotalPur").ConvertToDouble().ToString("0.00"));

            //html = html.Replace("{{Items}}", items.ToString());
            ////html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            ////html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            //html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            //return html;
            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;
            string previousLabel = "";

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                // Only append the first row's Doctor Name once
                if (i == 1)
                {
                    string Label = dr["DoctorName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">")
                          .Append(Label)
                          .Append("</td></tr>");
                }

                string currentCategory = dr["ItemCategoryName"].ConvertToString();

                // Append ItemCategoryName only if it is different from the previous one
                if (previousLabel != currentCategory)
                {
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                          .Append("<td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">")
                          .Append(currentCategory)
                          .Append("</td></tr>");
                }

                // Update previousLabel to the current ItemCategoryName
                previousLabel = currentCategory;

                // Append item details for each row
                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">")
                     .Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>")
                     .Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>")
                     .Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>")
                     .Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>")
                     .Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>")
                     .Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceQty"].ConvertToString()).Append("</td>")
                     .Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PurUnitRateWF"].ConvertToDouble()).Append("</td>")
                     .Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalPur"].ConvertToDouble()).Append("</td>")
                     .Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["UnitMRP"].ConvertToDouble()).Append("</td>")
                     .Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalMRP"].ConvertToDouble()).Append("</td></tr>");

                T_NetAmount += dr["TotalMRP"].ConvertToDouble();
            }

            // Replacing placeholders in HTML
            html = html.Replace("{{PurUnitRateWF}}", Bills.GetColValue("PurUnitRateWF").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{chkdunitrateflag}}", Bills.GetColValue("PurUnitRateWF").ConvertToDouble() > 0 ? "table-row" : "none");
            html = html.Replace("{{UnitMRP}}", Bills.GetColValue("UnitMRP").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{chkdunitmrpflag}}", Bills.GetColValue("UnitMRP").ConvertToDouble() > 0 ? "table-row" : "none");
            html = html.Replace("{{chkdtotalmrpflag}}", Bills.GetColValue("TotalMRP").ConvertToDouble() > 0 ? "table-row" : "none");
            html = html.Replace("{{TotalMRP}}", Bills.GetColValue("TotalMRP").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{chkdtotalpurflag}}", Bills.GetColValue("TotalPur").ConvertToDouble() > 0 ? "table-row" : "none");
            html = html.Replace("{{TotalPur}}", Bills.GetColValue("TotalPur").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;

        }

        public string ViewClosingCurrentStockReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            throw new NotImplementedException();
        }

        public string ViewItemWiseSupplierList(int StoreId, int SupplierID, int ItemId, DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[5];
            para[0] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@SupplierID", SupplierID) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@ItemId", ItemId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[4] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };




            var Bills = GetDataTableProc("rptItemWiseGrnList", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");




                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrnNumber"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["InvoiceNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ReceiveQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["FreeQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MRP"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Rate"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatPercentage"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiscAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td></tr>");
                T_NetAmount += dr["NetAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewCurrentStockDateWise(DateTime InsertDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@InsertDate", InsertDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
           




            var Bills = GetDataTableProc("rptCurrentStock_DateWise_StoreWise", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Insert_Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");


                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
       
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["UnitMRP"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalMRP"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PurchaseRate"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalPurchase"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LandedRate"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalLanded"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatPercentage"].ConvertToString()).Append("</td></tr>");
                T_NetAmount += dr["TotalLanded"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewNonMovingItemList(DateTime FromDate, DateTime ToDate, int NonMovingDay, int StoreId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@NonMovingDay", NonMovingDay) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };




            var Bills = GetDataTableProc("rptNonMovingItemList", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");




              
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
         
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DaySales"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LastSalesDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td></tr>");
                T_NetAmount += dr["PerUnitPurchaseRate"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewNonMovingItemWithoutBatchList(DateTime FromDate, DateTime ToDate,int NonMovingDay,int StoreId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@NonMovingDay", NonMovingDay) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };




            var Bills = GetDataTableProc("rptNonMovingItemListWithoutBatchNo", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");





                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
            

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DaySales"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LastSalesDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td></tr>");
                T_NetAmount += dr["PerUnitPurchaseRate"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewPatientWiseMaterialConsumption(DateTime FromDate, DateTime ToDate, int Id,int ToStoreId,string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[4];

           
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@Id", Id) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@ToStoreId", ToStoreId) { DbType = DbType.Int64 };




            var Bills = GetDataTableProc("rptPatMaterialConsDetails", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

              


                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MrpAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotMrpAmt"].ConvertToDouble()).Append("</td>");
               
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Remark"].ConvertToString()).Append("</td></tr>");
                T_NetAmount += dr["TotMrpAmt"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewLastPurchaseRateWiseConsumtion(DateTime FromDate, DateTime ToDate,int ItemId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[3];


            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@ItemId", ItemId) { DbType = DbType.Int64 };
        




            var Bills = GetDataTableProc("rptPurRateWiseConsumptionReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");




              
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Lastpur"].ConvertToString()).Append("</td>");
              
              

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PRQC"].ConvertToDouble().ToString("0.00")).Append("</td></tr>");
                T_NetAmount += dr["PRQC"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewItemCount(DateTime FromDate, DateTime ToDate,int ItemId,int ToStoreId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[4];


            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@ItemId", ItemId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@ToStoreId", ToStoreId) { DbType = DbType.Int64 };




            var Bills = GetDataTableProc("rptItemCount", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");





                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
              



                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalReceived"].ConvertToString()).Append("</td></tr>");
                T_NetAmount += dr["TotalReceived"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewSupplierWiseDebitCreditNote(DateTime FromDate, DateTime ToDate,int SupplierId,int StoreId,  string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[4];


            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@SupplierId", SupplierId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };




            var Bills = GetDataTableProc("rptSupplierWiseDebitCreditNote", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");




                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrnNumber"].ConvertToString()).Append("</td>");
        
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalDiscAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalVATAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DebitNote"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CreditNote"].ConvertToString()).Append("</td></tr>");
                T_NetAmount += dr["NetAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewStockAdjustmentReport(DateTime FromDate, DateTime ToDate,int ToStoreId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[3];


            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@ToStoreId", ToStoreId) { DbType = DbType.Int64 };
          




            var Bills = GetDataTableProc("rptStockAdjustmentReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");




                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MRPAdg"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PreBalQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Ad_DD_Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AfterBalQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Ad_DD_Type"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["UserName"].ConvertToString()).Append("</td></tr>");
                //T_NetAmount += dr["NetAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            return html;
        }

        public string ViewPurchaseWiseGRNSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            throw new NotImplementedException();
        }
    }
}





