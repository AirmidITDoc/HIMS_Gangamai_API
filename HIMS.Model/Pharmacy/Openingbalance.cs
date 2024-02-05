using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
    public class Openingbalance
    {
        public InsertOpeningbalanceheader InsertOpeningbalanceheader { get; set; }

        public List<InsertOpeningbalancedetail> InsertOpeningbalancedetail { get; set; }
    }

    public class InsertOpeningbalanceheader
    {
      

        public int StoreId { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime OpeningTime { get; set; }
       
         public long AddedBy { get; set; }
        public int OpeningHId { get; set; }
       
    }

    public class InsertOpeningbalancedetail{

        public long StoreId { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime OpeningTime { get; set; }
     
        public int OpeningDocNo { get; set; }
        public int ItemId { get; set; }

        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public float PerUnitPurRate { get; set; }
        public float PerUnitMrp { get; set; }
        public float VatPer { get; set; }
        public int BalQty { get; set; }
        public int Addedby { get; set; }
        public int OpeningId { get; set; }

    }
}
