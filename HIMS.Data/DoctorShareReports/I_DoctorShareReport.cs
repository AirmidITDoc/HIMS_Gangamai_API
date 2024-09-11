using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace HIMS.Data.DoctorShareReports
{
    public interface I_DoctorShareReport
    {
        string ViewDoctorShareReport(DateTime FromDate, DateTime ToDate, int DoctorId, string htmlFilePath, string htmlHeaderFilePath);
        string ViewDoctorWiseSummaryReport(DateTime FromDate, DateTime ToDate, int DoctorId,string htmlFilePath, string htmlHeader);
        string ViewConDoctorShareDetails(DateTime FromDate, DateTime ToDate, int DoctorId, string htmlFilePath, string htmlHeader);
        string ViewDoctorShareListWithCharges(DateTime FromDate, DateTime ToDate, int DoctorId, string htmlFilePath, string htmlHeader);
    }
}
