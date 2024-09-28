using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class LabRequesChargesParams
    {
        public LabRequestCharges LabRequestCharges { get; set; }
    }

    public class LabRequestCharges
    {
        public long Opipid { get; set; }
        public long ClassId { get; set; }
        public long ServiceId { get; set; }
        public long TraiffId { get; set; }
        public long ReqDetId { get; set; }
        public long UserId { get; set; }
        public DateTime ChargesDate { get; set; }
    }
}
