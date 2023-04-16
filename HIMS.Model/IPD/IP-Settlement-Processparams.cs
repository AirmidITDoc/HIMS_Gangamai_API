using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public class IP_Settlement_Processparams
    {
        public UpdateIpBill UpdateIpBill { get; set; }
        public IPPaymentCreditUpdate IPPaymentCreditUpdate { get; set; }

        public List<IPsettlementAdvanceDetailUpdate> IPsettlementAdvanceDetailUpdate { get; set; }

        public IPsettlementAdvanceHeaderUpdate IPsettlementAdvanceHeaderUpdate { get; set; }
    }

    public class IPPaymentCreditUpdate
    {
        public int PaymentId { get; set; }
        public int BillNo { get; set; }
        //  public String ReceiptNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentTime { get; set; }
        public long CashPayAmount { get; set; }
        public long ChequePayAmount { get; set; }
        public String ChequeNo { get; set; }
        public String BankName { get; set; }
        public DateTime ChequeDate { get; set; }
        public long CardPayAmount { get; set; }
        public String CardNo { get; set; }
        public String CardBankName { get; set; }
        public DateTime CardDate { get; set; }
        public long AdvanceUsedAmount { get; set; }

        public int AdvanceId { get; set; }
        public int RefundId { get; set; }
        public int TransactionType { get; set; }
        public String Remark { get; set; }
        public int AddBy { get; set; }
        public bool IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }

        public int OPD_IPD_Type { get; set; }
        //    public Boolean IsSelfORCompany { get; set; }
        //  public int CompanyId { get; set; }
        public long NEFTPayAmount { get; set; }
        public String NEFTNo { get; set; }
        public String NEFTBankMaster { get; set; }
        public DateTime NEFTDate { get; set; }
        public long PayTMAmount { get; set; }

        public String PayTMTranNo { get; set; }

        public DateTime PayTMDate { get; set; }


    }

    public class UpdateIpBill
    {
        public int BillNo { get; set; }

        public long BillBalAmount { get; set; }

    }

    public class IPsettlementAdvanceDetailUpdate
    {
        public long AdvanceDetailID { get; set; }
        public long UsedAmount { get; set; }
        public long BalanceAmount { get; set; }
    }


    public class IPsettlementAdvanceHeaderUpdate
    {

        public long AdvanceId { get; set; }
        public long AdvanceUsedAmount { get; set; }
        public long BalanceAmount { get; set; }
    }

}