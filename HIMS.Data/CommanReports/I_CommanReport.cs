using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace HIMS.Data.CommanReports
{
    public interface I_CommanReport
    {

        string ViewDoctorWisePatientCountReport(DateTime FromDate, DateTime ToDate,int DoctorID, string htmlFilePath, string htmlHeader);
        string ViewReferenceDoctorWisePatientCountReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewConcessionReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewCreditReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewIPDischargeBillGenerationPendingReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewIPBillGenerationPaymentDueReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewCollectionSummaryReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewRefByPatientList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDailyCollectionSummaryReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewIPCompanyWiseBillReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewIPCompanyWiseCreditReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);




        string ViewServicewisepatinetamtReport(DateTime FromDate, DateTime ToDate,int ServiceId, string htmlFilePath, string htmlHeader);

        string ViewServicewiseReportwithbill(int ServiceId,DateTime FromDate, DateTime ToDate,string htmlFilePath, string htmlHeader);

        string ViewCanclechargeslist(DateTime FromDate, DateTime ToDate,string htmlFilePath, string htmlHeader);

        string ViewgroupwisecollectionReport(DateTime FromDate, DateTime ToDate,int GroupId, string htmlFilePath, string htmlHeader);
        string ViewGroupwiseSummary(DateTime FromDate, DateTime ToDate,int GroupId, string htmlFilePath, string htmlHeader);

        string ViewGroupwiseRevenuSummary(DateTime FromDate, DateTime ToDate,string htmlFilePath, string htmlHeader);
        string ViewBillSummarytwolakhamt(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
       
        string ViewBillSummarywithtcs(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);

       
    }
}
