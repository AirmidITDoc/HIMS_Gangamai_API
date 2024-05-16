using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace HIMS.Data.OPReports
{
    public interface I_OPBillingReport
    {
        string ViewOPDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, int doctorId, string htmlFilePath, string htmlHeaderFilePath);
        string ViewRegistrationReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDoctorWiseVisitReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewRefDoctorWiseReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewCrossConsultationReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewOPAppointmentListReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDepartmentWisecountSummury(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDoctorWiseVisitCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewAppoinmentListWithServiseAvailed(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDoctorWiseNewAndOldPatientReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDepartmentWiseOpdCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDayWiseOpdCountDetails(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDayWiseOpdCountSummry(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDepartmentWiseOpdCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
       


    }
}
