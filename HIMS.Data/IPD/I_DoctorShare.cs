using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_DoctorShare
    {
        public String Insert(Doctorshareparam Doctorshareparam);

        string ViewDeptdoctorshareReport(int DoctorId,DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
    }
}
