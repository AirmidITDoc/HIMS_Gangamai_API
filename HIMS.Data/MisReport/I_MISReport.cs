using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace HIMS.Data.MISReports
{
    public interface I_MISReport
    {
        string ViewCityWiseIPPatientCountReport(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId, string htmlFilePath, string HeaderName);
        string ViewDepartmentWiseOPandIPRevenueReport(DateTime FromDate, DateTime ToDate,int DoctorID, string htmlFilePath, string htmlHeader);
        string ViewDepartmentandDoctorWiseOPBillingReport(DateTime FromDate, DateTime ToDate, int DoctorID, string htmlFilePath, string htmlHeader);
        string ViewDepartmentandDoctorWiseIPBillingReport(DateTime FromDate, DateTime ToDate, int OP_IP_Type, int DoctorID, string htmlFilePath, string htmlHeader);
        string ViewDepartmentWiseOPRevenueReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDepartmentWiseIPRevenueReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
    }
}
