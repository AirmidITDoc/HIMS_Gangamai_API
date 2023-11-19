using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace HIMS.Data.Pharmacy
{
    public class R_Sales : GenericRepository, I_Sales
    {
        public R_Sales(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public String InsertSales(SalesParams salesParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@SalesId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId3 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = salesParams.SalesInsert.ToDictionary();
            disc3.Remove("SalesId");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Sales_1", disc3, outputId1);

            foreach (var a in salesParams.SalesDetailInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["SalesID"] = BillNo;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_SalesDetails_1", disc5);
            }

            foreach (var a in salesParams.UpdateCurStkSales)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_T_CurStk_Sales_Id_1", disc1);
            }

            var vDiscCal = salesParams.Cal_DiscAmount_Sales.ToDictionary();
            vDiscCal["SalesID"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_Sales", vDiscCal);

            var vGSTCal = salesParams.Cal_GSTAmount_Sales.ToDictionary();
            vGSTCal["SalesID"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("m_Cal_GSTAmount_Sales", vDiscCal);

            var vPayment = salesParams.SalesPayment.ToDictionary();
            vPayment["BillNo"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_Pharmacy_New_1", vPayment);

            _unitofWork.SaveChanges();
            return BillNo;
        }

        public String InsertSalesWithCredit(SalesCreditParams salesCreditParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@SalesId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId3 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = salesCreditParams.SalesInsertCredit.ToDictionary();
            disc3.Remove("SalesId");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Sales_1", disc3, outputId1);

            foreach (var a in salesCreditParams.SalesDetailInsertCredit)
            {
                var disc5 = a.ToDictionary();
                disc5["SalesID"] = BillNo;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_SalesDetails_1", disc5);
            }

            foreach (var a in salesCreditParams.UpdateCurStkSalesCredit)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_T_CurStk_Sales_Id_1", disc1);
            }

            var vDiscCal = salesCreditParams.Cal_DiscAmount_SalesCredit.ToDictionary();
            vDiscCal["SalesID"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_Sales", vDiscCal);

            var vGSTCal = salesCreditParams.Cal_GSTAmount_SalesCredit.ToDictionary();
            vGSTCal["SalesID"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("m_Cal_GSTAmount_Sales", vDiscCal);

            _unitofWork.SaveChanges();
            return BillNo;
        }

        public bool PaymentSettlement(SalesParams salesParams)
        {
            var vPayment = salesParams.SalesPayment.ToDictionary();
            vPayment.Remove("PaymentID");
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_Pharmacy_New_1", vPayment);

            var vUpdateHeader = salesParams.update_Pharmacy_BillBalAmount.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_Pharmacy_BillBalAmount_1", vUpdateHeader);

            _unitofWork.SaveChanges();
            return true;
        }

        public String ViewBill(int SalesID, int OP_IP_Type, string htmlFilePath)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                { "SalesID", SalesID },
                { "OP_IP_Type", OP_IP_Type }
            };
            var Bills = GetDataTableProc("rptSalesPrint", dictionary);
            string html = File.ReadAllText(htmlFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{StoreName}}", Bills.GetColValue("StoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
            html = html.Replace("{{GSTIN}}", Bills.GetColValue("GSTIN"));
            html = html.Replace("{{DL_NO}}", Bills.GetColValue("DL_NO"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{ExtMobileNo}}", Bills.GetColValue("ExtMobileNo"  ));
            html = html.Replace("{{SalesNo}}", Bills.GetColValue("SalesNo"));
            html = html.Replace("{{Date}}", Bills.GetDateColValue("Date"));
            StringBuilder items = new StringBuilder("");
            int i = 0;
            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                items.Append("<tr><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["HSNcode"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">-</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">-</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["BatchExpDate"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["UnitMRP"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["TotalAmount"].ConvertToString()).Append("</td></tr>");
            }
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{HTotalAmount}}", Bills.GetColValue("HTotalAmount"));
            html = html.Replace("{{CGSTPer}}", Bills.GetColValue("CGSTPer"));
            html = html.Replace("{{CGSTAmt}}", Bills.GetColValue("CGSTAmt"));
            html = html.Replace("{{SGSTPer}}", Bills.GetColValue("SGSTPer"));
            html = html.Replace("{{SGSTAmt}}", Bills.GetColValue("SGSTAmt"));
            html = html.Replace("{{IGSTPer}}", Bills.GetColValue("IGSTPer"));
            html = html.Replace("{{IGSTAmt}}", Bills.GetColValue("IGSTAmt"));
            html = html.Replace("{{DiscAmount}}", Bills.GetColValue("DiscAmount"));
            html = html.Replace("{{TotalGst}}", Bills.GetColValue("TotalGst"));
            html = html.Replace("{{NetAmount}}", Bills.GetColValue("NetAmount"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


            return html;

        }
        public string GetFilePath()
        {
            // for live
            //var dt = GetDataTableQuery("SELECT TOP 1 FilePathLocation FROM ConfigSetting order by ConfigId DESC", null);
            // for local
            var dt = new DataTable();
            dt.Columns.Add("s");
            dt.Rows.Add("");
            return Convert.ToString(dt.Rows[0][0]);
        }

    }
}
