using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Transaction
{
   public class Sw_Bill_infoParams
    {
        public Sw_Bill_infoInsert Sw_Bill_infoInsert { get; set; }
        public Sw_Bill_infoUpdate Sw_Bill_infoUpdate { get; set; }
    }

    public class Sw_Bill_infoInsert
    {
        public int BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public int ClientId { get; set; }
        public long BillAmount { get; set; }
        public long PaidAmount { get; set; }
        public long BalanceAmount { get; set; }
        public String Comment { get; set; }
        public int AddedBy { get; set; }
    }

    public class Sw_Bill_infoUpdate
    {
        public int BillId { get; set; }
        public int BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public int ClientId { get; set; }
        public long BillAmount { get; set; }
        public long PaidAmount { get; set; }
        public long BalanceAmount { get; set; }
        public String Comment { get; set; }
       public long UpdatedBy { get; set; }
    }
}

