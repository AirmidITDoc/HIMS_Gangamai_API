using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master
{
    public class HospitalMaster
    {
        public long HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public string HospitalHeaderLine { get; set; }
        public string EmailID { get; set; }
        public string WebSiteInfo { get; set; }
        public string Header {  get; set; }
    }

    public class HospitalStoreMaster
    {
        public long StoreId { get; set; }
        public string PrintStoreName { get; set; }
        public string StoreAddress { get; set; }
        public string HospitalMobileNo { get; set; }
        public string HospitalEmailId { get; set; }
        public string PrintStoreUnitName { get; set; }
        public string DL_NO { get; set; }
        public string GSTIN { get; set; }
        public string Header { get; set; }
    }
}
