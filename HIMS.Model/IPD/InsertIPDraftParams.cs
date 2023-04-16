using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public class InsertIPDraftParams
    {
        public IPIntremdraftbillInsert IPIntremdraftbillInsert { get; set; }
        public List<InterimBillDetailsInsert> InterimBillDetailsInsert { get; set; }

       
    }

    public class IPIntremdraftbillInsert
    {
      
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
        public bool IsSettled { get; set; }
        public bool IsPrinted { get; set; }
        public bool IsFree { get; set; }
        public int CompanyId { get; set; }
        public int TariffId { get; set; }
        public int UnitId { get; set; }
        public int InterimOrFinal { get; set; }
        public int CompanyRefNo { get; set; }
        public int ConcessionAuthorizationName { get; set; }
        public float TaxPer { get; set; }
        public float TaxAmount { get; set; }
        public int DRBNo { get; set; }
    }

    public class InterimBillDetailsInsert
    {
        public int DRNo { get; set; }
        public int ChargesId { get; set; }
    }
}
