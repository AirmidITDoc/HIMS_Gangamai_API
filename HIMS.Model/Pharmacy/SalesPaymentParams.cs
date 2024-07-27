using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
    public class SalesPaymentParams
    {
        public SalesPaymentSettlement SalesPaymentSettlement { get; set; }
        public update_Pharmacy_BillBalAmountSettlement update_Pharmacy_BillBalAmountSettlement { get; set; }
        public List<update_T_PHAdvanceDetailSettlement> update_T_PHAdvanceDetailSettlement { get; set; }
        public update_T_PHAdvanceHeaderSettlement update_T_PHAdvanceHeaderSettlement { get; set; }
    }
    public class SalesPaymentSettlement
    {
        public long BillNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentTime { get; set; }
        public float CashPayAmount { get; set; }
        public float ChequePayAmount { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public DateTime ChequeDate { get; set; }
        public float CardPayAmount { get; set; }
        public string CardNo { get; set; }
        public string CardBankName { get; set; }
        public DateTime CardDate { get; set; }
        public float AdvanceUsedAmount { get; set; }
        public long AdvanceId { get; set; }
        public long RefundId { get; set; }
        public long TransactionType { get; set; }
        public string Remark { get; set; }
        public long AddBy { get; set; }
        public bool IsCancelled { get; set; }
        public long IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
        public long OPD_IPD_Type { get; set; }
        public float NEFTPayAmount { get; set; }
        public string NEFTNo { get; set; }
        public string NEFTBankMaster { get; set; }
        public DateTime NEFTDate { get; set; }
        public float PayTMAmount { get; set; }
        public string PayTMTranNo { get; set; }
        public DateTime PayTMDate { get; set; }
        public long PaymentId { get; set; }
    }

    public class update_Pharmacy_BillBalAmountSettlement
    {
        public long SalesID { get; set; }
        public float BalanceAmount { get; set; }
        public float SalRefundAmt { get; set; }
    }
    public class update_T_PHAdvanceDetailSettlement
    {
        public long AdvanceDetailID { get; set; }
        public float UsedAmount { get; set; }
        public float BalanceAmount { get; set; }
    }
    public class update_T_PHAdvanceHeaderSettlement
    {
        public long AdvanceId { get; set; }
        public float AdvanceUsedAmount { get; set; }
        public float BalanceAmount { get; set; }

    }
}
