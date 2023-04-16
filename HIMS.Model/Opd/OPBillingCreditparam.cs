using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
    public class OPBillingCreditparam
    {

        public List<InsertPathologyReportHeadercredit> InsertPathologyReportHeadercredit { get; set; }

        public List<InsertRadiologyReportHeadercredit> InsertRadiologyReportHeadercredit { get; set; }
        public InsertBillcreditupdatewithbillno InsertBillcreditupdatewithbillno { get; set; }

        public List<OpBillDetailscreditInsert> OpBillDetailscreditInsert { get; set; }

        //  public OPoctorShareGroupAdmChargeDoc OPoctorShareGroupAdmChargeDoc { get; set; }

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
            //public string BillRemark { get; set; }
            public string DiscComments { get; set; }
        }

        public class OpBillDetailscreditInsert
        {
            public int BillNo { get; set; }
            public int ChargesId { get; set; }

            // public int BillDetailId { get; set; }
        }




        public class OPCalDiscAmountBillcredit
         {
            public int BillNo { get; set; }

        }

    }

