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
        string ViewDoctorShareReport(int Doctor_Id, int GroupId, DateTime From_Dt, DateTime To_Dt, int OP_IP_Type, string htmlFilePath, string htmlHeaderFilePath);
        string ViewDoctorWiseSummaryReport(DateTime FromDate, DateTime ToDate, int DoctorId, int OPD_IPD_Type, string htmlFilePath, string htmlHeader);
        string ViewConDoctorShareDetails(DateTime FromDate, DateTime ToDate, int DoctorId, int OPD_IPD_Type, string htmlFilePath, string htmlHeader);
        string ViewDoctorShareListWithCharges(DateTime FromDate, DateTime ToDate, int Doctor_Id, int GroupId,int OP_IP_Type, string htmlFilePath, string htmlHeader);
    }
}
