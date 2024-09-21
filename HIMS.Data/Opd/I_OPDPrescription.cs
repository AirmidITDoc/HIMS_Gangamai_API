using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HIMS.Data.Opd
{
   public interface I_OPDPrescription
    {
        bool Insert(OPDPrescriptionParams OPDPrescriptionParams);
        DataTable GetDataForReport(int VisitId);
        string ViewOPPrescriptionReceipt(DataTable Bills, string htmlFilePath, string HeaderName);
    }
}
