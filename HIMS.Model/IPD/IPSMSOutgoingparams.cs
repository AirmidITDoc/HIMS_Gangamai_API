using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public class IPSMSOutgoingparams
    {
        public IPSMSOutgoingInsert IPSMSOutgoingInsert { get; set; }
    }

    public class IPSMSOutgoingInsert
    {
      
        public String MobileNo { get; set; }
        public String SMSString { get; set; }
        public bool IsSent { get; set; }
     

    }
}
