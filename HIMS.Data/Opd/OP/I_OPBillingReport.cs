using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd.OP
{
   public interface I_OPBillingReport
    {
        string ViewOPDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, string htmlFilePath, string htmlHeaderFilePath);
        string ViewOPdeptwisecountsummary(DateTime FromDate, DateTime ToDate,string htmlFilePath, string htmlHeaderFilePath);

        string ViewOPDoctorwisecountsummary(DateTime FromDate, DateTime ToDate,string htmlFilePath, string htmlHeaderFilePath);
        string ViewpatientAppointmentwithserviceavailed(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeaderFilePath);

        string ViewOPDoctorwisenewoldpatient(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeaderFilePath);
    }
}
