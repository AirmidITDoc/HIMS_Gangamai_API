using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Billing
{
   public class CompanyMasterParams
    {
        public CompanyMasterInsert CompanyMasterInsert { get; set; }
        public CompanyMasterUpdate CompanyMasterUpdate { get; set; }

    }
     
    public class CompanyMasterInsert
    {
        public int CompTypeId { get; set; }
        public String CompanyName { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String PinNo { get; set; }
        public String PhoneNo { get; set; }
        public String MobileNo { get; set; }
        public String FaxNo { get; set; }
        public int TariffId { get; set; }
        public Boolean IsDeleted { get; set; }
        public int AddedBy { get; set; }
        public Boolean IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }

    }

    public class CompanyMasterUpdate
    {
        
        public int CompanyId { get; set; }
        public int CompTypeId { get; set; }
        public String CompanyName { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String PinNo { get; set; }
        public String PhoneNo { get; set; }
        public String MobileNo { get; set; }
        public String FaxNo { get; set; }
        public int TariffId { get; set; }
        public Boolean IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
        public Boolean IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }


    }
}

