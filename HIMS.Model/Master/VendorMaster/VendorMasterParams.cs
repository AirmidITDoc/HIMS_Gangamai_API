using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.VendorMaster
{
   public class VendorMasterParams
    {
        public VendorMasterInsert VendorMasterInsert { get; set; }
        public VendorMasterUpdate VendorMasterUpdate { get; set; }
    }

    public class VendorMasterInsert
    {
        public string VendorName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public long BankId { get; set; }
        public long CategoryID { get; set; }
        public Boolean IsDeleted { get; set; }
        public long AddedBy { get; set; }
    }

    public class VendorMasterUpdate
    {
        public long VendorId { get; set; }
        public string VendorName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public long BankId { get; set; }
        public long CategoryID { get; set; }
        public Boolean IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
    }
}

