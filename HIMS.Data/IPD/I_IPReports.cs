using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_IPReports
    {
        string ViewIPDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, string htmlFilePath, string HeaderName);

        string ViewCommanDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId, string htmlFilePath, string HeaderName);

        string ViewOPIPBillSummaryReceipt(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);

        string ViewIPCreditReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewIPDAdmissionListCompanyWiseSummary(DateTime FromDate, DateTime ToDate, BigInteger DoctorId, BigInteger WardId, string htmlFilePath, string HeaderName);

        string ViewIPDAdmissionWardWiseCharges(int DoctorId, int WardId,int CompanyId, string htmlFilePath, string HeaderName);


        string ViewIPDDischargeReportWithMarkStatus(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewIPDDischargeReportWithBillSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDepartmentWiseCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDoctorWiseCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);

        string ViewCurrentrefadmittedlist(int DoctorId, string htmlFilePath, string HeaderName);

        string ViewIPDDischargetypewise(int DoctorId, DateTime FromDate, DateTime ToDate,int DischargeTypeId, string htmlFilePath, string HeaderName);


        string ViewOPToIPConvertedListWithServiceAvailed(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);

        //IPD MIS REPORTS

        string ViewDateWiseAdmissionCount(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewMonthWiseAdmissionCount(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDateWiseDoctorWiseAdmissionCountDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDateWiseDoctorWiseAdmissionCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDateWiseDepartmentWiseAdmissionCountDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDateWiseDepartmentWiseAdmissionCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDrWiseCollectionDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDrWiseCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDepartmentWiseCollectionDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewDepartmentWiseCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewCompanyWiseAdmissionCountDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewCompanyWiseAdmissionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
    }
}
