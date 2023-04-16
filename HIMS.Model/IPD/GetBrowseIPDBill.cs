using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class GetBrowseIPDBill
    {
        public string F_Name { get; set; }
        public string L_Name { get; set; }
        public DateTime From_Dt { get; set; }
        public DateTime To_Dt { get; set; }
        public long Reg_No { get; set; }
        public string PBillNo { get; set; }
        public int IsInterimOrFinal { get; set; }
        public long CompanyId { get; set; }
    }
}
