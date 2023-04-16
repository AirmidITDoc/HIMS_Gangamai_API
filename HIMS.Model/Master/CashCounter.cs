using System;

namespace HIMS.Model.Master
{
    public class CashCounter
    {
        public int CashCounterId { get; set; }
        public string CashCounterName { get; set; }
        public string Prefix { get; set; }
        public int BillNo { get; set; }
        public bool IsActive { get; set; }
    }
}
