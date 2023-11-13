using HIMS.Model.Pharmacy;

namespace HIMS.Data.Pharmacy
{
    public interface I_SalesReturn
    {
        public string InsertSalesReturnCredit(SalesReturnCreditParams salesReturnParams);
        public string InsertSalesReturnPaid(SalesReturnCreditParams salesReturnParams);
    }
}
