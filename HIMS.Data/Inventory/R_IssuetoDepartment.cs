﻿using System;
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
    public class R_IssuetoDepartment : GenericRepository, I_IssuetoDepartment
    {
        public R_IssuetoDepartment(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String InsertIssuetoDepartment(IssuetoDepartmentParams issuetoDepartmentParams)
        {
            var vIssueId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@IssueId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = issuetoDepartmentParams.InsertIssuetoDepartmentHeader.ToDictionary();
            disc3.Remove("IssueId");
            var IssueId = ExecNonQueryProcWithOutSaveChanges("m_Insert_IssueToDepartmentHeader_1_New", disc3, vIssueId);

            foreach (var a in issuetoDepartmentParams.InsertIssuetoDepartmentDetail)
            {
                var disc5 = a.ToDictionary();
                disc5["IssueId"] = IssueId;
                ExecNonQueryProcWithOutSaveChanges("m_insert_IssueToDepartmentDetails_1", disc5);

            }
            foreach (var a in issuetoDepartmentParams.updateissuetoDepartmentStock)
            {
                var disc5 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_upd_T_Curstk_issdpt_1", disc5);

            }

            _unitofWork.SaveChanges();
            return IssueId;
        }

        public string ViewIssuetoDeptIssuewise(int IssueId, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@IssueId", IssueId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptPrintIssueToDepartment", para);
            string html = File.ReadAllText(htmlFilePath);
            //string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalVatAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["UnitofMeasurementName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["UnitMRP"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["IssueQty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/Mm/yyyy")).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PerUnitLandedRate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["VatPercentage"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["VatAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["LandedTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                T_TotalVatAmount += dr["VatAmount"].ConvertToDouble();
                T_TotalNETAmount += dr["LandedTotalAmount"].ConvertToDouble();
                //   exec RptPharmacyCreditReport '11-01-2022','11-26-2023',11052,24879,0,10016

            }

            T_TotalVatAmount = Math.Round(T_TotalVatAmount);
            T_TotalNETAmount = Math.Round(T_TotalNETAmount);
            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());

            html = html.Replace("{{Remark}}", Bills.GetColValue("Remark"));
            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            html = html.Replace("{{StoreName}}", Bills.GetColValue("StoreName"));
            html = html.Replace("{{AddedByName}}", Bills.GetColValue("AddedByName").ToString());


            html = html.Replace("{{IssueNo}}", Bills.GetColValue("IssueNo"));

            html = html.Replace("{{IssueTime}}", Bills.GetColValue("IssueTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));

            html = html.Replace("{{StoreName}}", Bills.GetColValue("StoreName"));

            html = html.Replace("{{ToStreName}}", Bills.GetColValue("ToStreName"));
            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));


            //string finalamt = NumberToWords(T_TotalNETAmount.ToInt());


            string finalamt = conversion(T_TotalNETAmount.ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

            return html;
        }

        public string ViewReturnfrdeptdatewise(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId, string htmlFilePath, string htmlHeaderFilePath)
        {
            //throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@FromStoreId", FromStoreId) { DbType = DbType.String };
            para[3] = new SqlParameter("@ToStoreId", ToStoreId) { DbType = DbType.String };

            var Bills = GetDataTableProc("rptReturnFromDepDateWise", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalLRateAmount = 0, T_TotalBalancepay = 0, T_TotalMRPAmount = 0, T_TotalPurchase = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["ReturnNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["ReturnTime"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:left;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:left;\">").Append(dr["ToStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["LandedRateTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["MRPTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["PurchaseTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:left;\">").Append(dr["Remark"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["AddedBy"].ConvertToString()).Append("</td></tr>");


                T_TotalLRateAmount += dr["LandedRateTotalAmount"].ConvertToDouble();
                T_TotalMRPAmount += dr["MRPTotalAmount"].ConvertToDouble();

                T_TotalPurchase += dr["PurchaseTotalAmount"].ConvertToDouble();
                // T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();

                //   exec RptPharmacyCreditReport '11-01-2022','11-26-2023',11052,24879,0,10016

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalLRateAmount}}", T_TotalLRateAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalMRPAmount}}", T_TotalMRPAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalPurchase}}", T_TotalPurchase.To2DecimalPlace());


            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            return html;
        }

        public string ViewReturnfromDeptReturnIdwise(int ReturnId, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@ReturnId", ReturnId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptReturnFromDepartment", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalVatAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["ReturnNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["ReturnTime"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");

                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["FromStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["ToStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["PurchaseTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["UnitLandedRate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                //items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["UnitLandedRate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["TotalLandedRate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["ReturnQty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["RemainingQty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["BalQty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["IssueTime"].ConvertToDateString("dd/MM/yyyy")).Append("</td></tr>");


                //T_TotalVatAmount += dr["VatPercentage"].ConvertToDouble();
                //T_TotalNETAmount += dr["LandedTotalAmount"].ConvertToDouble();
                //   exec RptPharmacyCreditReport '11-01-2022','11-26-2023',11052,24879,0,10016

            }

            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            //html = html.Replace("{{T_TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());


            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));


            return html;
        }



        //public static string NumberToWords(int number)
        //{
        //    if (number == 0)
        //        return "zero";

        //    if (number < 0)
        //        return "minus " + NumberToWords(Math.Abs(number));

        //    string words = "";

        //    if ((number / 1000000) > 0)
        //    {
        //        words += NumberToWords(number / 1000000) + " million ";
        //        number %= 1000000;
        //    }

        //    if ((number / 1000) > 0)
        //    {
        //        words += NumberToWords(number / 1000) + " thousand ";
        //        number %= 1000;
        //    }

        //    if ((number / 100) > 0)
        //    {
        //        words += NumberToWords(number / 100) + " hundred ";
        //        number %= 100;
        //    }

        //    if (number > 0)
        //    {
        //        if (words != "")
        //            words += "and ";

        //        var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        //        var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        //        if (number < 20)
        //            words += unitsMap[number];
        //        else
        //        {
        //            words += tensMap[number / 10];
        //            if ((number % 10) > 0)
        //                words += "-" + unitsMap[number % 10];
        //        }
        //    }

        //    return words;
        //}

        public string ViewIssuetodeptsummary(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId, string htmlFilePath, string htmlHeaderFilePath)
        {
            //  throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@FromStoreId", FromStoreId) { DbType = DbType.String };
            para[3] = new SqlParameter("@ToStoreId", ToStoreId) { DbType = DbType.String };

            var Bills = GetDataTableProc("rptIssueToDepartmentSummary", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalLRateAmount = 0, T_TotalVatAmount = 0, T_TotalMRPAmount = 0, T_NetAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["IssueNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["IssueTime"].ConvertToDateString("dd/MM/yyyy hh:mm tt")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:left;\">").Append(dr["FromStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:left;\">").Append(dr["ToStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["TotalVatAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["UserName"].ConvertToString()).Append("</td></tr>");


                T_TotalLRateAmount += dr["TotalAmount"].ConvertToDouble();
                T_TotalVatAmount += dr["TotalVatAmount"].ConvertToDouble();

                T_NetAmount += dr["NetAmount"].ConvertToDouble();
                // T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();

                //   exec RptPharmacyCreditReport '11-01-2022','11-26-2023',11052,24879,0,10016

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalLRateAmount}}", T_TotalLRateAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

            html = html.Replace("{{AddedByName}}", Bills.GetColValue("AddedByName").ToString());

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            return html;
        }

        public string ViewNonMovingItem(int NonMovingDay, int StoreId, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@NonMovingDay", NonMovingDay) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptNonMovingItemList", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;


                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["LastSalesDate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yyyy hh:mm tt")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["BalanceQty"].ConvertToString()).Append("</td>");


                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["DaySales"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");



            }

            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            html = html.Replace("{{StoreName}}", Bills.GetColValue("StoreName"));

            return html;


        }

        public string ViewExpItemlist(int ExpMonth, int ExpYear, int StoreID, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();


            SqlParameter[] para = new SqlParameter[3];

            para[0] = new SqlParameter("@ExpMonth ", ExpMonth) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@ExpYear ", ExpYear) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@StoreID ", StoreID) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("RptItemExpReportMonthWise", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalVatAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["StockId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yyyy hh:mm tt")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ExpMonth"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ExpYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceQty"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

            }

            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{Remark}}", Bills.GetColValue("Remark"));
            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));


            return html;
        }





        //private void btntowords_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show(words(Convert.ToInt32(textBox1.Text)));
        //}

        public string conversion(string amount)
        {
            double m = Convert.ToInt64(Math.Floor(Convert.ToDouble(amount)));
            double l = Convert.ToDouble(amount);

            double j = (l - m) * 100;
            //string Word = " ";

            var beforefloating = ConvertNumbertoWords(Convert.ToInt64(m));
            var afterfloating = ConvertNumbertoWords(Convert.ToInt64(j));

           // Word = beforefloating + '.' + afterfloating;

            var Content = beforefloating + ' ' + " RUPEES" + ' ' + afterfloating + ' ' + " PAISE only";

            return Content;
        }

        public string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            //if ((number / 10) > 0)  
            //{  
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            // number %= 10;  
            //}  
            if (number > 0)
            {
                if (words != "") words += "AND ";
                var unitsMap = new[]
           {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
           {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        public string ViewIssuetoDeptItemwise(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId, int ItemId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[5];


            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@FromStoreId", FromStoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@ToStoreId", ToStoreId) { DbType = DbType.Int64 };
            para[4] = new SqlParameter("@ItemId", ItemId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptIssueToDepartmentDetails", para);
            string html = File.ReadAllText(htmlFilePath);
            //string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
           
            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0;
           
            double G_TotAmount = 0, G_DiscAmount = 0, G_NETAmount = 0, T_BalAmount = 0, T_PaidAmount = 0;
          
          

            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["ItemName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["UserName"].ConvertToString())
                {
                    j = 1;
                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='9' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Type Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        // .Append(G_TotAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        //.Append(G_DiscAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                       .Append(G_NETAmount.To2DecimalPlace()).Append("</td></tr>");
                    G_TotAmount = 0; G_DiscAmount = 0; G_NETAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:black\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["ItemName"].ConvertToString()).Append("</td></tr>");

                }

                G_TotAmount += dr["TotalAmount"].ConvertToDouble();
                G_DiscAmount += dr["VatAmount"].ConvertToDouble();
                G_NETAmount += dr["NetAmount"].ConvertToDouble();
                //G_PaidAmount += dr["PaidAmount"].ConvertToDouble();
                //G_BalAmount += dr["BalanceAmt"].ConvertToDouble();
                //G_NETPayAmount += dr["NetPayableAmt"].ConvertToDouble();

                //G_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                //G_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                //G_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                //G_PayTMAmount += dr["PayTMAmount"].ConvertToDouble();



                previousLabel = dr["ItemName"].ConvertToString();

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["FromStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ToStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["IssueTime"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["IssueQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PerUnitLandedRate"].ConvertToString()).Append("</td>");
              
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["VatPercentage"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["VatAmount"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td>");

                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='9' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Type Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        //.Append(G_TotAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        //.Append(G_DiscAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                       .Append(G_NETAmount.To2DecimalPlace()).Append("</td></tr>");


                }

            }


          

           

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
           
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName").ToString());

            return html;
        }

        public string ViewItemwiseSupplierlist(int StoreId, int SupplierId, int ItemId, DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[5];

            para[0] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@SupplierId", SupplierId) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@ItemId", ItemId) { DbType = DbType.Int64 };

            para[3] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[4] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
          

            var Bills = GetDataTableProc("rptItemWiseGrnList", para);
            string html = File.ReadAllText(htmlFilePath);
            //string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");

            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0, T_TotalQty=0;


            double G_TotAmount = 0, G_DiscAmount = 0, G_NETAmount = 0, T_BalAmount = 0, T_PaidAmount = 0;



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
                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='8' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Type Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                         .Append(G_TotAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_DiscAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                       .Append(G_NETAmount.To2DecimalPlace()).Append("</td></tr>");
                    G_TotAmount = 0; G_DiscAmount = 0; G_NETAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:black\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["ItemName"].ConvertToString()).Append("</td></tr>");

                }

                G_TotAmount += dr["TotalAmount"].ConvertToDouble();
                G_DiscAmount += dr["VatAmount"].ConvertToDouble();
                T_TotalQty += dr["TotalQty"].ConvertToDouble();
                //G_PaidAmount += dr["PaidAmount"].ConvertToDouble();
                //G_BalAmount += dr["BalanceAmt"].ConvertToDouble();
                //G_NETPayAmount += dr["NetPayableAmt"].ConvertToDouble();

                //G_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                //G_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                //G_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                //G_PayTMAmount += dr["PayTMAmount"].ConvertToDouble();



                previousLabel = dr["ItemName"].ConvertToString();

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["GrnNumber"].ConvertToString()).Append("</td>");
                
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["GRNTime"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["InvoiceNo"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ReceiveQty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Rate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["VatAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalQty"].ConvertToDouble()).Append("</td>");

                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

               items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceQty"].ConvertToString()).Append("</td></tr>");

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='8' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle;margin-right:20px;font-weight:bold;\">Type Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_TotAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_DiscAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                         .Append(T_TotalQty.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append("</td></tr>");


                }

            }






            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName").ToString());

            return html;
        }
    }
}
