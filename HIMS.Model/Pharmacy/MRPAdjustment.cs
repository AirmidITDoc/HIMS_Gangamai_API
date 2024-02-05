using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
   public class MRPAdjustment
    {
        public InsertMRPAdju InsertMRPAdju { get; set; }
        public InsertMRPAdjuNew InsertMRPAdjuNew { get; set; }

    }

    public class InsertMRPAdju
    {


        public int StoreId { get; set; }
        public int ItemId { get; set; }


        public String BatchNo { get; set; }
        public float OldMrp { get; set; }

        public float OldLandedRate { get; set; }
        public float OldPurRate { get; set; }


        public float Qty { get; set; }
        public float Mrp { get; set; }

        public float LandedRate { get; set; }
        public float PurRate { get; set; }


        public int AddedBy { get; set; }
        public DateTime AddedDateTime { get; set; }


    }

    public class InsertMRPAdjuNew
    {

        public int StoreId { get; set; }

        public int Stockid { get; set; }

        public float ItemId { get; set; }
        public String BatchNo { get; set; }

        public float PerUnitMrp { get; set; }
        public float PerUnitPurrate { get; set; }

        public float PerUnitLanedrate { get; set; }
        public float OldUnitMrp { get; set; }


        public float OldUnitPur { get; set; }
        public float OldUnitLanded { get; set; }



    }

}