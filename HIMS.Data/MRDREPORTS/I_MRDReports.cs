using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.MRDREPORTS
{
    public interface I_MRDReports
    {
        string ViewAdmissionRegister(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, string htmlFilePath, string htmlHeader);
        string ViewDischargeRegister(DateTime FromDate, DateTime ToDate, int DoctorID, int DischargeTypeId, string htmlFilePath, string htmlHeader);
        //string ViewCharityReport(DateTime FromDate, DateTime ToDate, int DoctorID, string htmlFilePath, string htmlHeader);
        //string ViewCharityReport25A(DateTime FromDate, DateTime ToDate, int OP_IP_Type, int DoctorID, string htmlFilePath, string htmlHeader);
        string ViewIPDischargeDetails(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewPatientRegister(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewDeathCensusReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
    }
}
