using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
    public class OPBillingCreditparam
    {

        //public List<InsertPathologyReportHeadercredit> InsertPathologyReportHeadercredit { get; set; }

        //public List<InsertRadiologyReportHeadercredit> InsertRadiologyReportHeadercredit { get; set; }
        public InsertBillcreditupdatewithbillno InsertBillcreditupdatewithbillno { get; set; }
        public List<OpBillDetailscreditInsert> OpBillDetailscreditInsert { get; set; }
        public List<ChargesDetailCreditInsert> ChargesDetailCreditInsert { get; set; }
        public OPCalDiscAmountBillcredit OPCalDiscAmountBillcredit { get; set; }
    }
        public class InsertPathologyReportHeadercredit
        {
            public DateTime PathDate { get; set; }
            public DateTime PathTime { get; set; }
            public Boolean OPD_IPD_Type { get; set; }
            public int OPD_IPD_Id { get; set; }
            public int PathTestID { get; set; }
            public int AddedBy { get; set; }
            public int ChargeID { get; set; }
            public Boolean IsCompleted { get; set; }
            public Boolean IsPrinted { get; set; }
            public Boolean IsSampleCollection { get; set; }
            public Boolean TestType { get; set; }
        }
        public class InsertRadiologyReportHeadercredit
        {
            public DateTime RadDate { get; set; }
            public DateTime RadTime { get; set; }
            public Boolean OPD_IPD_Type { get; set; }
            public int OPD_IPD_Id { get; set; }
            public int RadTestID { get; set; }
            public int AddedBy { get; set; }
            public Boolean IsCancelled { get; set; }
            public int ChargeID { get; set; }
            public Boolean IsPrinted { get; set; }
            public Boolean IsCompleted { get; set; }
            public Boolean TestType { get; set; }
        }
        public class InsertBillcreditupdatewithbillno
        {
            public int BillNo { get; set; }
            public int OPD_IPD_ID { get; set; }
            public float TotalAmt { get; set; }
            public float ConcessionAmt { get; set; }
            public float NetPayableAmt { get; set; }
            public float PaidAmt { get; set; }
            public float BalanceAmt { get; set; }
            public DateTime BillDate { get; set; }
            public int OPD_IPD_Type { get; set; }
            public int AddedBy { get; set; }
            public float TotalAdvanceAmount { get; set; }
            public DateTime BillTime { get; set; }
            public int ConcessionReasonId { get; set; }
            public Boolean IsSettled { get; set; }
            public Boolean IsPrinted { get; set; }
            public Boolean IsFree { get; set; }
            public int CompanyId { get; set; }
            public int TariffId { get; set; }
            public int UnitId { get; set; }
            public int InterimOrFinal { get; set; }
            public int CompanyRefNo { get; set; }
            public int ConcessionAuthorizationName { get; set; }
            public float TaxPer { get; set; }
            public float TaxAmount { get; set; }
            public string DiscComments { get; set; }
             public float CompDiscAmt { get; set; }
        }
        public class OpBillDetailscreditInsert
        {
            public int BillNo { get; set; }
            public int ChargesId { get; set; }

        }
        public class ChargesDetailCreditInsert
        {
        public DateTime ChargesDate { get; set; }
        public bool OPD_IPD_Type { get; set; }
        public int OPD_IPD_Id { get; set; }
        public int ServiceId { get; set; }
        public long Price { get; set; }
        public long Qty { get; set; }
        public float TotalAmt { get; set; }
        public float ConcessionPercentage { get; set; }
        public float ConcessionAmount { get; set; }
        public float NetAmount { get; set; }
        public int DoctorId { get; set; }
        public float DocPercentage { get; set; }
        public float DocAmt { get; set; }
        public float HospitalAmt { get; set; }
        public bool IsGenerated { get; set; }
        public int AddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
        public bool IsPathology { get; set; }
        public bool IsRadiology { get; set; }
        public bool IsPackage { get; set; }
        public int PackageMainChargeID { get; set; }
        public bool IsSelfOrCompanyService { get; set; }
        public int PackageId { get; set; }
        public DateTime ChargeTime { get; set; }
        public int ClassId { get; set; }
        public int BillNo { get; set; }
        public int ChargeID { get; set; }

        }
        public class OPCalDiscAmountBillcredit
         {
            public int BillNo { get; set; }

        }

    }

