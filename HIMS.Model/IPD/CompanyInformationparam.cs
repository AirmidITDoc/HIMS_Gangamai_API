using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public class CompanyInformationparam
    {
        public CompanyUpdate CompanyUpdate { get; set; }
    }

    public class CompanyUpdate
    {

     
        public String PolicyNo { get; set; }
        public String ClaimNo { get; set; }
        public long EstimatedAmount { get; set; }
        public long ApprovedAmount { get; set; }
        public long HosApreAmt { get; set; }
        public long PathApreAmt { get; set; }

        public long PharApreAmt { get; set; }
        public long RadiApreAmt { get; set; }
        public long C_DisallowedAmt { get; set; }
        public long CompDiscount { get; set; }
        public long HDiscAmt { get; set; }
        public long C_OutsideInvestAmt { get; set; }
        public long RecoveredByPatient { get; set; }
        public long C_FinalBillAmt { get; set; }
        
        public long MedicalApreAmt { get; set; }
        public int AdmissionID { get; set; }

    }
}
