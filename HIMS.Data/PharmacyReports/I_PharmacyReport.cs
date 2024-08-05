using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace HIMS.Data.PharmacyReports
{
    public interface I_PharmacyReports
    {
        string ViewPharmacyDailycollectionReport(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string HeaderName);
       
    }
}
