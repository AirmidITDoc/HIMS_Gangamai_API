using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd.OP
{
   public class ConfigSettingparam
    {
        public ConfigsettingUpdate Configsettingupdate { get; set; }
    }

    public class ConfigsettingUpdate
    {
        public bool ConfigId { get; set; }
        public int PrintRegAfterReg { get; set; }
        public String IPDPrefix { get; set; }
        public int OTCharges { get; set; }
        public int PrintOPDCaseAfterVisit { get; set; }
        public int PrintIPDAfterAdm { get; set; }
        public int PopOPBillAfterVisit { get; set; }
        public int PopPayAfterOPBill { get; set; }
        public int GenerateOPBillInCashOption { get; set; }
        public int MandatoryFirstName { get; set; }
        public int MandatoryLastName { get; set; }
        public int MandatoryMiddleName { get; set; }
        public int MandatoryAddress { get; set; }
        public int MandatoryCity { get; set; }
        public int MandatoryAge { get; set; }
        public int MandatoryPhoneNo { get; set; }
        public int OPD_Billing_CounterId { get; set; }
        public int OPD_Receipt_CounterId { get; set; }
        public int OPD_Refund_Bill_CounterId { get; set; }
        public int OPD_Advance_CounterId { get; set; }
        public int OPD_Refund_Advance_CounterId { get; set; }
        public int IPD_Advance_CounterId { get; set; }
        public int IPD_Billing_CounterId { get; set; }
        public int IPD_Receipt_CounterId { get; set; }
        public int IPD_Refund_of_Bill_CounterId { get; set; }
        public int IPD_Refund_of_Advance_CounterId { get; set; }
        public String RegPrefix { get; set; }
        public String IPPrefix { get; set; }
        public String OPPrefix { get; set; }
        public int PathDepartment { get; set; }
        public int IsPathologistDr { get; set; }
        public int PatientTypeSelf { get; set; }
        public int SalesCounterId { get; set; }
        public int SalesReturnCounterId { get; set; }
        public int SalesReceiptCounterID { get; set; }
        public bool ClassForEdit { get; set; }
    }
}
