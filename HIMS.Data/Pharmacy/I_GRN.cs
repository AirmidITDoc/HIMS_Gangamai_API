using HIMS.Model.Pharmacy;

namespace HIMS.Data.Pharmacy
{
    public interface I_GRN
    {
        public string InsertGRNDirect(GRNParams grnParams);
        public string InsertGRNPurchase(GRNParams grnParams);
        public bool UpdateGRN(GRNParams grnParams);
        public bool VerifyGRN(GRNParams grnParams);
    }
}
