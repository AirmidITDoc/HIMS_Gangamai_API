﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace HIMS.Data.CommanReports
{
    public interface I_CommanReport
    {
        string ViewDoctorWisePatientCountReportDetails(DateTime FromDate, DateTime ToDate, int DoctorID, string htmlFilePath, string htmlHeader);
        string ViewCommanDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId, string htmlFilePath, string HeaderName);
        string ViewDoctorWisePatientCountReport(DateTime FromDate, DateTime ToDate,int DoctorID, string htmlFilePath, string htmlHeader);
        string ViewReferenceDoctorWisePatientCountReport(DateTime FromDate, DateTime ToDate, int DoctorID, string htmlFilePath, string htmlHeader);
        string ViewConcessionReport(DateTime FromDate, DateTime ToDate, int OP_IP_Type, int DoctorID, string htmlFilePath, string htmlHeader);
        string ViewCreditReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewIPDischargeBillGenerationPendingReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewIPBillGenerationPaymentDueReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewCollectionSummaryReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewRefByPatientList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDailyCollectionSummaryReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewIPCompanyWiseBillReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewIPCompanyWiseCreditReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewGroupWiseCollectionReport(DateTime FromDate, DateTime ToDate, int GroupId, string htmlFilePath, string htmlHeader);
        string ViewGroupwiseRevenueSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewGroupwiseSummaryReport(DateTime FromDate, DateTime ToDate,int GroupId, string htmlFilePath, string htmlHeader);
        string ViewServiceWiseReportWithoutBill(int ServiceId, DateTime FromDate, DateTime ToDate, int DoctorId,string htmlFilePath, string htmlHeader);

        string ViewServicewiseReportwithbill(int ServiceId, DateTime FromDate, DateTime ToDate,int DoctorId, string htmlFilePath, string htmlHeader);
        string ViewDoctorVisitAdmittedWiseGroupReport(DateTime FromDate, DateTime ToDate,int DoctorId, string htmlFilePath, string htmlHeader);

        string ViewBillSummaryWithTCS(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewCanclechargeslist(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);



        string ViewBillSummaryReportfor2LakhAmount(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDoctorAndDepartmentWiseMonthlyCollectionReport(int DepartmentId,int DoctorId,DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        //string ViewServiceWiseReport(int ServiceId, DateTime FromDate, DateTime ToDate,int DoctorId, string htmlFilePath, string htmlHeader);
        string ViewCashCounterWiseDailyCollection(DateTime FromDate, DateTime ToDate,int OP_IP_Type,int CashCounterId,int UserId, string htmlFilePath, string htmlHeader);
        string ViewCashCounterWiseDailyCollectionSummary(DateTime FromDate, DateTime ToDate, int OP_IP_Type, int CashCounterId, int UserId, string htmlFilePath, string htmlHeader);
        string ViewOPPatientLedger(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewIPPatientLedger(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);


    }
}
