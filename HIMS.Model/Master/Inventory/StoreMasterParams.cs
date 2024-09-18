using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HIMS.Model.Master.Inventory
{
    public class StoreMasterParams
    {
        public StoreMasterInsert InsertStoreMaster { get; set; }
        public UpdateStoreMaster UpdateStoreMaster { get; set; }
    }
    public class StoreMasterInsert
    {
        public string StoreShortName { get; set; }
        public string StoreName { get; set; }
        public string IndentPrefix { get; set; }
        public string IndentNo { get; set; }
        public string PurchasePrefix { get; set; }
        public string PurchaseNo { get; set; }
        public string GrnPrefix { get; set; }
        public string GrnNo { get; set; }
        public string GrnreturnNoPrefix { get; set; }
        public string GrnreturnNo { get; set; }
        public string IssueToDeptPrefix { get; set; }
        public string IssueToDeptNo { get; set; }
        public string ReturnFromDeptNoPrefix { get; set; }
        public string ReturnFromDeptNo { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }
        //public long UpdatedBy { get; set; }
        //public string WorkOrderPrefix { get; set; }
        //public string WorkOrderNo { get; set; }
        public long PharSalCountID { get; set; }
        public long PharSalRecCountID { get; set; }
        public long PharSalReturnCountID { get; set; }
        //public long PharAdvId { get; set; }n
        //public long PharAdvReptId { get; set; }
        //public long PharAdvRefId { get; set; }
        //public long PharAdvRefReptId { get; set; }
        //public string PrintStoreName { get; set; }
        public string DL_NO { get; set; }
        public string GSTIN { get; set; }
        //public string StoreAddress { get; set; }
        //public string HospitalMobileNo { get; set; }
        //public string HospitalEmailId { get; set; }
        //public long IsPharStore { get; set; }
        //public long IsWhatsAppMsg { get; set; }
        //public string WhatsAppTemplateId { get; set; }
        //public long IsSMSMsg { get; set; }
        //public string SMSTemplateId { get; set; }
        //public string PrintStoreUnitName { get; set; }
        //public long StoreId { get; set; }
        public string Header { get; set; }
    }
    public class UpdateStoreMaster
    {
        public long StoreId { get; set; }
        public string StoreShortName { get; set; }
        public string StoreName { get; set; }
        public long PharSalCountID { get; set; }
        public long PharSalRecCountID { get; set; }
        public long PharSalReturnCountID { get; set; }
        public long PharAdvId { get; set; }
        public long PharAdvReptId { get; set; }
        public long PharAdvRefId { get; set; }
        public long PharAdvRefReptId { get; set; }
        public string PrintStoreName { get; set; }
        public string DL_NO { get; set; }
        public string GSTIN { get; set; }
        public string StoreAddress { get; set; }
        public string HospitalMobileNo { get; set; }
        public long HospitalEmailId { get; set; }
        public string PrintStoreUnitName { get; set; }
        public long IsPharStore { get; set; }
        public long IsWhatsAppMsg { get; set; }
        public string WhatsAppTemplateId { get; set; }
        public long IsSMSMsg { get; set; }
        public long SMSTemplateId { get; set; }
        public long AddedBy { get; set; }
        public long IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
       // public string Header { get; set; }
    }
}
