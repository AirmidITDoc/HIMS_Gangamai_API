using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_IPPrescription
    {
        public bool Insert(IPPrescriptionParams IPPrescriptionParams);
        string ViewIPPrescriptionReceipt(int OP_IP_ID, int PatientType, string htmlFilePath, string HeaderName);
        string ViewOPPrescriptionReceipt(int VisitId, int PatientType, string htmlFilePath, string HeaderName);
    }
}
