using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class AdvanceParamCancelPram
    {
        public AdvanceParamCancelPrams AdvanceParamCancelPrams { get; set; }


     
    }

    public class AdvanceParamCancelPrams
    {
        public int IsCancelled { get; set; }
       
        public int AdvanceId { get; set; }
        public int AdvanceDetailId { get; set; }
        public int UserId { get; set; }
        public float AdvanceAmount { get; set; }

    }

  
   
   
}
