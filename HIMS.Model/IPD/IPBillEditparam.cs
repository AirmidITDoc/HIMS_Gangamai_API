using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public class IPBillEditparam
    {
        public List<UpdateAddChargesforBillEdit> UpdateAddChargesforBillEdit { get; set; }

        public List<UpdateBillForEdit> UpdateBillForEdit { get; set; }

        public List<UpdatePaymentForBillEdit> UpdatePaymentForBillEdit { get; set; }
    }


    public class UpdateAddChargesforBillEdit
    {

        public long ChargeId { get; set; }
        public long Price { get; set; }
        public long Qty { get; set; }

        public long TotalAmt { get; set; }
    }


    public class UpdateBillForEdit
    {
        public long BillNo { get; set; }
        public long TotalBillAmt { get; set; }
        public long ConcessionAmt { get; set; }

        public long NetPayableAmt { get; set; }

        public long TotalAdvanceAmount { get; set; }

        public long IsBillCheck { get; set; }

    }


    public class UpdatePaymentForBillEdit
    {
        public int PaymentId { get; set; }
        public long CashPayAmount { get; set; }
        public long ChequePayAmount { get; set; }

        public long CardPayAmount { get; set; }
        public String CardNo { get; set; }

        public String CardBankName { get; set; }
        public long AdvanceUsedAmount { get; set; }


        public long NEFTPayAmount { get; set; }

        public String NEFTNo { get; set; }

        public String NEFTBankMaster { get; set; }

        public long PayTMAmount { get; set; }

        public String PayTMTranNo { get; set; }

    }
}
