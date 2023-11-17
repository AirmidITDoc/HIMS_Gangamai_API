using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
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
            html = html.Replace("{{RegNo}}", Convert.ToString(Bills.Rows[0]["RegNo"]));
            html = html.Replace("{{StoreName}}", Convert.ToString(Bills.Rows[0]["StoreName"]));
            html = html.Replace("{{StoreAddress}}", Convert.ToString(Bills.Rows[0]["StoreAddress"]));
            html = html.Replace("{{GSTIN}}", Convert.ToString(Bills.Rows[0]["GSTIN"]));
            html = html.Replace("{{DL_NO}}", Convert.ToString(Bills.Rows[0]["DL_NO"]));
            html = html.Replace("{{PayMode}}", Convert.ToString(Bills.Rows[0]["PayMode"]));
            html = html.Replace("{{PatientName}}", Convert.ToString(Bills.Rows[0]["PatientName"]));
            html = html.Replace("{{ExtMobileNo}}", Convert.ToString(Bills.Rows[0]["ExtMobileNo"]));
            html = html.Replace("{{SalesNo}}", Convert.ToString(Bills.Rows[0]["SalesNo"]));
            html = html.Replace("{{Date}}", Convert.ToDateTime(Bills.Rows[0]["Date"]).ToString("dd/MM/yyyy hh:mm tt"));
            StringBuilder items = new StringBuilder("");
            int i = 0;
            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                items.Append("<tr><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(Convert.ToString(dr["HSNcode"])).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">-</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(Convert.ToString(dr["ItemName"])).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">-</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(Convert.ToString(dr["BatchNo"])).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(Convert.ToDateTime(dr["BatchExpDate"]).ToString("dd/MM/yyyy hh:mm tt")).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(Convert.ToString(dr["Qty"])).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(Convert.ToString(dr["UnitMRP"])).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(Convert.ToString(dr["TotalAmount"])).Append("</td></tr>");
            }
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{HTotalAmount}}", Convert.ToString(Bills.Rows[0]["HTotalAmount"]));
            html = html.Replace("{{CGSTPer}}", Convert.ToString(Bills.Rows[0]["CGSTPer"]));
            html = html.Replace("{{CGSTAmt}}", Convert.ToString(Bills.Rows[0]["CGSTAmt"]));
            html = html.Replace("{{SGSTPer}}", Convert.ToString(Bills.Rows[0]["SGSTPer"]));
            html = html.Replace("{{SGSTAmt}}", Convert.ToString(Bills.Rows[0]["SGSTAmt"]));
            html = html.Replace("{{IGSTPer}}", Convert.ToString(Bills.Rows[0]["IGSTPer"]));
            html = html.Replace("{{IGSTAmt}}", Convert.ToString(Bills.Rows[0]["IGSTAmt"]));
            html = html.Replace("{{DiscAmount}}", Convert.ToString(Bills.Rows[0]["DiscAmount"]));
            html = html.Replace("{{TotalGst}}", Convert.ToString(Bills.Rows[0]["TotalGst"]));
            html = html.Replace("{{NetAmount}}", Convert.ToString(Bills.Rows[0]["NetAmount"]));
            html = html.Replace("{{UserName}}", Convert.ToString(Bills.Rows[0]["UserName"]));


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
