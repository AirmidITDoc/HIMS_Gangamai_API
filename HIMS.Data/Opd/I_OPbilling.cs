using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd
{
   public interface I_OPbilling
    {
        public string Insert(OPbillingparams OPbillingparams);
        string ViewOPBillReceipt(int BillNo, string htmlFilePath, string htmlHeaderFilePath);
        string ViewOPBillDailyReportReceipt(DateTime FromDate, DateTime ToDate,int AddedById, string htmlFilePath, string htmlHeaderFilePath);

        //string ViewOPDailyCollectionReceipt(DateTime FromDate,DateTime ToDate, int AddedById,int DoctorId, string htmlFilePath, string htmlHeaderFilePath);
    }
}
