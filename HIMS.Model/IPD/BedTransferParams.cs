using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
  public  class BedTransferParams
    {

        public UpdateBedtransferSetFix UpdateBedtransferSetFix { get; set; }
        public UpdateBedtransferSetFree UpdateBedtransferSetFree { get; set; }
        public UpdateAdmissionBedtransfer UpdateAdmissionBedtransfer { get; set; }
        public UpdateBedtransfer UpdateBedtransfer { get; set; }
    }

   
    public class UpdateBedtransfer
    {
        public long AdmissionID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime FromTime { get; set; }
        public int FromWardID { get; set; }
        public long FromBedId { get; set; }
        public int FromClassId { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime ToTime { get; set; }
        public int ToWardID { get; set; }
        public int ToBedId { get; set; }
        public int ToClassId { get; set; }
        public String Remark { get; set; }
        public int AddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }


    }

    public class UpdateBedtransferSetFree
    {
        public int BedId { get; set; }


    }

    public class UpdateAdmissionBedtransfer
    {
        public int AdmissionID { get; set; }
        public int BedId { get; set; }
        public int WardId { get; set; }
        public int ClassId { get; set; }


    }


    public class UpdateBedtransferSetFix
    {
        public int BedId { get; set; }


    }
}

