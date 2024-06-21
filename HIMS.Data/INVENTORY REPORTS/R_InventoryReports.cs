//using HIMS.Common.Utility;
//using HIMS.Data.MISReports;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
//using System.Text;
//using HIMS.Common.Utility;
//using HIMS.Data.InventoryReports;

//namespace HIMS.Data.Opd
//{
//    public class R_InventoryReport : GenericRepository, I_InventoryReport
//    {
       

//        public R_InventoryReport(IUnitofWork unitofWork) : base(unitofWork)
//        {

//        }


        //public string ViewItemList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        //{
            

        //    SqlParameter[] para = new SqlParameter[0];
        //    //para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
        //    //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
        //    var Bills = GetDataTableProc("rptItemList", para);


        //    string html = File.ReadAllText(htmlFilePath);

        //    html = html.Replace("{{NewHeader}}", htmlHeader);
        //    html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

        //    StringBuilder items = new StringBuilder("");
        //    int i = 0, j = 0;
        //    //double T_Count = 0;
        //    string previousLabel = "";

            

        //    foreach (DataRow dr in Bills.Rows)
        //    {
        //        i++; j++;
        //        if (i == 1)
        //        {
        //            String Label;
        //            Label = dr["DoctorName"].ConvertToString();
        //            items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td>");
        //            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(j).Append("</td></tr>");
        //        }

        //        if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
        //        {
        //            j = 1;


        //            items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    
        //                    .Append("</td></tr>");
                   

        //            items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");
        //        }



        //        previousLabel = dr["DoctorName"].ConvertToString();

        //        items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegID"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitTime"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");
        //        T_Count += dr["PatientName"].ConvertToDouble();
        //    }

        //    html = html.Replace("{{T_Count}}", T_Count.ToString());
        //    html = html.Replace("{{Items}}", items.ToString());
        //    html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
        //    html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
        //    return html;

        //}

        //public string View(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        //{
        //    // throw new NotImplementedException();

        //    SqlParameter[] para = new SqlParameter[0];
        //    //para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
        //    //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
        //    var Bills = GetDataTableProc("rptItemList", para);


        //    string html = File.ReadAllText(htmlFilePath);

        //    html = html.Replace("{{NewHeader}}", htmlHeader);
        //    html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

        //    StringBuilder items = new StringBuilder("");
        //    int i = 0, j = 0;
        //    //double T_Count = 0;

        //    string previousLabel = "";



        //    foreach (DataRow dr in Bills.Rows)
        //    {
        //        i++; j++;
        //        if (i == 1)
        //        {
        //            String Label;
        //            Label = dr["IPD Revenue"].ConvertToString();
        //            Label = dr["OPD Revenue"].ConvertToString();
        //            items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td>");
        //            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(j).Append("</td></tr>");
        //        }

        //        if (previousLabel != "" && previousLabel != dr["IPD Revenue"].ConvertToString())
        //        {
        //            j = 1;


        //            items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">New And Follow Up</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")

        //                    .Append("</td></tr>");


        //            items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["IPD Revenue"].ConvertToString()).Append("</td></tr>");
        //        }

        //        if (previousLabel != "" && previousLabel != dr["OPD Revenue"].ConvertToString())
        //        {
        //            j = 1;


        //            items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">New And Follow Up</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")

        //                    .Append("</td></tr>");


        //            items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["OPD Revenue"].ConvertToString()).Append("</td></tr>");
        //        }


        //        previousLabel = dr["IPDRevenue"].ConvertToString();

        //        items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetpayableAmount"].ConvertToString()).Append("</td></tr>");
                
        //        T_Count += dr["IPDRevenue"].ConvertToDouble();
        //    }


        //    html = html.Replace("{{T_Count}}", T_Count.ToString());

        //    html = html.Replace("{{Items}}", items.ToString());
        //    html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
        //    html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
        //    return html;


//        }
//        public string ViewDepartmentandDoctorWiseOPBillingReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            // throw new NotImplementedException();

//            SqlParameter[] para = new SqlParameter[2];
//            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
//            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

//            var Bills = GetDataTableProc("rtrv_IPCompanyWiseBillInfo", para);


//            string html = File.ReadAllText(htmlFilePath);

//            html = html.Replace("{{NewHeader}}", htmlHeader);
//            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

//            StringBuilder items = new StringBuilder("");
//            int i = 0;
//            double T_TotalBillAmount = 0;


//            foreach (DataRow dr in Bills.Rows)
//            {
//                i++;

//                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
               
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToDouble()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillAmount"].ConvertToString()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmountAmount"].ConvertToDouble()).Append("</td></tr>");



//                T_TotalBillAmount += dr["BillAmount"].ConvertToDouble();
                

//            }


//            html = html.Replace("{{Items}}", items.ToString());
//            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
//            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

//            html = html.Replace("{{TotalBillAmount}}", T_TotalBillAmount.To2DecimalPlace());
           
//            return html;

//        }
//        public string ViewDepartmentandDoctorWiseIPBillingReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            // throw new NotImplementedException();

//            SqlParameter[] para = new SqlParameter[2];
//            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
//            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

//            var Bills = GetDataTableProc("rtrv_IPCompanyWiseBillInfo", para);


//            string html = File.ReadAllText(htmlFilePath);

//            html = html.Replace("{{NewHeader}}", htmlHeader);
//            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

//            StringBuilder items = new StringBuilder("");
//            int i = 0;
//            double T_TotalBillAmount = 0;


//            foreach (DataRow dr in Bills.Rows)
//            {
//                i++;

//                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToDouble()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillAmount"].ConvertToString()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmountAmount"].ConvertToDouble()).Append("</td></tr>");



//                T_TotalBillAmount += dr["BillAmount"].ConvertToDouble();


//            }


//            html = html.Replace("{{Items}}", items.ToString());
//            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
//            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

//            html = html.Replace("{{TotalBillAmount}}", T_TotalBillAmount.To2DecimalPlace());

//            return html;

//        }

//        public string ViewDepartmentWiseOPRevenueReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            // throw new NotImplementedException();

//            SqlParameter[] para = new SqlParameter[2];
//            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
//            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

//            var Bills = GetDataTableProc("rtrv_IPCompanyWiseBillInfo", para);


//            string html = File.ReadAllText(htmlFilePath);

//            html = html.Replace("{{NewHeader}}", htmlHeader);
//            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

//            StringBuilder items = new StringBuilder("");
//            int i = 0;
//            double T_TotalRevenue = 0;


//            foreach (DataRow dr in Bills.Rows)
//            {
//                i++;

//                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorAvailable"].ConvertToString()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["FullTime"].ConvertToString()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OnCall"].ConvertToString()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalPatients"].ConvertToString()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Revenue"].ConvertToDouble()).Append("</td></tr>");



//                T_TotalRevenue += dr["Revenue"].ConvertToDouble();


//            }


//            html = html.Replace("{{Items}}", items.ToString());
//            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
//            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

//            html = html.Replace("{{TotalRevenue}}", T_TotalRevenue.To2DecimalPlace());

//            return html;

//        }

//        public string ViewDepartmentWiseIPRevenueReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            // throw new NotImplementedException();

//            SqlParameter[] para = new SqlParameter[2];
//            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
//            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

//            var Bills = GetDataTableProc("rtrv_IPCompanyWiseBillInfo", para);


//            string html = File.ReadAllText(htmlFilePath);

//            html = html.Replace("{{NewHeader}}", htmlHeader);
//            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

//            StringBuilder items = new StringBuilder("");
//            int i = 0;
//            double T_TotalRevenue = 0;


//            foreach (DataRow dr in Bills.Rows)
//            {
//                i++;

//                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorAvailable"].ConvertToString()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["FullTime"].ConvertToString()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OnCall"].ConvertToString()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalPatients"].ConvertToString()).Append("</td>");
//                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Revenue"].ConvertToDouble()).Append("</td></tr>");



//                T_TotalRevenue += dr["Revenue"].ConvertToDouble();


//            }


//            html = html.Replace("{{Items}}", items.ToString());
//            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
//            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

//            html = html.Replace("{{TotalRevenue}}", T_TotalRevenue.To2DecimalPlace());

//            return html;

//        }

//        public string ViewCityWiseIPPatientCountReport(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId, string htmlFilePath, string HeaderName)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewDepartmentWiseOPandIPRevenueReport(DateTime FromDate, DateTime ToDate, int DoctorID, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewDepartmentandDoctorWiseOPBillingReport(DateTime FromDate, DateTime ToDate, int DoctorID, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewDepartmentandDoctorWiseIPBillingReport(DateTime FromDate, DateTime ToDate, int OP_IP_Type, int DoctorID, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewSupplierList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewIndentReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewMonthlyPurchaseGRNReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewGRNReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewGRNReportNΑΒΗ(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewGRNReturnReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewIssueToDepartmentMonthlySummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewGRNWiseProductQtyReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewGRNPurchaseReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewSupplierWiseGRNList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewIssueToDepartment(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewIssueToDepartmentItemWise(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewReturnFromDepartment(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewPurchaseOrder(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewMaterialConsumptionMonthlySummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewMaterialConsumption(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewItemExpiryReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewCurrentStockReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewClosingCurrentStockReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewItemWiseSupplierList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewCurrentStockDateWise(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewNonMovingItemList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewNonMovingItemWithoutBatchList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewPatientWiseMaterialConsumption(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewLastPurchaseRateWiseConsumtion(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewItemCount(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewSupplierWiseDebitCreditNote(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewStockAdjustmentReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }

//        public string ViewPurchaseWiseGRNSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}





