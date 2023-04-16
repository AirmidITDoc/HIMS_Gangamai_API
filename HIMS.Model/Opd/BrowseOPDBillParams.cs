using System;

namespace HIMS.Model.Opd
{
    public class BrowseOPDBillParams
    {
        public string F_Name { get; set; }
        public string L_Name { get; set; }
        public DateTime From_Dt { get; set; }
        public DateTime To_Dt { get; set; }
        public int Reg_No { get; set; }
        public string PBillNo { get; set; }
    }
}
