using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_IPPrescriptionReturn
    {
        public bool Insert(IPPrescriptionReturnParams IPPrescriptionReturnParams);


        string ViewIPPrescriptionReturnReceipt(int PresReId,string htmlFilePath, string HeaderName);

        string ViewIPPrescriptionReturnfromwardReceipt(DateTime FromDate,DateTime ToDate, int Reg_No, string htmlFilePath, string HeaderName);
    }
}
