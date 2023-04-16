using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Inventory
{
    public class TermsofPaymentMasterParams
    {
        public InsertTermsofPaymentMaster InsertTermsofPaymentMaster { get; set; }
        public UpdateTermsofPaymentMaster UpdateTermsofPaymentMaster { get; set; }

    }
    public class InsertTermsofPaymentMaster
    {
        public string TermsOfPayment{get;set;}
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }
    }
    public class UpdateTermsofPaymentMaster
    {
        public long Id { get; set; }
        public string TermsOfPayment { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }

    }
}
